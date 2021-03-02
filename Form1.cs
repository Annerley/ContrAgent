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

            TimeUpdater();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            


            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("INSERT INTO `conclusion` (`conclusion number`, `evaluation date`,`reason for rating`,`subject`," +
                "`specification`,`initiator`, `object`, `result`, `price`, `sad`) " +
                "VALUES (@conclusion_number, @evaluation_date, @reason_for_rating, @subject," +
                "@specification,  @initiator, @object, '' , @price, @sad)", db.getConnection());
            command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
            command.Parameters.Add("@evaluation_date", MySqlDbType.DateTime).Value = evaluationDateField.Text;
            command.Parameters.Add("@reason_for_rating", MySqlDbType.VarChar).Value = reasonField.Text;
            command.Parameters.Add("@subject", MySqlDbType.Text).Value = subjectField.Text;
            command.Parameters.Add("@specification", MySqlDbType.Text).Value = specificationField.Text;
            command.Parameters.Add("@initiator", MySqlDbType.VarChar).Value = initiatorField.Text;
            command.Parameters.Add("@object", MySqlDbType.Text).Value = objectField.Text;
            command.Parameters.Add("@price", MySqlDbType.Int32).Value = priceField.Text;
            command.Parameters.Add("@sad", MySqlDbType.VarChar).Value = sadField.Text;

            db.openConnection();
            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Добавилось");
            else
                MessageBox.Show("Не добавилось");

            db.closeConnection();


            //для select
            //adapter.Fill(table);
            


        }


        
        async void TimeUpdater()
        {
            while (true)
            {
                customTimer.Text = DateTime.Now.ToString();
                await Task.Delay(1000*60);
            }
        }

        
    }
}
