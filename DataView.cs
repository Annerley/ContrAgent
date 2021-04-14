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
    public partial class DataView : Form
    {
        string nameMain = "";
        public DataView(string name)
        {
            InitializeComponent();

            label53.Text = "Пользователь: " + name;
            nameMain = name;
            TimeUpdater();
            checkBox1.Checked = true;


        }

        async void TimeUpdater()
        {
            while (true)
            {
                customTimer.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                await Task.Delay(1000 * 60);
            }
        }

        private void DataView_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void DataView_Load(object sender, EventArgs e)
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
            dataGridView1.Columns[10].HeaderText = "ИНН";
            dataGridView1.Columns[11].HeaderText = "Наименование Контрагента";

        }

        private DataTable getConclusionList()
        {
            DataTable dtConclusion = new DataTable();

            DB db = new DB();

            db.openConnection();

            using (MySqlCommand cmd = new MySqlCommand("SELECT conclusion.conclusion_number, `evaluation date`, `reason for rating`, `subject`, `specification`," +
                "`initiator`, `object`, `result`, `price`, `sad`, main.inn, `name` FROM conclusion " +
                "INNER JOIN main ON main.conclusion_number = conclusion.conclusion_number " +
                "INNER JOIN organisation ON organisation.inn = main.inn WHERE conclusion.status = 1", db.getConnection()))
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
                    dataGridView1.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(160, 255, 160);
                }
                else if (dataGridView1.Rows[i].Cells[7].Value.ToString() == "Невозможно")
                {
                    dataGridView1.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(255, 96, 98);
                }
                else if (dataGridView1.Rows[i].Cells[7].Value.ToString() == "Возможно c ограничением")
                {
                    dataGridView1.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(255, 255, 191); ;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 Form1 = new Form1(nameMain, "", 2);
            Console.WriteLine(nameMain);
            Form1.ShowDialog();
            updateTable();
            
            
        }
        private void updateTable()
        {
            dataGridView1.DataSource = getConclusionList();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string number = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();

            DB db = new DB();

            db.openConnection();
            MySqlCommand cmd = new MySqlCommand("SELECT `status` FROM conclusion WHERE `conclusion_number` = @number", db.getConnection());
            cmd.Parameters.Add("@number", MySqlDbType.VarChar).Value = number;

            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            
            if(reader[0].ToString() == "0" )
            {
                Form1 Form1 = new Form1(nameMain, number, 0);
                Form1.ShowDialog();
                updateTable();
            }
            else
            {
                Form1 Form1 = new Form1(nameMain, number, 1);
                Form1.ShowDialog();
                updateTable();
            }

            
            
           
            db.closeConnection();
        }
        
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            
           if(checkBox2.Checked && !checkBox1.Checked)
           {
                DataTable dtConclusion = new DataTable();

                DB db = new DB();

                db.openConnection();

                using (MySqlCommand cmd = new MySqlCommand("SELECT conclusion.conclusion_number, `evaluation date`, `reason for rating`, `subject`, `specification`," +
                "`initiator`, `object`, `result`, `price`, `sad`, main.inn, `name` FROM conclusion " +
                "INNER JOIN main ON main.conclusion_number = conclusion.conclusion_number " +
                "INNER JOIN organisation ON organisation.inn = main.inn ", db.getConnection()))
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    dtConclusion.Load(reader);

                }

                db.closeConnection();
                dataGridView1.DataSource = dtConclusion;
           }
            else if(checkBox1.Checked && checkBox2.Checked)
            {
                DataTable dtConclusion = new DataTable();
                var letter = "";
                DB db = new DB();

                db.openConnection();
                MySqlCommand cmd2 = new MySqlCommand("SELECT `letter` FROM users WHERE name = @name", db.getConnection());
                cmd2.Parameters.Add("@name", MySqlDbType.VarChar).Value = nameMain;

                MySqlDataReader reader2 = cmd2.ExecuteReader();
                reader2.Read();
                letter = reader2[0].ToString();
                db.closeConnection();
                db.openConnection();
                //опять костыли

                using (MySqlCommand cmd = new MySqlCommand("SELECT conclusion.conclusion_number, `evaluation date`, `reason for rating`, `subject`, `specification`," +
                "`initiator`, `object`, `result`, `price`, `sad`, main.inn, `name` FROM conclusion " +
                "INNER JOIN main ON main.conclusion_number = conclusion.conclusion_number " +
                "INNER JOIN organisation ON organisation.inn = main.inn WHERE letter = @name", db.getConnection()))
                {
                    cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = letter;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    dtConclusion.Load(reader);

                }
                db.closeConnection();
                dataGridView1.DataSource = dtConclusion;
            }
            else if (checkBox1.Checked && !checkBox2.Checked)
            {

                var letter = "";

                DataTable dtConclusion = new DataTable();

                DB db = new DB();

                db.openConnection();
                MySqlCommand cmd2 = new MySqlCommand("SELECT `letter` FROM users WHERE name = @name", db.getConnection());
                cmd2.Parameters.Add("@name", MySqlDbType.VarChar).Value = nameMain;

                MySqlDataReader reader2 = cmd2.ExecuteReader();
                reader2.Read();
                letter = reader2[0].ToString();
                db.closeConnection();
                db.openConnection();
                //опять костыли

                using (MySqlCommand cmd = new MySqlCommand("SELECT conclusion.conclusion_number, `evaluation date`, `reason for rating`, `subject`, `specification`," +
                "`initiator`, `object`, `result`, `price`, `sad`, main.inn, `name` FROM conclusion " +
                "INNER JOIN main ON main.conclusion_number = conclusion.conclusion_number " +
                "INNER JOIN organisation ON organisation.inn = main.inn WHERE letter = @name AND status = 1", db.getConnection()))
                {
                    cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = letter;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    dtConclusion.Load(reader);

                }
                db.closeConnection();
                dataGridView1.DataSource = dtConclusion;
            }
            else
            {
                dataGridView1.DataSource = getConclusionList();
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var letter = "";
            if (checkBox1.Checked && !checkBox2.Checked)
            {

                

                DataTable dtConclusion = new DataTable();

                DB db = new DB();

                db.openConnection();
                MySqlCommand cmd2 = new MySqlCommand("SELECT `letter` FROM users WHERE name = @name", db.getConnection());
                cmd2.Parameters.Add("@name", MySqlDbType.VarChar).Value = nameMain;

                MySqlDataReader reader2 = cmd2.ExecuteReader();
                reader2.Read();
                letter = reader2[0].ToString();
                db.closeConnection();
                db.openConnection();
                //опять костыли

                using (MySqlCommand cmd = new MySqlCommand("SELECT conclusion.conclusion_number, `evaluation date`, `reason for rating`, `subject`, `specification`," +
                "`initiator`, `object`, `result`, `price`, `sad`, main.inn, `name` FROM conclusion " +
                "INNER JOIN main ON main.conclusion_number = conclusion.conclusion_number " +
                "INNER JOIN organisation ON organisation.inn = main.inn WHERE letter = @name AND status = 1", db.getConnection()))
                {
                    cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = letter;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    dtConclusion.Load(reader);

                }
                db.closeConnection();
                dataGridView1.DataSource = dtConclusion;
            }
            else if (checkBox2.Checked && !checkBox1.Checked)
            {
                DataTable dtConclusion = new DataTable();

                DB db = new DB();

                db.openConnection();

                using (MySqlCommand cmd = new MySqlCommand("SELECT conclusion.conclusion_number, `evaluation date`, `reason for rating`, `subject`, `specification`," +
                "`initiator`, `object`, `result`, `price`, `sad`, main.inn, `name` FROM conclusion " +
                "INNER JOIN main ON main.conclusion_number = conclusion.conclusion_number " +
                "INNER JOIN organisation ON organisation.inn = main.inn", db.getConnection()))
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    dtConclusion.Load(reader);

                }

                db.closeConnection();
                dataGridView1.DataSource = dtConclusion;
            }
            else if (checkBox1.Checked && checkBox2.Checked)
            {
                DataTable dtConclusion = new DataTable();

                DB db = new DB();

                db.openConnection();
                MySqlCommand cmd2 = new MySqlCommand("SELECT `letter` FROM users WHERE name = @name", db.getConnection());
                cmd2.Parameters.Add("@name", MySqlDbType.VarChar).Value = nameMain;

                MySqlDataReader reader2 = cmd2.ExecuteReader();
                reader2.Read();
                letter = reader2[0].ToString();
                db.closeConnection();
                db.openConnection();
                //опять костыли

                using (MySqlCommand cmd = new MySqlCommand("SELECT conclusion.conclusion_number, `evaluation date`, `reason for rating`, `subject`, `specification`," +
                "`initiator`, `object`, `result`, `price`, `sad`, main.inn, `name` FROM conclusion " +
                "INNER JOIN main ON main.conclusion_number = conclusion.conclusion_number " +
                "INNER JOIN organisation ON organisation.inn = main.inn WHERE letter = @name", db.getConnection()))
                {
                    cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = letter;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    dtConclusion.Load(reader);

                }
                db.closeConnection();
                dataGridView1.DataSource = dtConclusion;
            }
            else
            {
                dataGridView1.DataSource = getConclusionList();
            }
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.InitialDelay = 0;
            tt.SetToolTip(this.pictureBox1, "Создать новое заключение");
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.InitialDelay = 0;
            tt.SetToolTip(this.pictureBox2, "Открыть выбранное");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string number = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();

            DB db = new DB();

            db.openConnection();
            if(nameMain == "admin")
            {
                MySqlCommand cmd = new MySqlCommand("DELETE FROM conclusion WHERE `conclusion_number` = @number", db.getConnection());
                cmd.Parameters.Add("@number", MySqlDbType.VarChar).Value = number;
                if(cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Успешно удалено");
                }
            }
            else
            {
                MessageBox.Show("У вас нет прав администратора");
            }
            db.closeConnection();
            updateTable();
        }

       

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            string number = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }
    }



}
