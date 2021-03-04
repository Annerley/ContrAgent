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
            Hide_Unnecessary();


            this.tabPage3.Size = new Size(938, 1000);



        }
        private void Hide_Unnecessary()
        {
            richTextBox1.Hide();
            richTextBox2.Hide();
            richTextBox3.Hide();
            richTextBox4.Hide();
            richTextBox5.Hide();
            richTextBox6.Hide();
            richTextBox7.Hide();
            richTextBox8.Hide();
            richTextBox9.Hide();
            richTextBox10.Hide();
            richTextBox11.Hide();
            richTextBox12.Hide();
            richTextBox13.Hide();
            richTextBox14.Hide();
            richTextBox15.Hide();
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

            
            //MySqlCommand commandScore = new MySqlCommand(, db.getConnection());

            db.openConnection();

            addScoringToDb(db);

            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Добавилось");
            else
                MessageBox.Show("Не добавилось");

            db.closeConnection();


            //для select
            //adapter.Fill(table);
            


        }

        private void addScoringToDb(DB db)
        {
            
            MySqlCommand commandScore = new MySqlCommand("", db.getConnection());
            commandScore.CommandText = ("INSERT INTO `scoring` (`conclusion number`, `point`, `comment`) VALUES (@conclusion_number, @point, @comment)");
            if (checkBox2.Checked)
            {
                //Добавить номер заключения 
                
                commandScore.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                commandScore.Parameters.Add("@point", MySqlDbType.Int32).Value = 1;
                commandScore.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox1.Text;
            }
            //MySqlCommand commandScore = new MySqlCommand(, db.getConnection());

            if (commandScore.ExecuteNonQuery() == 1)
                MessageBox.Show("Добавилось");
            else
                MessageBox.Show("Не добавилось");
        }


        async void TimeUpdater()
        {
            while (true)
            {
                customTimer.Text = DateTime.Now.ToString();
                await Task.Delay(1000*60);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked)
            {
                richTextBox1.Show();
            }
            else
            {
                richTextBox1.Hide();
            }
        }

      

      

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void conclusionNumberField_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                richTextBox2.Show();
            }
            else
            {
                richTextBox2.Hide();
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                richTextBox3.Show();
            }
            else
            {
                richTextBox3.Hide();
            }
        }
    }
}
