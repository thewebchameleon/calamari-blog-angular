namespace CB.Common.Logging
{
    public class LoggingSettings
    {
        public SeqSettings Seq { get; set; }

        public RollingFileSettings RollingFile { get; set; }

        public ApplicationInsightsSettings ApplicationInsights { get; set; }

        public ConsoleSettings Console { get; set; }

        public string MinimumLogLevel { get; set; }
    }
}
