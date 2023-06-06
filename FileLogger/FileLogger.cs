namespace SmartEdu.FileLogger
{
    public class FileLogger : ILogger, IDisposable
    {
        public FileLogger(string path)
        {
            _filePath = path;
        }

        private string _filePath;
        private static object _lock = new object();

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return this;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            lock (_lock)
            {
                File.AppendAllText(_filePath, logLevel.ToString() + " " + DateTime.Now.ToString() + " ");
                File.AppendAllText(_filePath, formatter(state, exception) + Environment.NewLine);
            }
        }

        public void Dispose() { }
    }
}
