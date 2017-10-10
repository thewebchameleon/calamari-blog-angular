namespace CB.Common.Configuration
{
    public class LoggingSettings
    {
        public SeqSettings Seq { get; set; }

        public RollingFileSettings RollingFile { get; set; }

        public string MinimumLogLevel { get; set; }
    }
}
