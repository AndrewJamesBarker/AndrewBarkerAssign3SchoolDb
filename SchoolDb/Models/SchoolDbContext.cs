using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace SchoolDb.Models
{
    public class SchoolDbContext
    {
        private static string User { get { return "root"; } }

        private static string Password { get { return "root"; } }
        private static string Database { get { return "schooldb"; } }

        private static string Server { get { return "localhost"; } }

        private static string Port { get { return "3306"; } }

        protected static string ConnectionString
        {
            get
            {
                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password;
            }
        }
        /// <summary>
        /// returns a connection to the schooldb database
        /// </summary>
        /// <example>private SchoolDbContext SchoolDb = new SchoolDbContext();
        /// MySqlConnection Conn = SchoolDb.AccessDatabase();
        /// </example>
        /// <returns>A MySqlConnection Object</returns>
        public MySqlConnection AccessDatabase()
        {
            // we are instantiating the MySQLConnection Class to create an object
            // the object is a specific connection to our blog database on port 3306 of localhost
            return new MySqlConnection(ConnectionString);
        }
    }
}