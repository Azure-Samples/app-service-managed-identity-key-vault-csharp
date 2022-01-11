// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

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
        public static readonly string KeyVaultSecretName = "MySecret";

        public static readonly string AppInsightsKey = "AppInsightsKey";

        // if port is changed, also update value in the Dockerfiles
        public static readonly string Port = "4120";

        public static readonly int ServerError = (int)System.Net.HttpStatusCode.InternalServerError;
    }
}
