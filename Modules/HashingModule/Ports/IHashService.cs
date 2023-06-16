namespace SmartEdu.Modules.HashingModule.Ports
{
    public interface IHashService
    {
        string Salt { get; }
        string Hash { get; set; }

        void HashFunction(string data);

        void HashFunction(string data, string salt);
    }
}
