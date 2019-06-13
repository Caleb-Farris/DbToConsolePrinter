using System.Configuration;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DbToConsolePrinterUnitTESTS")]
namespace DbToConsolePrinter.ClassFiles
{
    static class DbConfigurationSettings
    {
        #region Properties
        public static string DefaultConnection
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["Default"].Name.ToString();
            }
        }

        public static string ProviderName
        {
            get
            {
                return ConfigurationManager.ConnectionStrings[DefaultConnection].ProviderName;
            }
        }
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings[DefaultConnection].ConnectionString;
            }
        }
        #endregion
    }
}
