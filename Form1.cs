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
        double resultInt = 0;
        string result = "";

        public Form1(string name)
        {
            InitializeComponent();
            label53.Text = "Пользователь: " + name;
            resultUpdater();
            TimeUpdater();
            Hide_Unnecessary();


            label51.Text = resultInt.ToString();

            






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
            richTextBox16.Hide();
            richTextBox17.Hide();
            richTextBox18.Hide();
            richTextBox19.Hide();
            richTextBox21.Hide();
            richTextBox22.Hide();
            richTextBox23.Hide();
            richTextBox24.Hide();
            richTextBox25.Hide();
            richTextBox26.Hide();
            richTextBox27.Hide();
            richTextBox28.Hide();
            richTextBox29.Hide();
            richTextBox30.Hide();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            


            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
           

            // Если уже есть, обновить
            MySqlCommand command = new MySqlCommand("INSERT INTO `conclusion` (`conclusion number`, `evaluation date`,`reason for rating`,`subject`," +
                "`specification`,`initiator`, `object`, `result`, `price`, `sad`, `status`) " +
                "VALUES (@conclusion_number, @evaluation_date, @reason_for_rating, @subject," +
                "@specification,  @initiator, @object, '' , @price, @sad, @status)", db.getConnection());
            command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
            //Console.WriteLine(evaluationDateField.Text);
            command.Parameters.Add("@evaluation_date", MySqlDbType.Date).Value = evaluationDateField.Text;
            
            command.Parameters.Add("@reason_for_rating", MySqlDbType.VarChar).Value = reasonField.Text;
            command.Parameters.Add("@subject", MySqlDbType.Text).Value = subjectField.Text;
            command.Parameters.Add("@specification", MySqlDbType.Text).Value = specificationField.Text;
            command.Parameters.Add("@initiator", MySqlDbType.VarChar).Value = initiatorField.Text;
            command.Parameters.Add("@object", MySqlDbType.Text).Value = objectField.Text;
            if(priceField.Text == "")
            {
                command.Parameters.Add("@price", MySqlDbType.Int32).Value = 0;
            }
            else
            {
                command.Parameters.Add("@price", MySqlDbType.Int32).Value = priceField.Text;
            }
            command.Parameters.Add("@sad", MySqlDbType.VarChar).Value = sadField.Text;
            if(hammerCheck.Checked)
            {
                command.Parameters.Add("@status", MySqlDbType.Int32).Value = 0;
            }
            else
            {
                command.Parameters.Add("@status", MySqlDbType.Int32).Value = 1;
            }
            MySqlCommand command2 = new MySqlCommand("INSERT INTO `main` (`inn`, `conclusion number`) VALUES(@inn, @conclusion_number) ", db.getConnection());
            command2.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
            command2.Parameters.Add("@inn", MySqlDbType.Int32).Value = innField.Text;




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
            string cmd = "";
            MySqlCommand commandScore = new MySqlCommand(cmd, db.getConnection());
            if(conclusionNumberField.Text=="")
            {
                MessageBox.Show("Введите номер заключения");
            }
            else
            {
                if (checkBox2.Checked)
                {
                    cmd = ("INSERT INTO `scoring` (`conclusion number`, `point`, `comment`) VALUES (@conclusion_number, @point, @comment)");
                    commandScore.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    commandScore.Parameters.Add("@point", MySqlDbType.Int32).Value = 1;
                    commandScore.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox1.Text;
                }
                if (checkBox3.Checked)
                {
                    cmd = ("INSERT INTO `scoring` (`conclusion number`, `point`, `comment`) VALUES (@conclusion_number, @point, @comment)");
                    commandScore.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    commandScore.Parameters.Add("@point", MySqlDbType.Int32).Value = 2;
                    commandScore.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox2.Text;
                }
            }

            //MySqlCommand commandScore = new MySqlCommand(, db.getConnection());
            
            if(cmd != "")
            {
                if (commandScore.ExecuteNonQuery() == 1)
                    MessageBox.Show("Добавилось");
                else
                    MessageBox.Show("Не добавилось");
            }
            
        }


        async void TimeUpdater()
        {
            while (true)
            {
                customTimer.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                await Task.Delay(1000*60);
            }
        }

        private void resultUpdater()
        {
            if(resultInt == 0 )
            {
                result = "Возможно";
                resultLabel.BackColor = Color.Green;
            }
            else if(resultInt > 0 && resultInt < 1 )
            {
                result = "Возможно c ограничением";
                resultLabel.BackColor = Color.Yellow;
            }
            else 
            {
                result = "Невозможно";
                resultLabel.BackColor = Color.Red;
            }
            resultLabel.Text = result;
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
            var extra = extraField.Text;

            var wordApp = new Word.Application();
            wordApp.Visible = false;

            var wordDocument = wordApp.Documents.Open(@"C:\учебка\agent\ContrAgent\pattern.docx");
            ReplaceWordStub("{conclusion_number}", conclusionNumber, wordDocument);
            ReplaceWordStub("{initiator}", initiator, wordDocument);
            ReplaceWordStub("{evaluation_date}", evaluationDate, wordDocument);
            ReplaceWordStub("{sad_number}", sad_number, wordDocument);
            ReplaceWordStub("{object}", object_field, wordDocument);
            ReplaceWordStub("{inn}", inn, wordDocument);
            ReplaceWordStub("{reason}", reason, wordDocument);
            ReplaceWordStub("{subject}", subject, wordDocument);
            ReplaceWordStub("{price}", price, wordDocument);
            ReplaceWordStub("{extra}", extra, wordDocument);
            ReplaceWordStub("{result}", result, wordDocument);

            addScoringToWord(conclusionNumber, wordDocument);

            wordDocument.SaveAs(@"C:\учебка\agent\ContrAgent\test2.rtf");
            wordApp.Visible = true;

        }

        private void addScoringToWord(string conclusionNumber, Word.Document wordDocument)
        {
            DB db = new DB();
            db.openConnection();
            addScoringToDb(db);
            MySqlCommand command = new MySqlCommand("SELECT `point`, `comment` FROM scoring WHERE `conclusion number` = @conc", db.getConnection());
            command.Parameters.Add("@conc", MySqlDbType.Text).Value = conclusionNumber;

            MySqlDataReader reader = command.ExecuteReader();
            var result = "";
            while (reader.Read())
            {
                result+= reader[0].ToString() + ". " + reader[1].ToString();
                result += "\r\n";
            }
            ReplaceWordStub("{scoring}", result, wordDocument);

        }

        private void ReplaceWordStub(string stubToReplace, string text, Word.Document wordDocument)
        {
            var range = wordDocument.Content;
            //Сбрасываем форматирование
            range.Find.ClearFormatting();

            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text, Format: true);

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                richTextBox1.Show();
                resultInt += 1;
            }
            else
            {
                richTextBox1.Hide();
                resultInt -= 1;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                richTextBox2.Show();
                resultInt += 1;
            }
            else
            {
                richTextBox2.Hide();
                resultInt -= 1;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                richTextBox3.Show();
                resultInt += 1;
            }
            
            else
            {
                richTextBox3.Hide();
                resultInt -= 1;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox5.Checked)
            {
                resultInt += 0.5;
                richTextBox4.Show();
            }
            else if (!checkBox5.Checked && !checkBox6.Checked)
            {
                richTextBox4.Hide();
                resultInt -= 0.5;
            }
            else
            {
                resultInt -= 0.5;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                resultInt += 1;
                richTextBox4.Show();
            }
            else if (!checkBox5.Checked && !checkBox6.Checked)
            {
                richTextBox4.Hide();
                resultInt -= 1;
            }
            else
            {
                resultInt -= 1;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            
            if (checkBox7.Checked)
            {
                richTextBox5.Show();
                resultInt += 1;
            }
            else
            {
                richTextBox5.Hide();
                resultInt -= 1;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox8.Checked)
            {
                richTextBox6.Show();
                resultInt += 1;
            }
            else
            {
                richTextBox6.Hide();
                resultInt -= 1;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked)
            {
                richTextBox7.Show();
                resultInt += 1;
            }
            else
            {
                richTextBox7.Hide();
                resultInt -= 1;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked)
            {
                richTextBox8.Show();
                resultInt += 1;
            }
            else
            {
                richTextBox8.Hide();
                resultInt -= 1;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox11.Checked)
            {
                richTextBox9.Show();
                resultInt += 1;
            }
            else
            {
                richTextBox9.Hide();
                resultInt -= 1;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox12.Checked)
            {
                richTextBox10.Show();
                resultInt += 1;
            }
            else
            {
                richTextBox10.Hide();
                resultInt -= 1;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox13.Checked)
            {
                richTextBox11.Show();
                resultInt += 1;
            }
            else
            {
                richTextBox11.Hide();
                resultInt -= 1;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox14.Checked)
            {
                richTextBox12.Show();
                resultInt += 1;
            }
            else
            {
                richTextBox12.Hide();
                resultInt -= 1;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox15.Checked)
            {
                richTextBox13.Show();
                resultInt += 0.5;
            }
            else
            {
                richTextBox13.Hide();
                resultInt -= 0.5;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox16.Checked)
            {
                richTextBox14.Show();
                resultInt += 0.5;
            }
            else
            {
                richTextBox14.Hide();
                resultInt -= 0.5;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox17_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox17.Checked)
            {
                richTextBox15.Show();
                resultInt += 0.5;
            }
            else
            {
                richTextBox15.Hide();
                resultInt -= 0.5;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                richTextBox16.Show();
                resultInt += 0.25;
            }
            else
            {
                richTextBox16.Hide();
                resultInt -= 0.25;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox18_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox18.Checked)
            {
                richTextBox17.Show();
                resultInt += 0.25;
            }
            else
            {
                richTextBox17.Hide();
                resultInt -= 0.25;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox20_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox20.Checked)
            {
                richTextBox19.Show();
                resultInt += 0.25;
            }
            else
            {
                richTextBox19.Hide();
                resultInt -= 0.25;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox19_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox19.Checked)
            {
                resultInt += 0.25;
                richTextBox18.Show();
            }
            else if (!checkBox19.Checked && !checkBox21.Checked)
            {
                richTextBox18.Hide();
                resultInt -= 0.25;
            }
            else
            {
                resultInt -= 0.25;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox21_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox21.Checked)
            {
                resultInt += 0.25;
                richTextBox18.Show();
            }
            else if (!checkBox21.Checked && !checkBox19.Checked)
            {
                richTextBox18.Hide();
                resultInt -= 0.25;
            }
            else
            {
                resultInt -= 0.25;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.Checked)
            {
                richTextBox21.Show();
                resultInt += 0.25;
            }
            else
            {
                richTextBox21.Hide();
                resultInt -= 0.25;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox23_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox23.Checked)
            {
                richTextBox22.Show();
                resultInt += 0.25;
            }
            else
            {
                richTextBox22.Hide();
                resultInt -= 0.25;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox24_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox24.Checked)
            {
                richTextBox23.Show();
                resultInt += 0.25;
            }
            else
            {
                richTextBox23.Hide();
                resultInt -= 0.25;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox25_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox25.Checked)
            {
                richTextBox24.Show();
                resultInt += 0.25;
            }
            else
            {
                richTextBox24.Hide();
                resultInt -= 0.25;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox26_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox26.Checked)
            {
                resultInt += 0.25;
                richTextBox25.Show();
            }
            else if (!checkBox26.Checked && !checkBox27.Checked)
            {
                richTextBox25.Hide();
                resultInt -= 0.25;
            }
            else
            {
                resultInt -= 0.25;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox27_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox27.Checked)
            {
                resultInt += 0.25;
                richTextBox25.Show();
            }
            else if (!checkBox26.Checked && !checkBox27.Checked)
            {
                richTextBox25.Hide();
                resultInt -= 0.25;
            }
            else
            {
                resultInt -= 0.25;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox28_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox28.Checked)
            {
                richTextBox26.Show();
                resultInt += 0.25;
            }
            else
            {
                richTextBox26.Hide();
                resultInt -= 0.25;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox29_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox29.Checked)
            {
                richTextBox27.Show();
                resultInt += 0.25;
            }
            else
            {
                richTextBox27.Hide();
                resultInt -= 0.25;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox30_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox30.Checked)
            {
                richTextBox28.Show();
                resultInt += 0.25;
            }
            else
            {
                richTextBox28.Hide();
                resultInt -= 0.25;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox31_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox31.Checked)
            {
                richTextBox29.Show();
                resultInt += 0.25;
            }
            else
            {
                richTextBox29.Hide();
                resultInt -= 0.25;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox32_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox32.Checked)
            {
                richTextBox30.Show();
                resultInt += 0.25;
            }
            else
            {
                richTextBox30.Hide();
                resultInt -= 0.25;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }





        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var rtb = this.ActiveControl as RichTextBox;
            if (rtb != null)
            {
                rtb.SelectionFont = new Font(rtb.Font, FontStyle.Bold);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var rtb = this.ActiveControl as RichTextBox;
            if (rtb != null)
            {
                rtb.SelectionFont = new Font(rtb.Font, FontStyle.Regular);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            var rtb = this.ActiveControl as RichTextBox;
            if (rtb != null)
            {
                rtb.SelectionFont = new Font(rtb.Font, FontStyle.Italic);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //стереть старое
            DB db = new DB();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            db.openConnection();

            List<Label> labels = new List<Label>();
            MySqlCommand command = new MySqlCommand("SELECT `conclusion number` FROM main WHERE inn = @inn", db.getConnection());
            command.Parameters.Add("@inn", MySqlDbType.Int32).Value = innSearchField.Text;

            MySqlDataReader reader = command.ExecuteReader();
            int i = 0;
            int x = 15;
            int y = 100;
            while (reader.Read())
            {
                
                labels.Add(new Label());
                tabPage6.Controls.Add(labels[i]);
                labels[i].Text = reader[0].ToString();
                labels[i].Location = new Point(x, y);
                labels[i].ForeColor = Color.Black;
                labels[i].Font = label51.Font;
                labels[i].AutoSize = true;
                labels[i].Show();
                
                y += 25;
                
                
                i++;
                
                
            }

            db.closeConnection();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            
            dataGridView1.DataSource = getConclusionList();
            dataGridView1.Columns[0].HeaderText = "Номер заключения";
            dataGridView1.Columns[1].HeaderText = "Дата оценки";
            dataGridView1.Columns[2].HeaderText = "Основание оценки";
            dataGridView1.Columns[3].HeaderText = "Предмет";
            dataGridView1.Columns[4].HeaderText = "Спецификация";
            dataGridView1.Columns[5].HeaderText = "Инициатор";
            dataGridView1.Columns[6].HeaderText = "Объект строительства";
            dataGridView1.Columns[7].HeaderText = "Установление договорных отношений";
            dataGridView1.Columns[8].HeaderText = "Цена";
            dataGridView1.Columns[9].HeaderText = "Номер СЭД";


            


        }

        

        private DataTable getConclusionList()
        {
            DataTable dtConclusion = new DataTable();

            DB db = new DB();

            db.openConnection();

            using (MySqlCommand cmd = new MySqlCommand("SELECT `conclusion number`, `evaluation date`, `reason for rating`, " +
                "`subject`, `specification`, `initiator`, `object`," +
                "`result`, `price`, `sad` FROM conclusion WHERE status = 0", db.getConnection()))
            {
                MySqlDataReader reader = cmd.ExecuteReader();

                dtConclusion.Load(reader);

                
            }

            

            db.closeConnection();
            return dtConclusion;
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[7].Value.ToString() == "Возможно")
                {
                    dataGridView1.Rows[i].Cells[7].Style.BackColor = Color.Green;
                }
                else if (dataGridView1.Rows[i].Cells[7].Value.ToString() == "Невозможно")
                {
                    dataGridView1.Rows[i].Cells[7].Style.BackColor = Color.Red;
                }
                else if (dataGridView1.Rows[i].Cells[7].Value.ToString() == "Возможно с ограничениями")
                {
                    dataGridView1.Rows[i].Cells[7].Style.BackColor = Color.Yellow;
                }
            }
            //dataGridView1.Rows[1].Cells[7].Style.BackColor = Color.Yellow;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var conc = conclusionSearchField.Text;
            

            DB db = new DB();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            db.openConnection();

            MySqlCommand command = new MySqlCommand("SELECT * FROM conclusion WHERE `conclusion number` = @conc", db.getConnection());
            command.Parameters.Add("@conc", MySqlDbType.VarChar).Value = conc;
            //если нет мессадж бокс
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                conclusionNumberField.Text = reader[0].ToString();
                evaluationDateField.Text = reader[1].ToString();
                reasonField.Text = reader[2].ToString();
                subjectField.Text = reader[3].ToString();
                specificationField.Text = reader[4].ToString();
                initiatorField.Text = reader[5].ToString();
                objectField.Text = reader[6].ToString();
                result = reader[7].ToString();
                priceField.Text = reader[8].ToString();
                sadField.Text = reader[9].ToString();
                

            }
            resultUpdater();
            db.closeConnection();
            //молоток
        }
    }
}
