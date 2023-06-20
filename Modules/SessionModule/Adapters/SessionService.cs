using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using SmartEdu.Modules.SessionModule.Core;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SmartEdu.Modules.SessionModule.Adapters
{
    public class SessionService
    {
        public SessionService(IDistributedCache tokensBlackListCache) 
        {
            _tokensBlackList = tokensBlackListCache;
        }

        public static AuthenticationTokenOptions tokenOptions { set; get; } = null!;

        private IDistributedCache _tokensBlackList;

        public TimeSpan deltaExpire;

        public string CreateToken(List<Claim> claims, TimeSpan? deltaExpire)
        {
            this.deltaExpire = deltaExpire ?? this.deltaExpire;

            var token = new JwtSecurityToken(
                            issuer: tokenOptions.issuer,
                            audience: tokenOptions.audience,
                            claims: claims,
                            expires: DateTime.UtcNow + deltaExpire,
                            signingCredentials: new SigningCredentials(tokenOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string CreateAccessToken(List<Claim> claims, TimeSpan? deltaExpire)
        {
            return CreateToken(claims, deltaExpire);
        }

        public string CreateRefreshToken(List<Claim> claims)
        {
            return CreateToken(claims, null);
        }

        public TokensData RefreshTokens(string refreshToken, string accessToken)
        {
            var decodedRefreshToken = DecodeToken(refreshToken);
            var decodedAccessToken = DecodeToken(accessToken);

            var refreshTokenClaims = new List<Claim>();

            foreach(var claim in decodedRefreshToken)
                refreshTokenClaims.Add(new Claim(claim.Key, claim.Value));

            var accessTokenClaims = new List<Claim>();

            foreach (var claim in decodedAccessToken)
                accessTokenClaims.Add(new Claim(claim.Key, claim.Value));

            return new TokensData(CreateRefreshToken(refreshTokenClaims), CreateAccessToken(accessTokenClaims, deltaExpire));
        }

        public async Task<bool> CheckBlackList(string token)
        {
            var decodedToken = DecodeToken(token);

            return await _tokensBlackList.GetStringAsync(decodedToken["userId"]) != null;
        }

        public async void InvalidateToken(string token)
        {
            var decodedToken = DecodeToken(token);

            await _tokensBlackList.SetStringAsync(decodedToken["userId"], token, new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = Convert.ToDateTime(decodedToken["exp"])
            });
        }

        public Dictionary<string, string> DecodeToken(string token)
        {
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            return jwtToken.Claims.ToDictionary(claim => claim.Type, claim => claim.Value);
        }
    }
}
