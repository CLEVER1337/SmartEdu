namespace SmartEdu
{
    public static class FileLoggerExtensions
    {
        public static ILoggingBuilder AddFile(this ILoggingBuilder builder, string path)
        {
            builder.AddProvider(new FileLoggerProvider(path));
            return builder;
        }
    }
}
