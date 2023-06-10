namespace SmartEdu.Modules.UserModule.Core
{
    public abstract class User : BaseEntity
    {
        public User(string login, string salt, string hashedPassword)
        {
            _userData = new UserData(login, salt, hashedPassword);
        }

        private UserData _userData;

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
    }
}
