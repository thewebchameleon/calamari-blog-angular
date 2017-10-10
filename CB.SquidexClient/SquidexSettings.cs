namespace CB.CMS.SquidexClient
{
    public class SquidexSettings
    {
        public string ApplicationName { get; set; }

        public string ServiceURL { get; set; }

        public AuthenticatorSettings Authenticator { get; set; }

        public string WebhookSecret { get; set; }
    }
}