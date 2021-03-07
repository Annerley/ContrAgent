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
using Word = Microsoft.Office.Interop.Word;

namespace ContrAgent
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            TimeUpdater();
            Hide_Unnecessary();
            


            



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

            MySqlCommand command2 = new MySqlCommand("INSERT INTO `main` (`inn`, `conclusion number`) VALUES(@inn, @conclusion number) ", db.getConnection());
            command2.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
            command2.Parameters.Add("@inn", MySqlDbType.Int32).Value = innField.Text;


            // Если уже есть, обновить
            //MySqlCommand command3 = new MySqlCommand("INSERT INTO `organisation` (`inn`, `conclusion number`) VALUES(@inn, @conclusion number) ", db.getConnection());
            //MySqlCommand commandScore = new MySqlCommand(, db.getConnection());

            db.openConnection();

            addScoringToDb(db);

            if (command.ExecuteNonQuery() == 1 && command2.ExecuteNonQuery()==1)
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
                
                commandScore.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                commandScore.Parameters.Add("@point", MySqlDbType.Int32).Value = 1;
                commandScore.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox1.Text;
            }
            if (checkBox3.Checked)
            {
                
                commandScore.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                commandScore.Parameters.Add("@point", MySqlDbType.Int32).Value = 2;
                commandScore.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox2.Text;
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

        

      

      

        private void button4_Click(object sender, EventArgs e)
        {
            var conclusionNumber = conclusionNumberField.Text;
            var initiator = initiatorField.Text;
            var evaluationDate = evaluationDateField.Text;
            var sad_number = sadField.Text;
            var object_field = objectField.Text;
            var inn = innField.Text;
            var reason = reasonField.Text;
            var subject = subjectField.Text;
            var price = priceField.Text;

            var wordApp = new Word.Application();
            wordApp.Visible = false;

            var wordDocument = wordApp.Documents.Open(@"C:\Users\laput\source\repos\ContrAgent\pattern.docx");
            ReplaceWordStub("{conclusion_number}", conclusionNumber, wordDocument);
            ReplaceWordStub("{initiator}", initiator, wordDocument);
            ReplaceWordStub("{evaluation_date}", evaluationDate, wordDocument);
            ReplaceWordStub("{sad_number}", sad_number, wordDocument);
            ReplaceWordStub("{object}", object_field, wordDocument);
            ReplaceWordStub("{inn}", inn, wordDocument);
            ReplaceWordStub("{reason}", reason, wordDocument);
            ReplaceWordStub("{subject}", subject, wordDocument);
            ReplaceWordStub("{price}", price, wordDocument);

            wordDocument.SaveAs(@"C:\Users\laput\source\repos\ContrAgent\test2.doc");
            wordApp.Visible = true;

        }

        private void ReplaceWordStub(string stubToReplace, string text, Word.Document wordDocument)
        {
            var range = wordDocument.Content;
            //Сбрасываем форматирование
            range.Find.ClearFormatting();

            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text);

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                richTextBox1.Show();
            }
            else
            {
                richTextBox1.Hide();
            }
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

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox5.Checked || checkBox6.Checked)
            {
                richTextBox4.Show();
            }
            else if (!checkBox5.Checked && !checkBox6.Checked)
            {
                richTextBox4.Hide();
            }
        }
        

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked || checkBox6.Checked)
            {
                richTextBox4.Show();
            }
            else if (!checkBox5.Checked && !checkBox6.Checked)
            {
                richTextBox4.Hide();
            }
        }
        

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            
            if (checkBox7.Checked)
            {
                richTextBox5.Show();
            }
            else
            {
                richTextBox5.Hide();
            }
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox8.Checked)
            {
                richTextBox6.Show();
            }
            else
            {
                richTextBox6.Hide();
            }
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked)
            {
                richTextBox7.Show();
            }
            else
            {
                richTextBox7.Hide();
            }
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked)
            {
                richTextBox8.Show();
            }
            else
            {
                richTextBox8.Hide();
            }
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox11.Checked)
            {
                richTextBox9.Show();
            }
            else
            {
                richTextBox9.Hide();
            }
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox12.Checked)
            {
                richTextBox10.Show();
            }
            else
            {
                richTextBox10.Hide();
            }
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox13.Checked)
            {
                richTextBox11.Show();
            }
            else
            {
                richTextBox11.Hide();
            }
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox14.Checked)
            {
                richTextBox12.Show();
            }
            else
            {
                richTextBox12.Hide();
            }
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox15.Checked)
            {
                richTextBox13.Show();
            }
            else
            {
                richTextBox13.Hide();
            }
        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox16.Checked)
            {
                richTextBox14.Show();
            }
            else
            {
                richTextBox14.Hide();
            }
        }

        private void checkBox17_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox17.Checked)
            {
                richTextBox15.Show();
            }
            else
            {
                richTextBox15.Hide();
            }
        }

      
    }
}
