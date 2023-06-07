namespace SmartEdu.Modules.UserModule.Core
{
    public class UserData : BaseEntity
    {
        public UserData(string login, string salt, string hashedPassword)
        {
            _login = login;
            _salt = salt;
            _hashedPassword = hashedPassword;
        }

        private string _login = null!;
        private string _salt = null!;
        private string _hashedPassword = null!;

        public User? User { get; set; }

        public int UserId { get; set; }

        public string Login
        {
            get 
            { 
                return _login; 
            }
            set 
            { 
                _login = value;
            }
        }

        public string Salt
        {
            get
            {
                return _salt;
            }
            set 
            {
                _salt = value;
            }
        }

        public string HashedPassword 
        {
            get 
            { 
                return _hashedPassword; 
            }
            set
            {
                _hashedPassword = value;
            }
        }
    }
}
