using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ContrAgent
{
    class DB
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;charset= utf8;username=root;password=root;database=po");

        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public MySqlConnection getConnection()
         {
            return connection;
        }
    }
}
