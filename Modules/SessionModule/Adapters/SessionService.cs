using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using SmartEdu.Modules.SessionModule.DTO;

namespace SmartEdu.Modules.SessionModule.Adapters
{
    public class SessionService
    {
        public SessionService(IDistributedCache accessTokensBlackListCache, IDistributedCache refreshTokensWhiteListCache) 
        {
            _accessTokensBlackList = accessTokensBlackListCache;
            _refreshTokensWhiteList = refreshTokensWhiteListCache;
        }

        public static AuthenticationTokenOptions tokenOptions { set; get; } = null!;

        /// <summary>
        /// Redis cache
        /// </summary>
        private IDistributedCache _accessTokensBlackList;

        /// <summary>
        /// Redis cache
        /// </summary>
        private IDistributedCache _refreshTokensWhiteList;

        /// <summary>
        /// Time between generate token and its release
        /// </summary>
        public TimeSpan deltaExpire;

        /// <summary>
        /// Create JWT
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="deltaExpire"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Create access JWT
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="deltaExpire"></param>
        /// <returns></returns>
        public string CreateAccessToken(List<Claim> claims, TimeSpan? deltaExpire)
        {
            return CreateToken(claims, deltaExpire);
        }

        /// <summary>
        /// Create refresh JWT
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public async Task<string> CreateRefreshToken(List<Claim> claims)
        {
            var token = CreateToken(claims, null);

            await _refreshTokensWhiteList.SetStringAsync(claims.FirstOrDefault(claim => claim.Type == "userId")!.Value, token);

            return token;
        }

        /// <summary>
        /// Refresh access and refresh tokens
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<RefreshTokensDTO> RefreshTokens(string refreshToken, string accessToken)
        {
            // decode tokens
            var decodedRefreshToken = DecodeToken(refreshToken);
            var decodedAccessToken = DecodeToken(accessToken);

            // invalidate old tokens
            InvalidateTokens(refreshToken, accessToken);

            // set the same claims
            var refreshTokenClaims = new List<Claim>();
            var accessTokenClaims = new List<Claim>();

            foreach(var claim in decodedRefreshToken)
                refreshTokenClaims.Add(new Claim(claim.Key, claim.Value));
            foreach (var claim in decodedAccessToken)
                accessTokenClaims.Add(new Claim(claim.Key, claim.Value));

            return new RefreshTokensDTO(await CreateRefreshToken(refreshTokenClaims), CreateAccessToken(accessTokenClaims, deltaExpire));
        }

        /// <summary>
        /// Check if access token is in black list
        /// Return true if token still validate
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> CheckAccessTokenValidation(string token)
        {
            var decodedToken = DecodeToken(token);

            return await _accessTokensBlackList.GetStringAsync(decodedToken["userId"]) == null;
        }

        /// <summary>
        /// Check if refresh token isn't in white list
        /// Return true if token still validate
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> CheckRefreshTokenValidation(string token)
        {
            var decodedToken = DecodeToken(token);

            return await _refreshTokensWhiteList.GetStringAsync(decodedToken["userId"]) != null;
        }

        /// <summary>
        /// Add access token in black list and remove refresh token from white list
        /// </summary>
        /// <param name="token"></param>
        public async void InvalidateTokens(string refreshToken, string accessToken)
        {
            var decodedRefreshToken = DecodeToken(refreshToken);

            await _refreshTokensWhiteList.RemoveAsync(decodedRefreshToken["userId"]);
            await _accessTokensBlackList.SetStringAsync(decodedRefreshToken["userId"], accessToken, new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = Convert.ToDateTime(decodedRefreshToken["exp"])
            });
        }

        /// <summary>
        /// Decode token to claims
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Dictionary<string, string> DecodeToken(string token)
        {
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            return jwtToken.Claims.ToDictionary(claim => claim.Type, claim => claim.Value);
        }
    }
}
