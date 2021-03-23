using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ContrAgent
{
    public partial class AuthorizationForm : Form
    {
        public AuthorizationForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            db.openConnection();
            

            MySqlCommand command = new MySqlCommand("SELECT password FROM users WHERE name = @name", db.getConnection());
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = loginField.Text;

            MySqlDataReader reader = command.ExecuteReader();

            string password = "\0";

            int counter = 0;
            while (reader.Read())
            {
                password = reader[0].ToString();
                counter++;
            }

            if(password == passwordField.Text)
            {
                
                MessageBox.Show("Успешная авторизация");
                Form1 Form1 = new Form1(loginField.Text);
                Form1.Show();

                
                this.Hide();

            }
            else if(counter == 0)
            {
                MessageBox.Show("Такого пользователя не существует");
            }
            else if(password!= passwordField.Text)
            {
                MessageBox.Show("Вы ввели неправильный пароль");
            }
            
            db.closeConnection();

        }

        
    }
}
