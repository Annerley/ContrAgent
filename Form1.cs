using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;

namespace ContrAgent
{
    public partial class Form1 : Form

    {
        double resultInt = 0;
        string result = "";
        public int statusMain = 1;
        public string nameMain = "";
        public Form1(string name, string number, int status)
        {
            InitializeComponent();
            Hide_Unnecessary();
            conclusionNumberField.Enabled = false;
            statusMain = status;
            nameMain = name;
            setupFormName();
            uploadData(name, number, status);

            
            resultUpdater();
            
            label13.Text += conclusionNumberField.Text;
            label51.Text = resultInt.ToString();
            //label51.Show();

            TextBox[] saveButton = new TextBox[16];
            TextBox[] saveButton2 = new TextBox[20];
            // добавляем кнопку в следующую свободную ячейку

            //tableLayoutPanel1.Controls.Add(saveButton);
            // добавляем кнопку в ячейку (2,2)
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    saveButton[i + j] = new TextBox();
                    saveButton[i+j].Dock = DockStyle.Fill;
                    saveButton[i + j].Font = new System.Drawing.Font("Calibri", 12);
                    saveButton[i + j].Multiline = true;
                    saveButton[i + j].Margin = new Padding(0, 0, 0, 0);
                    saveButton[i + j].Padding = new Padding(0, 0, 0, 0);
                    tableLayoutPanel1.Controls.Add(saveButton[i+j], i, j);
                }
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    saveButton2[i + j] = new TextBox();
                    saveButton2[i + j].Dock = DockStyle.Fill;
                    saveButton2[i + j].Font = new System.Drawing.Font("Calibri", 12);
                    saveButton2[i + j].Multiline = true;
                    saveButton2[i + j].Margin = new Padding(0, 0, 0, 0);
                    saveButton2[i + j].Padding = new Padding(0, 0, 0, 0);
                    tableLayoutPanel2.Controls.Add(saveButton2[i + j], i, j);
                }
            }

            loadTableData();


        }

        private void setupFormName()
        {
            if(statusMain == 0)
            {
                this.Text = "Просмотр сформированного заключения";
                label105.Text += "Сформировано";
            }
            else if(statusMain == 1)
            {
                this.Text = "Редактирование заключения";
                label105.Text += "Не сформировано";
            }
            else if(statusMain == 2)
            {
                this.Text = "Новое заключение";
                label105.Text += "Новое";
            }
            else if(statusMain == 4)
            {
                this.Text = "Новое заключение";
                label105.Text += "Новое";
            }
        }

        private void loadTableData()
        {
            DB db = new DB();
            db.openConnection();

            MySqlCommand command = new MySqlCommand("SELECT `name`, `date`, `expired`, `extra` FROM `expirience` WHERE `conclusion_number` = @conc", db.getConnection());
            command.Parameters.Add("@conc", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
            MySqlDataReader reader = command.ExecuteReader();
            int i = 0;
            while(reader.HasRows)
            {
                while (reader.Read())
                {
                    tableLayoutPanel1.GetControlFromPosition(0, i).Text = reader[0].ToString();
                    tableLayoutPanel1.GetControlFromPosition(1, i).Text = reader[1].ToString();
                    tableLayoutPanel1.GetControlFromPosition(2, i).Text = reader[2].ToString();
                    tableLayoutPanel1.GetControlFromPosition(3, i).Text = reader[3].ToString();
                    i++;
                }
                reader.NextResult();
                
            }
            reader.Close();

            MySqlCommand command2 = new MySqlCommand("SELECT `name`, `start`, `price`, `percent`, `subject` FROM `work` WHERE `conclusion_number` = @conc", db.getConnection());
            command2.Parameters.Add("@conc", MySqlDbType.VarChar).Value = conclusionNumberField.Text;

            reader = command2.ExecuteReader();

            i = 0;
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    tableLayoutPanel2.GetControlFromPosition(0, i).Text = reader[0].ToString();
                    tableLayoutPanel2.GetControlFromPosition(1, i).Text = reader[1].ToString();
                    tableLayoutPanel2.GetControlFromPosition(2, i).Text = reader[2].ToString();
                    tableLayoutPanel2.GetControlFromPosition(3, i).Text = reader[3].ToString();
                    tableLayoutPanel2.GetControlFromPosition(4, i).Text = reader[3].ToString();
                    i++;
                }
                reader.NextResult();

            }


        }
        private void blockEverything()
        {
            conclusionNumberField.ReadOnly = true;
            innField.ReadOnly = true;
            sadField.ReadOnly = true;
            evaluationDateField.Enabled = false;
            subjectField.ReadOnly = true;
            reasonField.Enabled = false;
            specificationField.ReadOnly = true;
            initiatorField.Enabled = false;
            objectField.Enabled = false;
            priceField.ReadOnly = true;
            expcheckBox.Enabled = false;
            extraField.ReadOnly = true;
            hideExtraField.ReadOnly = true;
            orgNameField.ReadOnly = true;
            factAdressField.ReadOnly = true;
            registrationDateField.Enabled = false;
            activityField.ReadOnly = true;
            legalAdressField.ReadOnly = true;
            emailField.ReadOnly = true;
            phoneField.ReadOnly = true;
            leaderField.ReadOnly = true;
            foundersField.ReadOnly = true;

            richTextBox1.ReadOnly = true; 
            richTextBox2.ReadOnly = true;
            richTextBox3.ReadOnly = true;
            richTextBox4.ReadOnly = true;
            richTextBox5.ReadOnly = true;
            richTextBox6.ReadOnly = true;
            richTextBox7.ReadOnly = true;
            richTextBox8.ReadOnly = true;
            richTextBox9.ReadOnly = true;
            richTextBox10.ReadOnly = true;
            richTextBox11.ReadOnly = true;
            richTextBox12.ReadOnly = true;
            richTextBox13.ReadOnly = true;
            richTextBox14.ReadOnly = true;
            richTextBox15.ReadOnly = true;
            richTextBox16.ReadOnly = true;
            richTextBox17.ReadOnly = true;
            richTextBox18.ReadOnly = true;
            richTextBox19.ReadOnly = true;
            richTextBox21.ReadOnly = true;
            richTextBox22.ReadOnly = true;
            richTextBox23.ReadOnly = true;
            richTextBox24.ReadOnly = true;
            richTextBox25.ReadOnly = true;
            richTextBox26.ReadOnly = true;
            richTextBox27.ReadOnly = true;
            richTextBox28.ReadOnly = true;
            richTextBox29.ReadOnly = true;
            richTextBox30.ReadOnly = true;
            richTextBox31.ReadOnly = true;

            //тоже самое для чекбоксов

        }
        private void uploadData(string name, string number, int status)
        {

            DB db = new DB();
            db.openConnection();
            
            if(statusMain == 1 || statusMain == 0 || statusMain == 4)
            {


                if (statusMain == 0)
                {
                    blockEverything();
                }


                MySqlCommand command = new MySqlCommand("SELECT * FROM conclusion WHERE `conclusion_number` = @conc", db.getConnection());
                command.Parameters.Add("@conc", MySqlDbType.VarChar).Value = number;
                //если нет мессадж бокс
                //подтянуть скоринг
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    conclusionNumberField.Text = reader[0].ToString();
                    evaluationDateField.Text = reader[1].ToString().Substring(0, reader[1].ToString().LastIndexOf(' '));
                    reasonField.Text = reader[2].ToString();
                    subjectField.Text = reader[3].ToString();
                    specificationField.Text = reader[4].ToString();
                    initiatorField.Text = reader[5].ToString();
                    objectField.Text = reader[6].ToString();
                    result = reader[7].ToString();
                    priceField.Text = reader[8].ToString();
                    sadField.Text = reader[9].ToString();
                    if(reader[12].ToString() =="Есть опыт договорных отношений")
                    {
                        expcheckBox.Checked = true;
                    }
                    extraField.Text = reader[13].ToString();
                    hideExtraField.Text = reader[14].ToString();
                    c1Field.Text = reader[15].ToString();
                    if(reader[16].ToString() == "0")
                    {
                        radioButton1.Checked = true;
                    }
                    else if (reader[16].ToString() == "0.5")
                    {
                        radioButton2.Checked = true;
                    }
                    else if(reader[16].ToString() == "1")
                    {
                        radioButton3.Checked = true;
                    }
                    if(reader[17].ToString() == "0")
                    {
                        increaseNdsField.Checked = true;
                    }
                    else if(reader[17].ToString() == "1")
                    {
                        decreaseNdsCheckBox.Checked = true;
                    }
                    else
                    {
                        zeroNdsField.Checked = true;
                    }
                    Console.WriteLine(reader[17].ToString());


                }
                db.closeConnection();
                db.openConnection();
                var value = "";
                MySqlCommand command2 = new MySqlCommand("SELECT inn FROM main WHERE `conclusion_number` = @number", db.getConnection());
                command2.Parameters.Add("@number", MySqlDbType.VarChar).Value = number;
                MySqlDataReader reader2 = command2.ExecuteReader();
                while(reader2.Read())
                {
                    innField.Text = reader2[0].ToString();
                }
                
                db.closeConnection();
                db.openConnection();

                //костыли с reader`ом поправь потом

                MySqlCommand command3 = new MySqlCommand("SELECT * FROM organisation WHERE `inn` = @inn", db.getConnection());
                command3.Parameters.Add("@inn", MySqlDbType.VarChar).Value = innField.Text;
                MySqlDataReader reader3 = command3.ExecuteReader();
                

                //очень костыли

                while (reader3.Read())
                {

                    innField.Text = reader3[0].ToString();
                    orgNameField.Text = reader3[1].ToString();
                    factAdressField.Text = reader3[2].ToString();
                    if(reader3[3].ToString()!= "")
                    {
                        registrationDateField.Text = reader3[3].ToString().Substring(0, reader3[3].ToString().LastIndexOf(' '));
                    }
                    activityField.Text = reader3[4].ToString();
                    legalAdressField.Text = reader3[5].ToString();
                    emailField.Text = reader3[6].ToString();
                    phoneField.Text = reader3[7].ToString();
                    leaderField.Text = reader3[8].ToString();
                    foundersField.Text = reader3[9].ToString();
                    gendirField.Text = reader3[10].ToString();

                }

                updateScoring(number);
                resultUpdater();

                //молоток
            }
            if(statusMain == 2 || statusMain == 4)
            {
                db.closeConnection();
                db.openConnection();
                string result = "";
                MySqlCommand command = new MySqlCommand("SELECT letter FROM users WHERE name = @name", db.getConnection());
                command.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
                MySqlDataReader reader = command.ExecuteReader();
                evaluationDateField.Text = DateTime.Now.ToShortDateString().ToString();
                registrationDateField.Text = DateTime.Now.ToShortDateString().ToString();
                while (reader.Read())
                {
                   result = reader[0].ToString()+"-";
                }
                db.closeConnection();
                db.openConnection();
                MySqlCommand command2 = new MySqlCommand("SELECT last FROM users WHERE name = @name", db.getConnection());
                command2.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
                MySqlDataReader reader2 = command2.ExecuteReader();

                while (reader2.Read())
                {
                    result = result + reader2[0].ToString();
                }

                conclusionNumberField.Text = result;
            }
            db.closeConnection();
            if(statusMain == 4)
            {
                statusMain = 2; 
            }
        }
        private void updateScoring(string number)
        {
            DB db = new DB();
            db.openConnection();

            MySqlCommand command = new MySqlCommand("SELECT point, comment FROM scoring WHERE `conclusion number` = @conc", db.getConnection());
            command.Parameters.Add("@conc", MySqlDbType.VarChar).Value = number;
            //если нет мессадж бокс
            //подтянуть скоринг
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                if(reader[0].ToString() == "1")
                {
                    
                    richTextBox1.Show();
                    checkBox2.Checked = true;
                    richTextBox1.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "2")
                {

                    richTextBox2.Show();
                    checkBox3.Checked = true;
                    richTextBox2.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "3")
                {

                    richTextBox3.Show();
                    checkBox4.Checked = true;
                    richTextBox3.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "4.1")
                {

                    richTextBox4.Show();
                    checkBox5.Checked = true;
                    richTextBox4.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "4.2")
                {

                    richTextBox4.Show();
                    checkBox6.Checked = true;
                    richTextBox4.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "5")
                {

                    richTextBox31.Show();
                    checkBox33.Checked = true;
                    richTextBox31.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "6")
                {

                    richTextBox5.Show();
                    checkBox7.Checked = true;
                    richTextBox5.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "7")
                {

                    richTextBox6.Show();
                    checkBox8.Checked = true;
                    richTextBox6.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "8")
                {

                    richTextBox7.Show();
                    checkBox9.Checked = true;
                    richTextBox7.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "9")
                {

                    richTextBox8.Show();
                    checkBox10.Checked = true;
                    richTextBox8.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "10")
                {

                    richTextBox9.Show();
                    checkBox11.Checked = true;
                    richTextBox9.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "11")
                {

                    richTextBox10.Show();
                    checkBox12.Checked = true;
                    richTextBox10.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "12")
                {

                    richTextBox11.Show();
                    checkBox13.Checked = true;
                    richTextBox11.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "13")
                {

                    richTextBox12.Show();
                    checkBox14.Checked = true;
                    richTextBox12.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "14")
                {

                    richTextBox13.Show();
                    checkBox15.Checked = true;
                    richTextBox13.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "15")
                {

                    richTextBox14.Show();
                    checkBox16.Checked = true;
                    richTextBox14.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "16")
                {

                    richTextBox15.Show();
                    checkBox17.Checked = true;
                    richTextBox15.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "17")
                {

                    richTextBox16.Show();
                    checkBox1.Checked = true;
                    richTextBox16.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "18")
                {

                    richTextBox17.Show();
                    checkBox18.Checked = true;
                    richTextBox17.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "19")
                {

                    richTextBox19.Show();
                    checkBox20.Checked = true;
                    richTextBox19.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "20.1")
                {

                    richTextBox21.Show();
                    checkBox19.Checked = true;
                    richTextBox18.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "20.2")
                {

                    richTextBox18.Show();
                    checkBox19.Checked = true;
                    richTextBox18.Text = reader[1].ToString();
                }
                
                else if (reader[0].ToString() == "21")
                {

                    richTextBox21.Show();
                    checkBox22.Checked = true;
                    richTextBox21.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "22")
                {

                    richTextBox22.Show();
                    checkBox23.Checked = true;
                    richTextBox22.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "23")
                {

                    richTextBox23.Show();
                    checkBox24.Checked = true;
                    richTextBox23.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "24")
                {

                    richTextBox24.Show();
                    checkBox25.Checked = true;
                    richTextBox24.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "25.1")
                {

                    richTextBox25.Show();
                    checkBox26.Checked = true;
                    richTextBox25.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "25.2")
                {

                    richTextBox25.Show();
                    checkBox27.Checked = true;
                    richTextBox25.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "26")
                {

                    richTextBox26.Show();
                    checkBox28.Checked = true;
                    richTextBox26.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "27")
                {

                    richTextBox27.Show();
                    checkBox29.Checked = true;
                    richTextBox27.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "28")
                {

                    richTextBox28.Show();
                    checkBox30.Checked = true;
                    richTextBox28.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "29")
                {

                    richTextBox29.Show();
                    checkBox31.Checked = true;
                    richTextBox29.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "30")
                {

                    richTextBox30.Show();
                    checkBox32.Checked = true;
                    richTextBox30.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "31")
                {

                    richTextBox20.Show();
                    checkBox34.Checked = true;
                    richTextBox20.Text = reader[1].ToString();
                }
            }
            db.closeConnection();
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
            richTextBox20.Hide();
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
            richTextBox31.Hide();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            if(innField.Text == "")
            {
                MessageBox.Show("Введите ИНН");
                return;
            }

            if (conclusionNumberField.Text == "")
            {
                MessageBox.Show("Введите номер заключения");
                return;
            }

            DB db = new DB();

            System.Data.DataTable table = new System.Data.DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            db.closeConnection();
            db.openConnection();
            // Если уже есть, обновить
            MySqlCommand command4 = new MySqlCommand("SELECT COUNT(*) FROM `conclusion` WHERE `conclusion_number` = @conc", db.getConnection());
            command4.Parameters.Add("@conc", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
            MySqlDataReader readercmd4 = command4.ExecuteReader();
            readercmd4.Read();

            if (readercmd4[0].ToString() == "0")
            {
                db.closeConnection();
                db.openConnection();
                MySqlCommand command = new MySqlCommand("INSERT INTO `conclusion` (`conclusion_number`, `evaluation date`,`reason for rating`,`subject`," +
                "`specification`,`initiator`, `object`, `result`, `price`, `sad`, `status`, `letter`, `exp`, `extra`, `hide extra`, `c1`, `extra_point`,`nds` ) " +
                "VALUES (@conclusion_number, @evaluation_date, @reason_for_rating, @subject," +
                "@specification,  @initiator, @object, @result , @price, @sad, @status, @letter, @exp, @extra, @hide, @c1, @point, @nds)", db.getConnection());

                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    //Console.WriteLine(evaluationDateField.Text);
                    command.Parameters.Add("@evaluation_date", MySqlDbType.Date).Value = getUsualDate(evaluationDateField.Text);

                    command.Parameters.Add("@reason_for_rating", MySqlDbType.VarChar).Value = reasonField.Text;
                    command.Parameters.Add("@subject", MySqlDbType.Text).Value = subjectField.Text;
                    command.Parameters.Add("@specification", MySqlDbType.Text).Value = specificationField.Text;
                    command.Parameters.Add("@initiator", MySqlDbType.VarChar).Value = initiatorField.Text;
                    command.Parameters.Add("@object", MySqlDbType.Text).Value = objectField.Text;
                    command.Parameters.Add("@c1", MySqlDbType.Text).Value = c1Field.Text;
                    command.Parameters.Add("@result", MySqlDbType.Text).Value = result;
                    if(decreaseNdsCheckBox.Checked)
                    {
                        command.Parameters.Add("@nds", MySqlDbType.Int32).Value = "1";
                    }
                    else if(increaseNdsField.Checked)
                    {
                        command.Parameters.Add("@nds", MySqlDbType.Int32).Value = "0";
                    }
                    else
                    {
                         command.Parameters.Add("@nds", MySqlDbType.Int32).Value = "2";
                    }
                    if (expcheckBox.Checked)
                    {
                        command.Parameters.Add("@exp", MySqlDbType.VarChar).Value = "Есть опыт договорных отношений";
                    }
                    else
                    {
                        command.Parameters.Add("@exp", MySqlDbType.VarChar).Value = "Не имеет опыт договорных отношений с ООО `МИП - СТРОЙ №1`";
                    }
                    command.Parameters.Add("@extra", MySqlDbType.Text).Value = extraField.Text;
                    command.Parameters.Add("@hide", MySqlDbType.Text).Value = hideExtraField.Text;
                    if(radioButton2.Checked)
                    {
                        command.Parameters.Add("@point", MySqlDbType.VarChar).Value = "0.5";
                    }
                    else if(radioButton3.Checked)
                    {
                        command.Parameters.Add("@point", MySqlDbType.VarChar).Value = "1";
                    }
                    else
                    {
                        command.Parameters.Add("@point", MySqlDbType.VarChar).Value = "0";
                    }

                db.openConnection();
                var letter = "";
                MySqlCommand cmd2 = new MySqlCommand("SELECT `letter` FROM users WHERE name = @name", db.getConnection());
                cmd2.Parameters.Add("@name", MySqlDbType.VarChar).Value = nameMain;

                MySqlDataReader reader2 = cmd2.ExecuteReader();
                reader2.Read();
                letter = reader2[0].ToString();

                db.closeConnection();
                db.openConnection();

                command.Parameters.Add("@letter", MySqlDbType.Text).Value = letter;
                if (priceField.Text == "")
                {
                    command.Parameters.Add("@price", MySqlDbType.VarChar).Value = "0";
                }
                else
                {
                    command.Parameters.Add("@price", MySqlDbType.VarChar).Value = priceField.Text;
                }
                command.Parameters.Add("@sad", MySqlDbType.VarChar).Value = sadField.Text;
                
                command.Parameters.Add("@status", MySqlDbType.Int32).Value = 1;
                
                MySqlCommand command2 = new MySqlCommand("INSERT INTO `main` (`inn`, `conclusion_number`) VALUES(@inn, @conclusion_number) ", db.getConnection());
                command2.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                command2.Parameters.Add("@inn", MySqlDbType.VarChar).Value = innField.Text;



                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM organisation WHERE inn = @inn", db.getConnection());
                cmd.Parameters.Add("@inn", MySqlDbType.VarChar).Value = innField.Text;

                MySqlDataReader readercmd = cmd.ExecuteReader();
                readercmd.Read();
                MySqlCommand command3 = new MySqlCommand();
                if ( readercmd[0].ToString() == "0")
                {
                    command3 = new MySqlCommand("INSERT INTO `organisation` (`inn`, `name`,`fact adress`,`registration date`," +
               "`activity`,`legal adress`, `email`, `phone`, `leader`, `founder`, `gendir`) " +
               "VALUES (@inn, @name, @fact_adress, @reg_date, " +
               "@activity,  @legal_adress, @email, @phone , @leader, @founder, @gendir)", db.getConnection());

                    command3.Parameters.Add("@name", MySqlDbType.VarChar).Value = orgNameField.Text;
                    command3.Parameters.Add("@reg_date", MySqlDbType.Date).Value = getUsualDate(registrationDateField.Text);
                    command3.Parameters.Add("@inn", MySqlDbType.VarChar).Value = innField.Text;
                    command3.Parameters.Add("@fact_adress", MySqlDbType.VarChar).Value = factAdressField.Text;
                    command3.Parameters.Add("@activity", MySqlDbType.VarChar).Value = activityField.Text;
                    command3.Parameters.Add("@legal_adress", MySqlDbType.VarChar).Value = legalAdressField.Text;
                    command3.Parameters.Add("@email", MySqlDbType.VarChar).Value = emailField.Text;
                    command3.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phoneField.Text;
                    command3.Parameters.Add("@leader", MySqlDbType.VarChar).Value = leaderField.Text;
                    command3.Parameters.Add("@founder", MySqlDbType.VarChar).Value = foundersField.Text;
                    command3.Parameters.Add("@gendir", MySqlDbType.VarChar).Value = gendirField.Text;
                }
                else
                {
                    command3 = new MySqlCommand("UPDATE `organisation` SET  `name` = @name,`fact adress` = @fact_adress,`registration date` = @reg_date," +
               "`activity` = @activity,`legal adress` = @legal_adress, `email` = @email, `phone` = @phone, `leader` = @leader, `founder` = @founder, `gendir` =@gendir " +
               "WHERE `inn` = @inn", db.getConnection());

                    command3.Parameters.Add("@name", MySqlDbType.VarChar).Value = orgNameField.Text;
                    command3.Parameters.Add("@reg_date", MySqlDbType.Date).Value = getUsualDate(registrationDateField.Text);
                    command3.Parameters.Add("@inn", MySqlDbType.VarChar).Value = innField.Text;
                    command3.Parameters.Add("@activity", MySqlDbType.VarChar).Value = activityField.Text;
                    command3.Parameters.Add("@legal_adress", MySqlDbType.VarChar).Value = legalAdressField.Text;
                    command3.Parameters.Add("@fact_adress", MySqlDbType.VarChar).Value = factAdressField.Text;
                    command3.Parameters.Add("@email", MySqlDbType.VarChar).Value = emailField.Text;
                    command3.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phoneField.Text;
                    command3.Parameters.Add("@leader", MySqlDbType.VarChar).Value = leaderField.Text;
                    command3.Parameters.Add("@founder", MySqlDbType.VarChar).Value = foundersField.Text;
                    command3.Parameters.Add("@gendir", MySqlDbType.VarChar).Value = gendirField.Text;
                }

                db.closeConnection();
                db.openConnection();






                

                db.openConnection();


                addScoringToDb(db);


                MySqlCommand command5 = new MySqlCommand("SELECT COUNT(*) FROM `conclusion` WHERE `conclusion_number` = @conc", db.getConnection());
                command5.Parameters.Add("@conc", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                MySqlDataReader readercmd5 = command5.ExecuteReader();
                readercmd5.Read();

                if (readercmd5[0].ToString() == "0")
                {
                    
                    command5 = new MySqlCommand("UPDATE `users` SET `last` = @last WHERE `name` = @name", db.getConnection());
                    int last = Convert.ToInt32(conclusionNumberField.Text.Remove(0, 2));
                    last++;
                    command5.Parameters.Add("@last", MySqlDbType.Int32).Value = last;
                    command5.Parameters.Add("@name", MySqlDbType.VarChar).Value = nameMain;
                    db.closeConnection();
                    db.openConnection();
                    if (command.ExecuteNonQuery() == 1 && command2.ExecuteNonQuery() == 1 && command3.ExecuteNonQuery() == 1 && command5.ExecuteNonQuery() == 1)
                        MessageBox.Show("Добавилось");
                    else
                        MessageBox.Show("Не добавилось");
                }
                else
                {
                    db.closeConnection();
                    db.openConnection();
                    if (command.ExecuteNonQuery() == 1 && command2.ExecuteNonQuery() == 1 && command3.ExecuteNonQuery() == 1)
                        MessageBox.Show("Добавилось");
                    else
                        MessageBox.Show("Не добавилось");
                }
                

                
            }
            else
            {
                MySqlCommand command = new MySqlCommand("UPDATE `conclusion` SET `evaluation date` = @evaluation_date, `reason for rating` = @reason_for_rating, " +
                    "`subject` = @subject, `specification` = @specification, `initiator` = @initiator, `object` = @object, `result` = @result, `price` = @price," +
                    " `sad` = @sad, `status` = @status, `exp` = @exp, `extra` =@extra, `hide extra`=@hide, `c1` = @c, `extra_point` = @point, `nds` = @nds WHERE `conclusion_number` = @number" , db.getConnection());


                //Console.WriteLine(evaluationDateField.Text);
                command.Parameters.Add("@number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                command.Parameters.Add("@c", MySqlDbType.Text).Value = c1Field.Text;
                command.Parameters.Add("@evaluation_date", MySqlDbType.Date).Value = getUsualDate(evaluationDateField.Text);
                if (decreaseNdsCheckBox.Checked)
                {
                    command.Parameters.Add("@nds", MySqlDbType.Int32).Value = "1";
                }
                else if (increaseNdsField.Checked)
                {
                    command.Parameters.Add("@nds", MySqlDbType.Int32).Value = "0";
                }
                else
                {
                    command.Parameters.Add("@nds", MySqlDbType.Int32).Value = "2";
                }
                command.Parameters.Add("@status", MySqlDbType.Int32).Value = 1;
                
                command.Parameters.Add("@reason_for_rating", MySqlDbType.VarChar).Value = reasonField.Text;
                command.Parameters.Add("@subject", MySqlDbType.Text).Value = subjectField.Text;
                command.Parameters.Add("@specification", MySqlDbType.Text).Value = specificationField.Text;
                command.Parameters.Add("@initiator", MySqlDbType.VarChar).Value = initiatorField.Text;
                command.Parameters.Add("@object", MySqlDbType.Text).Value = objectField.Text;
                command.Parameters.Add("@result", MySqlDbType.Text).Value = result;
                if (expcheckBox.Checked)
                {
                    command.Parameters.Add("@exp", MySqlDbType.VarChar).Value = "Есть опыт договорных отношений";
                }
                else
                {
                    command.Parameters.Add("@exp", MySqlDbType.VarChar).Value = "Нет опыта договорных отношений";
                }
                command.Parameters.Add("@extra", MySqlDbType.Text).Value = extraField.Text;
                command.Parameters.Add("@hide", MySqlDbType.Text).Value = hideExtraField.Text;
                if (priceField.Text == "")
                {
                    command.Parameters.Add("@price", MySqlDbType.VarChar).Value = 0;
                }
                else
                {
                    command.Parameters.Add("@price", MySqlDbType.VarChar).Value = priceField.Text;
                }
                command.Parameters.Add("@sad", MySqlDbType.VarChar).Value = sadField.Text;

                if (radioButton2.Checked)
                {
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = "0.5";
                }
                else if (radioButton3.Checked)
                {
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = "1";
                }
                else 
                {
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = "0";
                }
                    MySqlCommand command3 = new MySqlCommand("UPDATE `organisation` SET  `name` = @name,`fact adress` = @fact_adress,`registration date` = @reg_date," +
               "`activity` = @activity,`legal adress` = @legal_adress, `email` = @email, `phone` = @phone, `leader` = @leader, `founder` = @founder, `gendir` =@gendir " +
               "WHERE `inn` = @inn", db.getConnection());

                command3.Parameters.Add("@name", MySqlDbType.VarChar).Value = orgNameField.Text;
                command3.Parameters.Add("@reg_date", MySqlDbType.Date).Value = getUsualDate(registrationDateField.Text);
                command3.Parameters.Add("@inn", MySqlDbType.VarChar).Value = innField.Text;
                command3.Parameters.Add("@activity", MySqlDbType.VarChar).Value = activityField.Text;
                command3.Parameters.Add("@legal_adress", MySqlDbType.VarChar).Value = legalAdressField.Text;
                command3.Parameters.Add("@fact_adress", MySqlDbType.VarChar).Value = factAdressField.Text;
                command3.Parameters.Add("@email", MySqlDbType.VarChar).Value = emailField.Text;
                command3.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phoneField.Text;
                command3.Parameters.Add("@leader", MySqlDbType.VarChar).Value = leaderField.Text;
                command3.Parameters.Add("@founder", MySqlDbType.VarChar).Value = foundersField.Text;
                command3.Parameters.Add("@gendir", MySqlDbType.VarChar).Value = gendirField.Text;
                db.closeConnection();
                db.openConnection();

                if (command.ExecuteNonQuery() == 1 && command3.ExecuteNonQuery() == 1)
                    MessageBox.Show("Добавилось");
                else
                    MessageBox.Show("Не добавилось");

                db.closeConnection();
                
                addScoringToDb(db);

                
            }

            addTablesToDB();
            db.closeConnection();


            


        }

        private void addScoringToDb(DB db)
        {
            string cmd = "";
           
            if(conclusionNumberField.Text=="")
            {
                MessageBox.Show("Введите номер заключения");
            }
            else 
            {
               
                //допилить, не работает

               
                db.openConnection();

                MySqlCommand command4 = new MySqlCommand("SELECT COUNT(*) FROM `scoring` WHERE `conclusion number` = @conc", db.getConnection());
                command4.Parameters.Add("@conc", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                MySqlDataReader readercmd4 = command4.ExecuteReader();
                readercmd4.Read();

                if (readercmd4[0].ToString() != "0")
                {
                    db.closeConnection();
                    db.openConnection();
                    cmd = ("DELETE FROM `scoring` WHERE `conclusion number` = @conclusion_number ");
                    MySqlCommand commandScore = new MySqlCommand(cmd, db.getConnection());
                    commandScore.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;


                    commandScore.ExecuteNonQuery();
                }


                cmd = ("INSERT INTO `scoring` (`conclusion number`, `point`, `comment`) VALUES (@conclusion_number, @point, @comment) ");
                db.closeConnection();
                db.openConnection();
                if (checkBox2.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = 1;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox1.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox3.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = 2;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox2.Text;
                    command.ExecuteNonQuery();

                }
                if (checkBox4.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = 3;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox3.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox5.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = "4.1";
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox4.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox33.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = 5;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox31.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox6.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = "4.2";
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox4.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox7.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = 6;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox5.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox8.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = 7;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox6.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox9.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = 8;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox7.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox10.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = 9;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox8.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox11.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = 10;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox9.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox12.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = 11;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox10.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox13.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = 12;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox11.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox14.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = 13;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox12.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox15.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = 14;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox13.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox16.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = 15;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox14.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox17.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = 16;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox15.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox1.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = 17;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox16.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox18.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = 18;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox17.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox19.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = "20.2";
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox18.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox20.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = 19;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox19.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox21.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = "20.1";
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox18.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox22.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = 21;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox21.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox23.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = 22;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox22.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox24.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = 23;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox23.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox25.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = 24;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox24.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox26.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = "25.1";
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox25.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox27.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = "25.2";
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox25.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox28.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 26;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox26.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox29.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 27;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox27.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox30.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 28;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox28.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox31.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 29;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox29.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox32.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 30;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox30.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox34.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.VarChar).Value = "31";
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox20.Text;
                    command.ExecuteNonQuery();
                }

            }

            //MySqlCommand commandScore = new MySqlCommand(, db.getConnection());
            
            
            
        }

        private string getUsualDate(string date)
        {
            DateTime dateTime = DateTime.Now;
            try
            {
                dateTime = DateTime.Parse(date);
            }
            catch
            {
                MessageBox.Show("Неправильный формат даты");
                dateTime = DateTime.Now;
            }
            
            return dateTime.ToString("yyyy-MM-dd");
            //System.FormatException

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
            var name = orgNameField.Text;
            string exp = "";
            if (expcheckBox.Checked)
            {
                exp = "Имеет опыт договорных отношений";
            }
            else
                exp = "Не имеет опыт договорных отношений";

            var wordApp = new Word.Application();
            wordApp.Visible = false;

            string patternPath = Directory.GetCurrentDirectory() + "\\pattern.docx";
            var wordDocument = wordApp.Documents.Open(@patternPath);
            //ReplaceWordStub("{test}", test, wordDocument);
            ReplaceWordStub("{conclusion_number}", conclusionNumber, wordDocument);
            ReplaceWordStub("{initiator}", initiator, wordDocument);
            ReplaceWordStub("{evaluation_date}", evaluationDate, wordDocument);
            ReplaceWordStub("{name}", name, wordDocument);
            ReplaceWordStub("{sad_number}", sad_number, wordDocument);
            ReplaceWordStub("{object}", object_field, wordDocument);
            ReplaceWordStub("{inn}", inn, wordDocument);
            ReplaceWordStub("{reason}", reason, wordDocument);
            ReplaceWordStub("{subject}", subject, wordDocument);
            if(decreaseNdsCheckBox.Checked)
            {
                ReplaceWordStub("{price}", priceField.Text + " (Включая НДС " + (Double.Parse(priceField.Text) - Double.Parse(overallPriceField.Text)).ToString() + ")", wordDocument);
            }
            else if(increaseNdsField.Checked)
            {
                ReplaceWordStub("{price}", overallPriceField.Text +" (Включая НДС " + (Double.Parse(overallPriceField.Text) - Double.Parse(priceField.Text)).ToString() +")", wordDocument);
            }
            else
            {
                ReplaceWordStub("{price}", price + "(НДС не облагается)", wordDocument);
            }
            ReplaceWordStub("{extra}", extra, wordDocument);
            if (result == "Возможно")
            {
                ReplaceWordStub("{result}", "ВОЗМОЖНО", wordDocument);
            }
            else if (result == "Возможно c ограничением")
            {
                ReplaceWordStub("{result}", "ВОЗМОЖНО С ОГРАНИЧЕНИЕМ", wordDocument);
            }
            else if (result == "Невозможно")
            {
                ReplaceWordStub("{result}", "НЕВОЗМОЖНО", wordDocument);
            }
            ReplaceWordStub("{exp}", exp, wordDocument);

            DB db = new DB();
            db.openConnection();
            MySqlCommand command = new MySqlCommand("SELECT `fio`, `position` FROM users WHERE `name` = @user", db.getConnection());
            command.Parameters.Add("@user", MySqlDbType.Text).Value = nameMain;

            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            ReplaceWordStub("{fio}", reader[0].ToString(), wordDocument);
            ReplaceWordStub("{position}", reader[1].ToString(), wordDocument);
            db.closeConnection();
            addScoringToWord(conclusionNumber, wordDocument);
            //берем из конфига
            string path = getConfigPath(0)+"\\" + conclusionNumberField.Text;

            //string path =  + conclusionNumberField.Text;

            if (!Directory.Exists(@path))
            {
                Directory.CreateDirectory(@path);
            }
            string pdf = path + "\\" + conclusionNumberField.Text + ".pdf";
            string adress = path + "\\" + conclusionNumberField.Text + ".docx";

            object oMissing = System.Reflection.Missing.Value;
            wordDocument.SaveAs(@adress);
            //wordDocument.SaveAs(@pdf);
            wordDocument.ExportAsFixedFormat(pdf, (WdExportFormat)WdSaveFormat.wdFormatPDF, false, WdExportOptimizeFor.wdExportOptimizeForOnScreen,
                    WdExportRange.wdExportAllDocument, 1, 1, WdExportItem.wdExportDocumentContent, true, true,
                    WdExportCreateBookmarks.wdExportCreateHeadingBookmarks, true, true, false, ref oMissing);
            //wordApp.Visible = true;
            MessageBox.Show("Документ сохранен");
            wordDocument.Close();

        }
        private string getConfigPath(int j)
        {
            string line;
            string path = Directory.GetCurrentDirectory()+ "\\config.txt";
            using (StreamReader sr = new StreamReader(path))
            {
                int i = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    if(i == j)
                    {
                        return line;
                    }

                    Console.WriteLine(line);
                    i++;
                }
            }
            return "default";
            
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
                //result+= reader[0].ToString() + ". " + reader[1].ToString();
                result += reader[0].ToString() + ". " + getScoringText(reader[0].ToString());
                //пофиксить, кривой символ
                result += "^p";
            }
            Console.WriteLine(result);
            ReplaceWordStub("{scoring}", result, wordDocument);

        }

        private string getScoringText(string number)
        {
            string a = File.ReadAllText(Directory.GetCurrentDirectory() + "\\text\\" + number + ".txt");
            return a;
        }

        private void ReplaceWordStub(string stubToReplace, string text, Word.Document wordDocument)
        {
            if(stubToReplace == "{extra}")
            {
                testFunc(text,  wordDocument);
                return;
            }
            if (stubToReplace == "{scoring}")
            {
                testFuncForScor(text, wordDocument);
                return;
            }
            Word.Find find;
            find = wordDocument.Content.Application.Selection.Find;
            //app.Selection.Find;

            find.Text = stubToReplace; // текст поиска
            find.Replacement.Text = text; // текст замены

            find.Execute(FindText: Type.Missing, MatchCase: false, MatchWholeWord: false, MatchWildcards: false,
                        MatchSoundsLike: Type.Missing, MatchAllWordForms: false, Forward: true, Wrap: Word.WdFindWrap.wdFindContinue,
                        Format: true, ReplaceWith: Type.Missing, Replace: Word.WdReplace.wdReplaceAll);

            
            /*var range = wordDocument.Content;

            text = text.Replace("\n","^p");
            //Сбрасываем форматирование
            //range.Find.ClearFormatting();

            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text, Format: true);*/

        }
        private void testFuncForScor(string text, Word.Document wordDocument)
        {
            Word.Find find;
            find = wordDocument.Content.Application.Selection.Find;
            //app.Selection.Find;

            find.Text = "{scoring}"; // текст поиска
            string newstr = text;
            int i = 0;
            while (true)
            {
                if (text.Length < 255)
                {
                    newstr = text.Substring(0, text.Length);
                    newstr = newstr.Replace("\n", "^p");
                    find.Replacement.Text = newstr; // текст замены
                    find.Execute(FindText: Type.Missing, MatchCase: false, MatchWholeWord: false, MatchWildcards: false,
                        MatchSoundsLike: Type.Missing, MatchAllWordForms: false, Forward: true, Wrap: Word.WdFindWrap.wdFindContinue,
                        Format: false, ReplaceWith: Type.Missing, Replace: Word.WdReplace.wdReplaceAll);
                    break;
                }
                newstr = text.Substring(0, 246);
                newstr = newstr.Replace("\n", "^p");
                text = text.Remove(0, 246);
                find.Replacement.Text = newstr + "{scoring}"; // текст замены

                find.Execute(FindText: Type.Missing, MatchCase: false, MatchWholeWord: false, MatchWildcards: false,
                        MatchSoundsLike: Type.Missing, MatchAllWordForms: false, Forward: true, Wrap: Word.WdFindWrap.wdFindContinue,
                        Format: false, ReplaceWith: Type.Missing, Replace: Word.WdReplace.wdReplaceAll);

                i++;
            }




        }

        private void testFunc(string text, Word.Document wordDocument)
        {
            Word.Find find;
            find = wordDocument.Content.Application.Selection.Find;
            //app.Selection.Find;

            find.Text = "{extra}"; // текст поиска
            string newstr = text;
            int i = 0;
            while (true)
            {
                if(text.Length < 220)
                {
                    newstr = text.Substring(0, text.Length);
                    newstr = newstr.Replace("\n", "^p");
                    find.Replacement.Text = newstr; // текст замены
                    find.Execute(FindText: Type.Missing, MatchCase: false, MatchWholeWord: false, MatchWildcards: false,
                        MatchSoundsLike: Type.Missing, MatchAllWordForms: false, Forward: true, Wrap: Word.WdFindWrap.wdFindContinue,
                        Format: false, ReplaceWith: Type.Missing, Replace: Word.WdReplace.wdReplaceAll);
                    break;
                }
                newstr = text.Substring(0, 220);
                newstr = newstr.Replace("\n", "^p");
                text = text.Remove(0, 220);
                find.Replacement.Text = newstr + "{extra}"; // текст замены

                find.Execute(FindText: Type.Missing, MatchCase: false, MatchWholeWord: false, MatchWildcards: false,
                        MatchSoundsLike: Type.Missing, MatchAllWordForms: false, Forward: true, Wrap: Word.WdFindWrap.wdFindContinue,
                        Format: false, ReplaceWith: Type.Missing, Replace: Word.WdReplace.wdReplaceAll);

                i++;
            }
            

            
            
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
                resultInt += 0.5;
            }
            else
            {
                richTextBox12.Hide();
                resultInt -= 0.5;
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
                resultInt += 0.25;
            }
            else
            {
                richTextBox15.Hide();
                resultInt -= 0.25;
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
                resultInt += 0.15;
            }
            else
            {
                richTextBox29.Hide();
                resultInt -= 0.15;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox32_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox32.Checked)
            {
                richTextBox30.Show();
                resultInt += 0.2;
            }
            else
            {
                richTextBox30.Hide();
                resultInt -= 0.2;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void checkBox33_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox33.Checked)
            {
                richTextBox31.Show();
                resultInt += 1;
            }
            else
            {
                richTextBox31.Hide();
                resultInt -= 1;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }
        private void checkBox34_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox34.Checked)
            {
                richTextBox20.Show();
                resultInt += 0.15;
            }
            else
            {
                richTextBox20.Hide();
                resultInt -= 0.15;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var rtb = this.ActiveControl as RichTextBox;
            if (rtb != null)
            {
                rtb.SelectionFont = new System.Drawing.Font(rtb.Font, FontStyle.Bold);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var rtb = this.ActiveControl as RichTextBox;
            if (rtb != null)
            {
                rtb.SelectionFont = new System.Drawing.Font(rtb.Font, FontStyle.Regular);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            var rtb = this.ActiveControl as RichTextBox;
            if (rtb != null)
            {
                rtb.SelectionFont = new System.Drawing.Font(rtb.Font, FontStyle.Italic);
            }
        }

        

        

       

        

        private System.Data.DataTable getConclusionList()
        {
            System.Data.DataTable dtConclusion = new System.Data.DataTable();

            DB db = new DB();

            db.openConnection();

            using (MySqlCommand cmd = new MySqlCommand("SELECT `conclusion_number`, `evaluation date`, `reason for rating`, " +
                "`subject`, `specification`, `initiator`, `object`," +
                "`result`, `price`, `sad` FROM conclusion WHERE status = 0", db.getConnection()))
            {
                MySqlDataReader reader = cmd.ExecuteReader();

                dtConclusion.Load(reader);

                
            }

            

            db.closeConnection();
            return dtConclusion;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (innField.Text == "")
            {
                MessageBox.Show("Введите ИНН");
                return;
            }

            if (conclusionNumberField.Text == "")
            {
                MessageBox.Show("Введите номер заключения");
                return;
            }

            DB db = new DB();

            System.Data.DataTable table = new System.Data.DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();


            // Если уже есть, обновить
            if (statusMain == 2)
            {
                MySqlCommand command = new MySqlCommand("INSERT INTO `conclusion` (`conclusion_number`, `evaluation date`,`reason for rating`,`subject`," +
                "`specification`,`initiator`, `object`, `result`, `price`, `sad`, `status`, `letter`, `exp`, `extra`, `hide extra`, `c1`) " +
                "VALUES (@conclusion_number, @evaluation_date, @reason_for_rating, @subject," +
                "@specification,  @initiator, @object, @result , @price, @sad, @status, @letter, @exp, @extra, @hide, @c1)", db.getConnection());

                command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                //Console.WriteLine(evaluationDateField.Text);
                command.Parameters.Add("@evaluation_date", MySqlDbType.Date).Value = getUsualDate(evaluationDateField.Text);

                command.Parameters.Add("@reason_for_rating", MySqlDbType.VarChar).Value = reasonField.Text;
                command.Parameters.Add("@subject", MySqlDbType.Text).Value = subjectField.Text;
                command.Parameters.Add("@specification", MySqlDbType.Text).Value = specificationField.Text;
                command.Parameters.Add("@initiator", MySqlDbType.VarChar).Value = initiatorField.Text;
                command.Parameters.Add("@object", MySqlDbType.Text).Value = objectField.Text;
                command.Parameters.Add("@c1", MySqlDbType.Text).Value = c1Field.Text;
                command.Parameters.Add("@result", MySqlDbType.Text).Value = result;
                if (expcheckBox.Checked)
                {
                    command.Parameters.Add("@exp", MySqlDbType.VarChar).Value = "Есть опыт договорных отношений";
                }
                else
                {
                    command.Parameters.Add("@exp", MySqlDbType.VarChar).Value = "Нет опыта договорных отношений";
                }
                command.Parameters.Add("@extra", MySqlDbType.Text).Value = extraField.Text;
                command.Parameters.Add("@hide", MySqlDbType.Text).Value = hideExtraField.Text;
                db.openConnection();
                var letter = "";
                MySqlCommand cmd2 = new MySqlCommand("SELECT `letter` FROM users WHERE name = @name", db.getConnection());
                cmd2.Parameters.Add("@name", MySqlDbType.VarChar).Value = nameMain;

                MySqlDataReader reader2 = cmd2.ExecuteReader();
                reader2.Read();
                letter = reader2[0].ToString();

                db.closeConnection();
                db.openConnection();

                command.Parameters.Add("@letter", MySqlDbType.Text).Value = letter;
                if (priceField.Text == "")
                {
                    command.Parameters.Add("@price", MySqlDbType.VarChar).Value = "0";
                }
                else
                {
                    command.Parameters.Add("@price", MySqlDbType.VarChar).Value = priceField.Text;
                }
                command.Parameters.Add("@sad", MySqlDbType.VarChar).Value = sadField.Text;
                
                    command.Parameters.Add("@status", MySqlDbType.Int32).Value = 0;
                
                MySqlCommand command2 = new MySqlCommand("INSERT INTO `main` (`inn`, `conclusion_number`) VALUES(@inn, @conclusion_number) ", db.getConnection());
                command2.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                command2.Parameters.Add("@inn", MySqlDbType.VarChar).Value = innField.Text;


                MySqlCommand command3 = new MySqlCommand("INSERT INTO `organisation` (`inn`, `name`,`fact adress`,`registration date`," +
               "`activity`,`legal adress`, `email`, `phone`, `leader`, `founder`, `gendir`) " +
               "VALUES (@inn, @name, @fact_adress, @reg_date," +
               "@activity,  @legal_adress, @email, @phone , @leader, @founder, @gendir)", db.getConnection());

                command3.Parameters.Add("@name", MySqlDbType.VarChar).Value = orgNameField.Text;
                command3.Parameters.Add("@reg_date", MySqlDbType.Date).Value = getUsualDate(registrationDateField.Text);
                command3.Parameters.Add("@inn", MySqlDbType.VarChar).Value = innField.Text;
                command3.Parameters.Add("@fact_adress", MySqlDbType.VarChar).Value = factAdressField.Text;
                command3.Parameters.Add("@activity", MySqlDbType.VarChar).Value = activityField.Text;
                command3.Parameters.Add("@legal_adress", MySqlDbType.VarChar).Value = legalAdressField.Text;
                command3.Parameters.Add("@email", MySqlDbType.VarChar).Value = emailField.Text;
                command3.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phoneField.Text;
                command3.Parameters.Add("@leader", MySqlDbType.VarChar).Value = leaderField.Text;
                command3.Parameters.Add("@founder", MySqlDbType.VarChar).Value = foundersField.Text;
                command3.Parameters.Add("@gendir", MySqlDbType.VarChar).Value = gendirField.Text;

                db.openConnection();


                addScoringToDb(db);
                
                MySqlCommand command4 = new MySqlCommand("SELECT COUNT(*) FROM `main` WHERE conclusion_number = @conc", db.getConnection());
                command4.Parameters.Add("@conc", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                MySqlDataReader reader4 = command4.ExecuteReader();
                reader4.Read();
                if(reader4[0].ToString() == "0")
                {
                    MySqlCommand command5 = new MySqlCommand("SELECT `last` FROM `users` WHERE `name` = @name", db.getConnection());
                    db.closeConnection();
                    db.openConnection();
                    command5.Parameters.Add("@name", MySqlDbType.VarChar).Value = nameMain;
                    MySqlDataReader reader5 = command5.ExecuteReader();
                    reader5.Read();
                    int last = Int32.Parse(reader5[0].ToString());
                    last++;
                    reader5.Close();
                    MySqlCommand command6 = new MySqlCommand("UPDATE `users` SET `last` = @last WHERE `name` = @name", db.getConnection());
                    command6.Parameters.Add("@name", MySqlDbType.VarChar).Value = nameMain;
                    command6.Parameters.Add("@last", MySqlDbType.Int32).Value = last;
                    command6.ExecuteNonQuery();
                }
                /*
                
                */
                if (command.ExecuteNonQuery() == 1 && command2.ExecuteNonQuery() == 1 && command3.ExecuteNonQuery() == 1)
                    MessageBox.Show("Добавилось");
                else
                    MessageBox.Show("Не добавилось");
            }
            else
            {
                MySqlCommand command = new MySqlCommand("UPDATE `conclusion` SET `evaluation date` = @evaluation_date, `reason for rating` = @reason_for_rating, " +
                    "`subject` = @subject, `specification` = @specification, `initiator` = @initiator, `object` = @object, `result` = @result, `price` = @price," +
                    " `sad` = @sad, `status` = @status, `exp` = @exp, `extra` =@extra, `hide extra`=@hide, `c1` = @c WHERE `conclusion_number` = @number", db.getConnection());


                //Console.WriteLine(evaluationDateField.Text);
                command.Parameters.Add("@number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                command.Parameters.Add("@c", MySqlDbType.Text).Value = c1Field.Text;
                command.Parameters.Add("@evaluation_date", MySqlDbType.Date).Value = getUsualDate(evaluationDateField.Text);

                command.Parameters.Add("@status", MySqlDbType.Int32).Value = 0;
                
                
                command.Parameters.Add("@reason_for_rating", MySqlDbType.VarChar).Value = reasonField.Text;
                command.Parameters.Add("@subject", MySqlDbType.Text).Value = subjectField.Text;
                command.Parameters.Add("@specification", MySqlDbType.Text).Value = specificationField.Text;
                command.Parameters.Add("@initiator", MySqlDbType.VarChar).Value = initiatorField.Text;
                command.Parameters.Add("@object", MySqlDbType.Text).Value = objectField.Text;
                command.Parameters.Add("@result", MySqlDbType.Text).Value = result;
                if (expcheckBox.Checked)
                {
                    command.Parameters.Add("@exp", MySqlDbType.VarChar).Value = "Есть опыт договорных отношений";
                }
                else
                {
                    command.Parameters.Add("@exp", MySqlDbType.VarChar).Value = "Нет опыта договорных отношений";
                }
                command.Parameters.Add("@extra", MySqlDbType.Text).Value = extraField.Text;
                command.Parameters.Add("@hide", MySqlDbType.Text).Value = hideExtraField.Text;
                if (priceField.Text == "")
                {
                    command.Parameters.Add("@price", MySqlDbType.VarChar).Value = "0";
                }
                else
                {
                    command.Parameters.Add("@price", MySqlDbType.VarChar).Value = priceField.Text;
                }
                command.Parameters.Add("@sad", MySqlDbType.VarChar).Value = sadField.Text;


                MySqlCommand command3 = new MySqlCommand("UPDATE `organisation` SET  `name` = @name,`fact adress` = @fact_adress,`registration date` = @reg_date," +
               "`activity` = @activity,`legal adress` = @legal_adress, `email` = @email, `phone` = @phone, `leader` = @leader, `founder` = @founder, `gendir` =@gendir " +
               "WHERE `inn` = @inn", db.getConnection());

                command3.Parameters.Add("@name", MySqlDbType.VarChar).Value = orgNameField.Text;
                command3.Parameters.Add("@reg_date", MySqlDbType.Date).Value = getUsualDate(registrationDateField.Text);
                command3.Parameters.Add("@inn", MySqlDbType.VarChar).Value = innField.Text;
                command3.Parameters.Add("@activity", MySqlDbType.VarChar).Value = activityField.Text;
                command3.Parameters.Add("@legal_adress", MySqlDbType.VarChar).Value = legalAdressField.Text;
                command3.Parameters.Add("@fact_adress", MySqlDbType.VarChar).Value = factAdressField.Text;
                command3.Parameters.Add("@email", MySqlDbType.VarChar).Value = emailField.Text;
                command3.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phoneField.Text;
                command3.Parameters.Add("@leader", MySqlDbType.VarChar).Value = leaderField.Text;
                command3.Parameters.Add("@founder", MySqlDbType.VarChar).Value = foundersField.Text;
                command3.Parameters.Add("@gendir", MySqlDbType.VarChar).Value = gendirField.Text;

                db.openConnection();

                if (command.ExecuteNonQuery() == 1 && command3.ExecuteNonQuery() == 1)
                    MessageBox.Show("Добавилось");
                else
                    MessageBox.Show("Не добавилось");

                db.closeConnection();

                addScoringToDb(db);


            }


            db.closeConnection();



            this.Close();

        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            /*for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                String insertData = "INSERT INTO CostList(SupplierName, CostPrice, PartsID) VALUES (@SupplierName, @CostPrice, @PartsID)";
                MySqlCommand cmd = new cmd   (insertData, con);
                cmd.Parameters.AddWithValue("@SupplierName", dataGridView1.Rows[i].Cells[0].Value);
                cmd.Parameters.AddWithValue("@CostPrice", dataGridView1.Rows[i].Cells[1].Value);
                cmd.Parameters.AddWithValue("@PartsID", textBox1.Text);
                da.InsertCommand = cmd;
                cmd.ExecuteNonQuery();
            }*/
        }

       

       

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("dpi");
            /*Console.ReadLine();
            hideExtraField.Location = new System.Drawing.Point(extraField.Size.Width / 2 + extraField.Location.X, c1Field.Location.Y);
            hideExtraField.Size = new System.Drawing.Size(extraField.Size.Width / 2 , extraField.Size.Height / 2);
            c1Field.Size = new System.Drawing.Size(extraField.Size.Width / 2 - 40, extraField.Size.Height / 2);

            /*extraField.Text = "height:" + extraField.Size.Height + "width:" + extraField.Size.Width;
            hideExtraField.Text = "height:" + hideExtraField.Size.Height + "width:" + hideExtraField.Size.Width;
            c1Field.Text = "height:" + c1Field.Size.Height + "width:" + c1Field.Size.Width;*/

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*hideExtraField.Location = new System.Drawing.Point(extraField.Size.Width / 2 +extraField.Location.X, c1Field.Location.Y);
            hideExtraField.Size = new System.Drawing.Size(extraField.Size.Width / 2, extraField.Size.Height / 2);
            c1Field.Size = new System.Drawing.Size(extraField.Size.Width / 2 - 40, extraField.Size.Height / 2);

            /*extraField.Text = "height:" + extraField.Size.Height + "width:" + extraField.Size.Width;
            hideExtraField.Text = "height:" + hideExtraField.Size.Height + "width:" + hideExtraField.Size.Width;
            c1Field.Text = "height:" + c1Field.Size.Height + "width:" + c1Field.Size.Width;*/
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();
            addScoringToDb(db);
            MySqlCommand command = new MySqlCommand("SELECT `point`, `comment` FROM scoring WHERE `conclusion number` = @conc", db.getConnection());
            command.Parameters.Add("@conc", MySqlDbType.Text).Value = conclusionNumberField.Text;

            MySqlDataReader reader = command.ExecuteReader();
            var result = "";
            while (reader.Read())
            {
                result += "п." + reader[0].ToString() + ". " + reader[1].ToString();
                //пофиксить, кривой символ
                result += "\n";
            }
            extraField.Text = result;
        }

        private void label47_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.InitialDelay = 0;
            tt.AutoPopDelay = 5000000;
            tt.SetToolTip(this.label47, "Согласно данным из ИАС «Спарк» наличие у организации незавершенных исполнительных производств;"+"\n"+ " отражение в бухгалтерской или налоговой отчетности организации убытков на протяжении двух последних лет " + "\n" + "/ размер чистых активов имеет отрицательное значение за последний завершенный отчётный год;" + "\n" + " организация выступает в арбитражных судах только в роли ответчика;" + "\n" + " сведения в ЕГРЮЛ в отношении организации признаны недостоверными;" + "\n" + " организации, отсутствующие по юридическому адресу по данным ФНС России;" + "\n" + " в производстве арбитражного суда находится дело о признании должника (несостоятельным) банкротом;" + "\n" + " организация исключена из ЕГРЮЛ на основании п.2 ст.21.1 ФЗ от 08.08.2001" + "\n" + " №129-ФЗ - `юридическое лицо, которое в течение последних двенадцати месяцев не представляло документы отчетности," + "\n" + " предусмотренные законодательством РФ о налогах и сборах, и не осуществляло операций хотя бы по одному банковскому счету`");
        }

       
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton2.Checked)
            {
                resultInt+=0.5;
            }
            else
            {
                resultInt -= 0.5;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
            
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                resultInt += 1;
            }
            else
            {
                resultInt -= 1;
            }
            label51.Text = resultInt.ToString();
            resultUpdater();
        }

        private void label36_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.InitialDelay = 0;
            tt.AutoPopDelay = 5000000;
            tt.SetToolTip(this.label36, "Учредители/руководитель контрагента были аффилированы с юридическим лицом (банкротом)"+"\n"+"в период возбуждения производства о признании его несостоятельным (банкротом)");

        }

        private void addTablesToDB()
        {
            DB db = new DB();
            db.openConnection();
            MySqlCommand command = new MySqlCommand("DELETE  FROM expirience WHERE `conclusion_number` = @conc", db.getConnection());
            command.Parameters.Add("@conc", MySqlDbType.Text).Value = conclusionNumberField.Text;

            command.ExecuteNonQuery();
            

                db.closeConnection();
                db.openConnection();
                for (int i = 0; i < 4; i++)
                {
                    if (tableLayoutPanel1.GetControlFromPosition(0, i).Text != "")
                    {
                        MySqlCommand command2 = new MySqlCommand("INSERT INTO `expirience` (`conclusion_number`, `name`, `date`, `expired`, `extra`) VALUES(@conc, @name, @date, @expired, @extra) ", db.getConnection());
                        command2.Parameters.Add("@conc", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                        command2.Parameters.Add("@name", MySqlDbType.VarChar).Value = tableLayoutPanel1.GetControlFromPosition(0, i).Text;
                        command2.Parameters.Add("@date", MySqlDbType.VarChar).Value = tableLayoutPanel1.GetControlFromPosition(1, i).Text;
                        command2.Parameters.Add("@expired", MySqlDbType.VarChar).Value = tableLayoutPanel1.GetControlFromPosition(2, i).Text;
                        command2.Parameters.Add("@extra", MySqlDbType.VarChar).Value = tableLayoutPanel1.GetControlFromPosition(3, i).Text;
                        command2.ExecuteNonQuery();                    
                    }
                    else
                    {
                        break;
                    }
                }
                db.closeConnection();

            db.openConnection();
            MySqlCommand command3 = new MySqlCommand("DELETE  FROM work WHERE `conclusion_number` = @conc", db.getConnection());
            command3.Parameters.Add("@conc", MySqlDbType.Text).Value = conclusionNumberField.Text;

            command3.ExecuteNonQuery();


            
            for (int i = 0; i < 4; i++)
            {
                if (tableLayoutPanel2.GetControlFromPosition(0, i).Text != "")
                {
                    MySqlCommand command4 = new MySqlCommand("INSERT INTO `work` (`conclusion_number`, `name`, `start`, `price`, `percent`, `subject`) VALUES(@conc, @name, @start, " +
                        "@price, @percent, @subject) ", db.getConnection());
                    command4.Parameters.Add("@conc", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command4.Parameters.Add("@name", MySqlDbType.Text).Value = tableLayoutPanel2.GetControlFromPosition(0, i).Text;
                    command4.Parameters.Add("@start", MySqlDbType.Text).Value = tableLayoutPanel2.GetControlFromPosition(1, i).Text;
                    command4.Parameters.Add("@price", MySqlDbType.VarChar).Value = tableLayoutPanel2.GetControlFromPosition(2, i).Text;
                    command4.Parameters.Add("@percent", MySqlDbType.VarChar).Value = tableLayoutPanel2.GetControlFromPosition(3, i).Text;
                    command4.Parameters.Add("@subject", MySqlDbType.Text).Value = tableLayoutPanel2.GetControlFromPosition(4, i).Text;
                    command4.ExecuteNonQuery();
                }
                else
                {
                    break;
                }
            }
            db.closeConnection();

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void label67_Click(object sender, EventArgs e)
        {

        }

        private void label68_Click(object sender, EventArgs e)
        {

        }

        private void label44_Click(object sender, EventArgs e)
        {

        }

        private void label50_Click(object sender, EventArgs e)
        {

        }

        private void innField_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                DB db = new DB();

                db.openConnection();

                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM `organisation` WHERE inn = @inn", db.getConnection());
                cmd.Parameters.Add("@inn", MySqlDbType.VarChar).Value = innField.Text;
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if(reader[0].ToString()!= "0")
                {
                    const string message = "В базе данных есть организация с данным ИНН. Заполнить поля?";
                    const string caption = "Найдена организация";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        reader.Close();
                        
                        MySqlCommand cmd2 = new MySqlCommand("SELECT * FROM `organisation` WHERE inn = @inn", db.getConnection());
                        cmd2.Parameters.Add("@inn", MySqlDbType.VarChar).Value = innField.Text;
                        reader = cmd2.ExecuteReader();
                        while (reader.Read())
                        {
                            orgNameField.Text = reader[1].ToString();
                            factAdressField.Text = reader[2].ToString();
                            registrationDateField.Text = reader[3].ToString();
                            activityField.Text = reader[4].ToString();
                            legalAdressField.Text = reader[5].ToString();
                            emailField.Text = reader[6].ToString();
                            phoneField.Text = reader[7].ToString();
                            leaderField.Text = reader[8].ToString();
                            foundersField.Text = reader[9].ToString();
                            gendirField.Text = reader[10].ToString();
                        }
                    }


                }
                Console.WriteLine(innField.Text);
                innField.Text = innField.Text.Replace("\n", "");
            }
        }

        private void label105_Click(object sender, EventArgs e)
        {

        }

        private void decreaseNdsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(decreaseNdsCheckBox.Checked)
            {
                double x = Double.Parse(priceField.Text);
                overallPriceField.Text = (x - Math.Round((x / 1.20 - x) * (-1), 2)).ToString();
                increaseNdsField.Enabled = false;
                zeroNdsField.Enabled = false;
            }
            else
            {
                overallPriceField.Text = "";
                increaseNdsField.Enabled = true;
                zeroNdsField.Enabled = true;
            }
        }

        private void increaseNdsField_CheckedChanged(object sender, EventArgs e)
        {
            if (increaseNdsField.Checked)
            {
                double x = Double.Parse(priceField.Text);
                overallPriceField.Text = Math.Round(x*1.20, 2).ToString();
                decreaseNdsCheckBox.Enabled = false;
                zeroNdsField.Enabled = false;
            }
            else
            {
                overallPriceField.Text = "";
                decreaseNdsCheckBox.Enabled = true;
                zeroNdsField.Enabled = true;
            }
        }

        private void zeroNdsField_CheckedChanged(object sender, EventArgs e)
        {
            if (zeroNdsField.Checked)
            {
                overallPriceField.Text = priceField.Text;
                decreaseNdsCheckBox.Enabled = false;
                increaseNdsField.Enabled = false;
            }
            else
            {
                overallPriceField.Text = "";
                decreaseNdsCheckBox.Enabled = true;
                increaseNdsField.Enabled = true;
            }
        }
    }
}
