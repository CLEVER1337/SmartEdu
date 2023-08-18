namespace SmartEdu.Modules.UserModule.Core
{
    public abstract class User : BaseEntity
    {
        public User() 
        {

        }

        public User(string login, string salt, string hashedPassword)
        {
            _userData = new UserData(login, salt, hashedPassword);
        }

        private UserData _userData = null!;

        // Here will be user's common data

        public UserData UserData
        {
            get
            {
                return _userData;
            }
            set
            {
                _userData = value;
            }
        }
        public int UserDataId { get; set; }

        public string Discriminator { get; set; } = null!;

        /// <summary>
        /// Save user in db
        /// </summary>
        /// <param name="user"></param>
        public async static Task Save(User user)
        {
            using (var context = new ApplicationContext())
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            }
        }
    }
}
