using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace WonderfulGames.Model
{
    /// <summary>
    /// En basklass för alla dataåtkomstklasserna.
    /// </summary>
    public class DALBase
    {
        private static string _connectionString;

        /// <summary>
        /// Skapar och initierar ett anslutningsobjekt.
        /// </summary>
        /// <returns>En referens till ett SqlConnection-objekt.</returns>
        protected SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        /// <summary>
        /// Initierar anslutningssträngen.
        /// </summary>
        static DALBase()
        {
            // Hämtar anslutningssträngen från web.config.
            _connectionString = WebConfigurationManager.ConnectionStrings["WonderfulGames_ConnectionString"].ConnectionString;
        }
    }
}