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
            statusMain = status;
            nameMain = name;
            uploadData(name, number, status);

            label53.Text = "Пользователь: " + name;
            resultUpdater();
            TimeUpdater();
            
            label51.Text = resultInt.ToString();

            

        }
        private void blockEverything()
        {
            conclusionNumberField.ReadOnly = true;
            innField.ReadOnly = true;
            sadField.ReadOnly = true;
            evaluationDateField.Enabled = false;
            subjectField.ReadOnly = true;


            richTextBox1.ReadOnly = true; ;
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

            //тоже самое для чекбоксов

        }
        private void uploadData(string name, string number, int status)
        {

            DB db = new DB();
            db.openConnection();
            
            if(statusMain == 1 || statusMain == 0)
            {


                if (statusMain == 0)
                {
                    blockEverything();
                }


                MySqlCommand command = new MySqlCommand("SELECT * FROM conclusion WHERE `conclusion number` = @conc", db.getConnection());
                command.Parameters.Add("@conc", MySqlDbType.VarChar).Value = number;
                //если нет мессадж бокс
                //подтянуть скоринг
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
                db.closeConnection();
                db.openConnection();
                var value = "";
                MySqlCommand command2 = new MySqlCommand("SELECT inn FROM main WHERE `conclusion number` = @number", db.getConnection());
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
                    registrationDateField.Text = reader3[3].ToString();
                    activityField.Text = reader3[4].ToString();
                    legalAdressField.Text = reader3[5].ToString();
                    emailField.Text = reader3[6].ToString();
                    phoneField.Text = reader3[7].ToString();
                    leaderField.Text = reader3[8].ToString();
                    foundersField.Text = reader3[9].ToString();

                }

                updateScoring(number);
                resultUpdater();

                //молоток
            }
            else if(statusMain == 2)
            {
                MySqlCommand command = new MySqlCommand("SELECT letter FROM users WHERE name = @name", db.getConnection());
                command.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    conclusionNumberField.Text = reader[0].ToString()+"-";
                }
            }
            db.closeConnection();
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
                else if (reader[0].ToString() == "4")
                {

                    richTextBox4.Show();
                    checkBox5.Checked = true;
                    richTextBox4.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "5")
                {

                    richTextBox4.Show();
                    checkBox6.Checked = true;
                    richTextBox4.Text = reader[1].ToString();
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
                else if (reader[0].ToString() == "20")
                {

                    richTextBox18.Show();
                    checkBox19.Checked = true;
                    richTextBox18.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "21")
                {

                    richTextBox18.Show();
                    checkBox21.Checked = true;
                    richTextBox18.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "22")
                {

                    richTextBox21.Show();
                    checkBox22.Checked = true;
                    richTextBox21.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "23")
                {

                    richTextBox22.Show();
                    checkBox23.Checked = true;
                    richTextBox22.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "24")
                {

                    richTextBox23.Show();
                    checkBox24.Checked = true;
                    richTextBox23.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "25")
                {

                    richTextBox24.Show();
                    checkBox25.Checked = true;
                    richTextBox24.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "26")
                {

                    richTextBox25.Show();
                    checkBox26.Checked = true;
                    richTextBox25.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "27")
                {

                    richTextBox25.Show();
                    checkBox27.Checked = true;
                    richTextBox25.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "28")
                {

                    richTextBox26.Show();
                    checkBox28.Checked = true;
                    richTextBox26.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "29")
                {

                    richTextBox27.Show();
                    checkBox29.Checked = true;
                    richTextBox27.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "30")
                {

                    richTextBox28.Show();
                    checkBox30.Checked = true;
                    richTextBox28.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "31")
                {

                    richTextBox29.Show();
                    checkBox31.Checked = true;
                    richTextBox29.Text = reader[1].ToString();
                }
                else if (reader[0].ToString() == "32")
                {

                    richTextBox30.Show();
                    checkBox32.Checked = true;
                    richTextBox30.Text = reader[1].ToString();
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
            if(statusMain == 2)
            {
                MySqlCommand command = new MySqlCommand("INSERT INTO `conclusion` (`conclusion number`, `evaluation date`,`reason for rating`,`subject`," +
                "`specification`,`initiator`, `object`, `result`, `price`, `sad`, `status`, `letter`) " +
                "VALUES (@conclusion_number, @evaluation_date, @reason_for_rating, @subject," +
                "@specification,  @initiator, @object, @result , @price, @sad, @status, @letter)", db.getConnection());

                command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                //Console.WriteLine(evaluationDateField.Text);
                command.Parameters.Add("@evaluation_date", MySqlDbType.Date).Value = evaluationDateField.Text;

                command.Parameters.Add("@reason_for_rating", MySqlDbType.VarChar).Value = reasonField.Text;
                command.Parameters.Add("@subject", MySqlDbType.Text).Value = subjectField.Text;
                command.Parameters.Add("@specification", MySqlDbType.Text).Value = specificationField.Text;
                command.Parameters.Add("@initiator", MySqlDbType.VarChar).Value = initiatorField.Text;
                command.Parameters.Add("@object", MySqlDbType.Text).Value = objectField.Text;
                command.Parameters.Add("@result", MySqlDbType.Text).Value = result;
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
                    command.Parameters.Add("@price", MySqlDbType.Int32).Value = 0;
                }
                else
                {
                    command.Parameters.Add("@price", MySqlDbType.Int32).Value = priceField.Text;
                }
                command.Parameters.Add("@sad", MySqlDbType.VarChar).Value = sadField.Text;
                if (hammerCheck.Checked)
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


                MySqlCommand command3 = new MySqlCommand("INSERT INTO `organisation` (`inn`, `name`,`fact adress`,`registration date`," +
               "`activity`,`legal adress`, `email`, `phone`, `leader`, `founder`) " +
               "VALUES (@inn, @name, @fact_adress, @reg_date," +
               "@activity,  @legal_adress, @email, @phone , @leader, @founder)", db.getConnection());

                command3.Parameters.Add("@name", MySqlDbType.VarChar).Value = orgNameField.Text;
                command3.Parameters.Add("@reg_date", MySqlDbType.Date).Value = registrationDateField.Text;
                command3.Parameters.Add("@inn", MySqlDbType.Int32).Value = innField.Text;
                command3.Parameters.Add("@fact_adress", MySqlDbType.VarChar).Value = factAdressField.Text;
                command3.Parameters.Add("@activity", MySqlDbType.VarChar).Value = activityField.Text;
                command3.Parameters.Add("@legal_adress", MySqlDbType.VarChar).Value = legalAdressField.Text;
                command3.Parameters.Add("@email", MySqlDbType.VarChar).Value = emailField.Text;
                command3.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phoneField.Text;
                command3.Parameters.Add("@leader", MySqlDbType.VarChar).Value = leaderField.Text;
                command3.Parameters.Add("@founder", MySqlDbType.VarChar).Value = foundersField.Text;

                db.openConnection();


                addScoringToDb(db);

                if (command.ExecuteNonQuery() == 1 && command2.ExecuteNonQuery() == 1 && command3.ExecuteNonQuery()==1)
                    MessageBox.Show("Добавилось");
                else
                    MessageBox.Show("Не добавилось");
            }
            else
            {
                MySqlCommand command = new MySqlCommand("UPDATE `conclusion` SET `evaluation date` = @evaluation_date, `reason for rating` = @reason_for_rating, " +
                    "`subject` = @subject, `specification` = @specification, `initiator` = @initiator, `object` = @object, `result` = @result, `price` = @price," +
                    " `sad` = @sad WHERE `conclusion number` = @number" , db.getConnection());


                //Console.WriteLine(evaluationDateField.Text);
                command.Parameters.Add("@number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                command.Parameters.Add("@evaluation_date", MySqlDbType.Date).Value = evaluationDateField.Text;

                command.Parameters.Add("@reason_for_rating", MySqlDbType.VarChar).Value = reasonField.Text;
                command.Parameters.Add("@subject", MySqlDbType.Text).Value = subjectField.Text;
                command.Parameters.Add("@specification", MySqlDbType.Text).Value = specificationField.Text;
                command.Parameters.Add("@initiator", MySqlDbType.VarChar).Value = initiatorField.Text;
                command.Parameters.Add("@object", MySqlDbType.Text).Value = objectField.Text;
                command.Parameters.Add("@result", MySqlDbType.Text).Value = result;
                if (priceField.Text == "")
                {
                    command.Parameters.Add("@price", MySqlDbType.Int32).Value = 0;
                }
                else
                {
                    command.Parameters.Add("@price", MySqlDbType.Int32).Value = priceField.Text;
                }
                command.Parameters.Add("@sad", MySqlDbType.VarChar).Value = sadField.Text;
                if (hammerCheck.Checked)
                {
                    command.Parameters.Add("@status", MySqlDbType.Int32).Value = 0;
                }
                else
                {
                    command.Parameters.Add("@status", MySqlDbType.Int32).Value = 1;
                }

                MySqlCommand command3 = new MySqlCommand("UPDATE `organisation` SET  `name` = @name,`fact adress` = @fact_adress,`registration date` = @reg_date," +
               "`activity` = @activity,`legal adress` = @legal_adress, `email` = @email, `phone` = @phone, `leader` = @leader, `founder` = @founder) " +
               "WHERE `inn` = @inn)", db.getConnection());

                command3.Parameters.Add("@name", MySqlDbType.VarChar).Value = orgNameField.Text;
                command3.Parameters.Add("@reg_date", MySqlDbType.Date).Value = registrationDateField.Text;
                command3.Parameters.Add("@inn", MySqlDbType.Int32).Value = innField.Text;
                command3.Parameters.Add("@activity", MySqlDbType.VarChar).Value = activityField.Text;
                command3.Parameters.Add("@legal_adress", MySqlDbType.VarChar).Value = legalAdressField.Text;
                command3.Parameters.Add("@email", MySqlDbType.VarChar).Value = emailField.Text;
                command3.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phoneField.Text;
                command3.Parameters.Add("@leader", MySqlDbType.VarChar).Value = leaderField.Text;
                command3.Parameters.Add("@founder", MySqlDbType.VarChar).Value = foundersField.Text;

                db.openConnection();

                if (command.ExecuteNonQuery() == 1 )
                    MessageBox.Show("Добавилось");
                else
                    MessageBox.Show("Не добавилось");

                db.closeConnection();
                
                addScoringToDb(db);

                
            }
            

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

                if(statusMain == 1)
                {
                    cmd = ("DELETE FROM `scoring` WHERE `conclusion number` = @conclusion_number ");
                    MySqlCommand commandScore = new MySqlCommand(cmd, db.getConnection());
                    commandScore.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;


                    commandScore.ExecuteNonQuery();
                }


                cmd = ("INSERT INTO `scoring` (`conclusion number`, `point`, `comment`) VALUES (@conclusion_number, @point, @comment) ");
                
                if (checkBox2.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 1;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox1.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox3.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 2;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox2.Text;
                    command.ExecuteNonQuery();

                }
                if (checkBox4.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 3;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox3.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox5.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 4;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox4.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox6.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 5;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox4.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox7.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 6;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox5.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox8.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 7;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox6.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox9.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 8;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox7.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox10.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 9;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox8.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox11.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 10;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox9.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox12.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 11;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox10.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox13.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 12;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox11.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox14.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 13;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox12.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox15.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 14;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox12.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox16.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 15;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox14.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox17.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 16;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox15.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox1.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 17;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox16.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox18.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 18;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox17.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox19.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 21;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox18.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox20.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 19;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox19.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox21.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 20;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox18.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox22.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 22;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox21.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox23.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 23;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox22.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox24.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 24;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox23.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox25.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 25;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox24.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox26.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 26;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox25.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox27.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 27;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox25.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox28.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 28;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox26.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox29.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 29;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox27.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox30.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 30;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox28.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox31.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 31;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox29.Text;
                    command.ExecuteNonQuery();
                }
                if (checkBox32.Checked)
                {
                    MySqlCommand command = new MySqlCommand(cmd, db.getConnection());
                    command.Parameters.Add("@conclusion_number", MySqlDbType.VarChar).Value = conclusionNumberField.Text;
                    command.Parameters.Add("@point", MySqlDbType.Int32).Value = 32;
                    command.Parameters.Add("@comment", MySqlDbType.Text).Value = richTextBox30.Text;
                    command.ExecuteNonQuery();
                }

            }

            //MySqlCommand commandScore = new MySqlCommand(, db.getConnection());
            
            
            
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
            var name = orgNameField.Text;

            var wordApp = new Word.Application();
            wordApp.Visible = false;

            string patternPath = Directory.GetCurrentDirectory() + "\\pattern.docx";
            var wordDocument = wordApp.Documents.Open(@patternPath);
            ReplaceWordStub("{conclusion_number}", conclusionNumber, wordDocument);
            ReplaceWordStub("{initiator}", initiator, wordDocument);
            ReplaceWordStub("{evaluation_date}", evaluationDate, wordDocument);
            ReplaceWordStub("{name}", name, wordDocument);
            ReplaceWordStub("{sad_number}", sad_number, wordDocument);
            ReplaceWordStub("{object}", object_field, wordDocument);
            ReplaceWordStub("{inn}", inn, wordDocument);
            ReplaceWordStub("{reason}", reason, wordDocument);
            ReplaceWordStub("{subject}", subject, wordDocument);
            ReplaceWordStub("{price}", price, wordDocument);
            ReplaceWordStub("{extra}", extra, wordDocument);
            ReplaceWordStub("{result}", result, wordDocument);

            addScoringToWord(conclusionNumber, wordDocument);
            //берем из конфига
            string path = "C:\\Users\\laput\\source\\repos\\Contr\\" + conclusionNumberField.Text;

            if (!Directory.Exists(@path))
            {
                Directory.CreateDirectory(@path);
            }

            string adress = path + "\\" + conclusionNumberField.Text + ".docx";
            wordDocument.SaveAs(@adress);
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
                //пофиксить, кривой символ
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

       
    }
}
