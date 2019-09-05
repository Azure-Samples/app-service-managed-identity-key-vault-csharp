using System;

namespace Mikv
{
    /// <summary>
    /// Assembly Versioning
    /// </summary>
    public class Version
    {
        static string version = string.Empty;

        public static string AssemblyVersion
        {
            get
            {
                if (string.IsNullOrEmpty(version))
                {
                    string file = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    DateTime dt = System.IO.File.GetCreationTime(file);

                    var aVer = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

                    version = string.Format("{0}.{1}.{2}", aVer.Major, aVer.Minor, dt.ToString("MMdd.HHmm"));
                }

                return version;
            }
        }
    }
}