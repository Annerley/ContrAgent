using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContrAgent
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            


            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("INSERT INTO `books` (`name`, `year`) VALUES (@name, @year)", db.getConnection());
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = innField.Text;
            command.Parameters.Add("@year", MySqlDbType.Int32).Value = textBox2.Text;

            db.openConnection();
            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Добавилось");
            else
                MessageBox.Show("Не добавилось");

            db.closeConnection();

            
            //для select
            //adapter.Fill(table);


        }

        private void inn_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
