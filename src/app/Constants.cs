namespace Mikv
{
    /// <summary>
    /// String constants
    /// </summary>
    public class Constants
    {
        public static readonly string SwaggerVersion = "v " + Version.AssemblyVersion;
        public static readonly string SwaggerName = "mikv";
        public static readonly string SwaggerTitle = "Managed Identity - Key Vault WebAPI Sample";
        public static readonly string SwaggerPath = "/swagger/" + SwaggerName + "/swagger.json";
        public static readonly string XmlCommentsPath = SwaggerName + ".xml";

        public static readonly string KeyVaultName = "KeyVaultName";
        public static readonly string KeyVaultUrlError = "KeyVaultUrl not set";
        public static readonly string KeyVaultError = "Unable to read from Key Vault {0}";
        public static readonly string KeyVaultSecretName = "MySecret";

        public static readonly string AppInsightsKey = "AppInsightsKey";
        public static readonly string AppInsightsKeyError = "App Insights Key specified";

        public static readonly string Port = "Port";
        public static readonly string PortError = "Listen port is invalid: {0}";
        public static readonly string PortException = "Port not specified";

        public static readonly string HealthzResult = "Movies: 100\r\nActors: 553\r\nGenres: 20";
        public static readonly string HealthzError = "Healthz Failed:\r\n{0}";

        public static readonly int NotFound = 404;
        public static readonly int ServerError = 500;
    }
}
