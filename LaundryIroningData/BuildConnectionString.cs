using LaundryIroningCommon;
using Microsoft.Extensions.Configuration;

namespace LaundryIroningData.Data
{
    public class BuildConnectionString : IBuildConnectionString
    {
        #region Variable and property declaration
        /// <summary>
        /// properties
        /// </summary>
        private IConfigurationRoot _configurationRoot;
        #endregion


        #region Constructor
        /// <summary>
        /// Initialize App settings
        /// </summary>
        /// <param name="configurationRoot"></param>
        public BuildConnectionString(IConfigurationRoot configurationRoot)
        {
            this._configurationRoot = configurationRoot;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Prepared connection string
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public string PreparedConnection()
        {
            string connectionString = _configurationRoot.GetConnectionString("SQLApiConnection");
            return connectionString;
        }

        #endregion
    }
}
