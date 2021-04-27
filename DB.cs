using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ContrAgent
{
    class DB
    {
        
       
        MySqlConnection connection = new MySqlConnection(getcon());

        private static string getcon()
        {
            return "server = " + getConfigPath(1) + "; port="+ getConfigPath(2)+";charset= utf8;username=monty;password=some_pass;database=po";
        }

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
        private static string getConfigPath(int j)
        {
            string line;
            string path = Directory.GetCurrentDirectory() + "\\config.txt";
            using (StreamReader sr = new StreamReader(path))
            {
                int i = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    if (i == j)
                    {
                        return line;
                    }

                    Console.WriteLine(line);
                    i++;
                }
            }
            return "default";

        }

    }

    
}
