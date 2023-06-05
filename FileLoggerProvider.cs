namespace SmartEdu
{
    public class FileLoggerProvider : ILoggerProvider
    {
        public FileLoggerProvider(string path)
        {
            _path = path;
        }

        private string _path;

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(_path);
        }

        public void Dispose() { }
    }
}
