using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace CardGameDemoDB
{
    public class Database
    {
        
        private const string connection = "SERVER=localhost;DATABASE=demodb;UID=root;PASSWORD=;";
        public MySqlConnection Connection = new(connection);

        public void Connect()
        {
            Connection.Open();
            Console.WriteLine(Connection.State == System.Data.ConnectionState.Open
                ? "Connection to database established"
                : "Connection to database failed");
        }

        public void Disconnect()
        {
            Connection.Close();
            Console.WriteLine(Connection.State == System.Data.ConnectionState.Closed
                ? "Connection to database closed"
                : "Connection to database failed");
        }



    }

}
