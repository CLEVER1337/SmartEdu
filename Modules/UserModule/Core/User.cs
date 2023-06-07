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

        private string _name;
        private int _age;
        private DateTime _birthDay;
        private string _school;

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
