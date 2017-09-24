using Microsoft.Extensions.Configuration;

namespace WC.CMS.SquidexClient
{
    public class SquidexConfiguration : ISquidexConfiguration
    {
        IConfiguration Base { get; set; }

        public SquidexConfiguration(IConfiguration root)
        {
            Base = root.GetSection("Squidex");
        }

        public string ApplicationName { get => Base["ApplicationName"]; }

        public string ServiceURL { get => Base["ServiceURL"]; }

        public string AuthServiceURL { get => Base.GetSection("Authenticator")["ServiceURL"]; }

        public string ClientID { get => Base.GetSection("Authenticator")["ClientID"]; }

        public string ClientSecret { get => Base.GetSection("Authenticator")["ClientSecret"]; }
    }
}