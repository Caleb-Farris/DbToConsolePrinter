using System.Data.Common;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DbToConsolePrinterUnitTESTS")]
namespace DbToConsolePrinter.ClassFiles
{
    class DbProvider
    {
        #region Properties
        public string ProviderName { get; set; }
        public DbProviderFactory Provider
        {
            get
            {
                return DbProviderFactories.GetFactory(ProviderName);
            }
        }
        #endregion


        public DbProvider()
        {
            ProviderName = DbConfigurationSettings.ProviderName;
        }

        public DbProvider(string providerName)
        {
            ProviderName = providerName;
        }
    }
}
