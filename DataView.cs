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
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using Microsoft.Office.Interop.Excel;

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


            //dataGridView1.Columns["Цена"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

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
            System.Windows.Forms.Application.Exit();
        }

        private void DataView_Load(object sender, EventArgs e)
        {
            loadDataCheckBox();

            
            dataGridView1.Columns[0].HeaderText = "Номер заключения";
            dataGridView1.Columns[1].HeaderText = "Дата оценки";
            dataGridView1.Columns[2].HeaderText = "Номер СЭД";
            dataGridView1.Columns[3].HeaderText = "ИНН";
            dataGridView1.Columns[4].HeaderText = "Наименование Контрагента";
            dataGridView1.Columns[5].HeaderText = "Основание оценки";
            dataGridView1.Columns[6].HeaderText = "Предмет";
            dataGridView1.Columns[7].HeaderText = "Спецификация";
            dataGridView1.Columns[8].HeaderText = "Инициатор";
            dataGridView1.Columns[9].HeaderText = "Объект строительства";
            dataGridView1.Columns[10].HeaderText = "Установление договорных отношений";
            dataGridView1.Columns[11].HeaderText = "Цена";
            dataGridView1.Columns[12].HeaderText = "Status";
            this.dataGridView1.Columns["Status"].Visible = false;
            this.dataGridView1.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private System.Data.DataTable getConclusionList()
        {
            System.Data.DataTable dtConclusion = new System.Data.DataTable();

            DB db = new DB();

            db.openConnection();

            using (MySqlCommand cmd = new MySqlCommand("SELECT conclusion.conclusion_number, `evaluation date`, `sad`, main.inn,`name`,`reason for rating`, `subject`, `specification`," +
                "`initiator`, `object`, `result`, `price`, `status` FROM conclusion " +
                "INNER JOIN main ON main.conclusion_number = conclusion.conclusion_number " +
                "INNER JOIN organisation ON organisation.inn = main.inn WHERE conclusion.status = 1 ORDER BY conclusion.conclusion_number DESC", db.getConnection()))
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
                if (dataGridView1.Rows[i].Cells[10].Value.ToString() == "Возможно")
                {
                    dataGridView1.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(160, 255, 160);
                    dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(160, 255, 160);
                }
                else if (dataGridView1.Rows[i].Cells[10].Value.ToString() == "Невозможно")
                {
                    dataGridView1.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(255, 96, 98);
                    dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(255, 96, 98);
                }
                else if (dataGridView1.Rows[i].Cells[10].Value.ToString() == "Возможно c ограничением")
                {
                    dataGridView1.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(255, 255, 191);
                    dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(255, 255, 191);
                }
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[12].Value.ToString() == "0")
                {
                    dataGridView1.Rows[i].Cells[0].Style.Font = new System.Drawing.Font(e.CellStyle.Font, FontStyle.Bold);
                }
                else
                {
                    dataGridView1.Rows[i].Cells[0].Style.Font = new System.Drawing.Font(e.CellStyle.Font, FontStyle.Regular);
                }

            }
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 Form1 = new Form1(nameMain, "", 2);
            Console.WriteLine(nameMain);
            Form1.ShowDialog();
            loadDataCheckBox();


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

            if (reader[0].ToString() == "0")
            {
                Form1 Form1 = new Form1(nameMain, number, 0);
                Form1.ShowDialog();
                loadDataCheckBox();
            }
            else
            {
                Form1 Form1 = new Form1(nameMain, number, 1);
                Form1.ShowDialog();
                loadDataCheckBox();
            }




            db.closeConnection();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox2.Checked && !checkBox1.Checked)
            {
                System.Data.DataTable dtConclusion = new System.Data.DataTable();

                DB db = new DB();

                db.openConnection();

                using (MySqlCommand cmd = new MySqlCommand("SELECT conclusion.conclusion_number, `evaluation date`, `sad`, main.inn,`name`,`reason for rating`, `subject`, `specification`," +
                "`initiator`, `object`, `result`, `price`, `status` FROM conclusion " +
                "INNER JOIN main ON main.conclusion_number = conclusion.conclusion_number " +
                "INNER JOIN organisation ON organisation.inn = main.inn ORDER BY conclusion.conclusion_number DESC", db.getConnection()))
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    dtConclusion.Load(reader);

                }

                db.closeConnection();
                dataGridView1.DataSource = dtConclusion;
            }
            else if (checkBox1.Checked && checkBox2.Checked)
            {
                System.Data.DataTable dtConclusion = new System.Data.DataTable();
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

                using (MySqlCommand cmd = new MySqlCommand("SELECT conclusion.conclusion_number, `evaluation date`, `sad`, main.inn,`name`,`reason for rating`, `subject`, `specification`," +
                "`initiator`, `object`, `result`, `price`, `status` FROM conclusion " +
                "INNER JOIN main ON main.conclusion_number = conclusion.conclusion_number " +
                "INNER JOIN organisation ON organisation.inn = main.inn WHERE letter = @name ORDER BY conclusion.conclusion_number DESC", db.getConnection()))
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

                System.Data.DataTable dtConclusion = new System.Data.DataTable();

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

                using (MySqlCommand cmd = new MySqlCommand("SELECT conclusion.conclusion_number, `evaluation date`, `sad`, main.inn,`name`,`reason for rating`, `subject`, `specification`," +
                "`initiator`, `object`, `result`, `price`, `status` FROM conclusion " +
                "INNER JOIN main ON main.conclusion_number = conclusion.conclusion_number " +
                "INNER JOIN organisation ON organisation.inn = main.inn WHERE letter = @name AND status = 1 ORDER BY conclusion.conclusion_number DESC", db.getConnection()))
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

            this.dataGridView1.Columns["Status"].Visible = false;
            dataGridView1.Refresh();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var letter = "";
            if (checkBox1.Checked && !checkBox2.Checked)
            {



                System.Data.DataTable dtConclusion = new System.Data.DataTable();

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

                using (MySqlCommand cmd = new MySqlCommand("SELECT conclusion.conclusion_number, `evaluation date`, `sad`, main.inn,`name`,`reason for rating`, `subject`, `specification`," +
                "`initiator`, `object`, `result`, `price`, `status` FROM conclusion " +
                "INNER JOIN main ON main.conclusion_number = conclusion.conclusion_number " +
                "INNER JOIN organisation ON organisation.inn = main.inn WHERE letter = @name AND status = 1 ORDER BY conclusion.conclusion_number DESC", db.getConnection()))
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
                System.Data.DataTable dtConclusion = new System.Data.DataTable();

                DB db = new DB();

                db.openConnection();

                using (MySqlCommand cmd = new MySqlCommand("SELECT conclusion.conclusion_number, `evaluation date`, `sad`, main.inn,`name`,`reason for rating`, `subject`, `specification`," +
                "`initiator`, `object`, `result`, `price`, `status` FROM conclusion " +
                "INNER JOIN main ON main.conclusion_number = conclusion.conclusion_number " +
                "INNER JOIN organisation ON organisation.inn = main.inn ORDER BY conclusion.conclusion_number DESC", db.getConnection()))
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    dtConclusion.Load(reader);

                }

                db.closeConnection();
                dataGridView1.DataSource = dtConclusion;
            }
            else if (checkBox1.Checked && checkBox2.Checked)
            {
                System.Data.DataTable dtConclusion = new System.Data.DataTable();

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

                using (MySqlCommand cmd = new MySqlCommand("SELECT conclusion.conclusion_number, `evaluation date`, `sad`, main.inn,`name`,`reason for rating`, `subject`, `specification`," +
                "`initiator`, `object`, `result`, `price`, `status` FROM conclusion " +
                "INNER JOIN main ON main.conclusion_number = conclusion.conclusion_number " +
                "INNER JOIN organisation ON organisation.inn = main.inn WHERE letter = @name ORDER BY conclusion.conclusion_number DESC", db.getConnection()))
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

            this.dataGridView1.Columns["Status"].Visible = false;
            dataGridView1.Refresh();
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
            tt.SetToolTip(this.pictureBox2, "Редактировать");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string number = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();

            DB db = new DB();

            db.openConnection();
            if (nameMain == "admin")
            {
                MySqlCommand cmd = new MySqlCommand("DELETE FROM conclusion WHERE `conclusion_number` = @number", db.getConnection());
                cmd.Parameters.Add("@number", MySqlDbType.VarChar).Value = number;
                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Успешно удалено");
                }
            }
            else
            {
                MessageBox.Show("У вас нет прав администратора");
            }
            db.closeConnection();
            loadDataCheckBox();
        }



        private void pictureBox6_Click(object sender, EventArgs e)
        {
            string number = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();


        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            string number = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();

            DB db = new DB();

            db.openConnection();

            Form1 Form1 = new Form1(nameMain, number, 4);
            Form1.ShowDialog();
            loadDataCheckBox();





            db.closeConnection();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string a = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                if (Directory.Exists(getConfigPath(0) + "\\" + a))
                {
                    Process.Start(getConfigPath(0) + "\\" + a);
                }
                else
                {
                    MessageBox.Show("Директории не существует, создайте документы заключения");
                }
            }
            catch (ArgumentOutOfRangeException)
            {

                
            }
            
            


        }

        private string getConfigPath(int j)
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

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            
        }



        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if(textBox1.Text == "")
                {
                    loadDataCheckBox();
                    return;
                }
                System.Data.DataTable dtConclusion = new System.Data.DataTable();

                DB db = new DB();

                db.openConnection();

                using (MySqlCommand cmd = new MySqlCommand("SELECT conclusion.conclusion_number, `evaluation date`, `sad`, main.inn,`name`,`reason for rating`, `subject`, `specification`," +
                "`initiator`, `object`, `result`, `price`, `status` FROM conclusion " +
                "INNER JOIN main ON main.conclusion_number = conclusion.conclusion_number " +
                "INNER JOIN organisation ON organisation.inn = main.inn WHERE conclusion.conclusion_number LIKE @number ", db.getConnection()))
                {
                    cmd.Parameters.Add("@number", MySqlDbType.VarChar).Value = "%"+textBox1.Text+"%";
                    MySqlDataReader reader = cmd.ExecuteReader();
                    dtConclusion.Load(reader);

                }

                db.closeConnection();
                dataGridView1.DataSource = dtConclusion;
                this.dataGridView1.Columns["Status"].Visible = false;
                dataGridView1.Refresh();
            }
        }

        private void loadDataCheckBox()
        {
            if(checkBox1.Checked && !checkBox2.Checked)
            {
                System.Data.DataTable dtConclusion = new System.Data.DataTable();

                DB db = new DB();

                db.openConnection();
                MySqlCommand cmd2 = new MySqlCommand("SELECT `letter` FROM users WHERE name = @name", db.getConnection());
                cmd2.Parameters.Add("@name", MySqlDbType.VarChar).Value = nameMain;

                MySqlDataReader reader2 = cmd2.ExecuteReader();
                reader2.Read();
                var letter = reader2[0].ToString();
                db.closeConnection();
                db.openConnection();
                //опять костыли

                using (MySqlCommand cmd = new MySqlCommand("SELECT conclusion.conclusion_number, `evaluation date`, `sad`, main.inn,`name`,`reason for rating`, `subject`, `specification`," +
                "`initiator`, `object`, `result`, `price`, `status` FROM conclusion " +
                "INNER JOIN main ON main.conclusion_number = conclusion.conclusion_number " +
                "INNER JOIN organisation ON organisation.inn = main.inn WHERE letter = @name AND `status` = 1 ORDER BY conclusion.conclusion_number DESC", db.getConnection()))
                {
                    cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = letter;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    dtConclusion.Load(reader);

                }
                db.closeConnection();
                dataGridView1.DataSource = dtConclusion;
            }
            else if(checkBox1.Checked && checkBox2.Checked)
            {
                System.Data.DataTable dtConclusion = new System.Data.DataTable();

                DB db = new DB();

                db.openConnection();
                MySqlCommand cmd2 = new MySqlCommand("SELECT `letter` FROM users WHERE name = @name", db.getConnection());
                cmd2.Parameters.Add("@name", MySqlDbType.VarChar).Value = nameMain;

                MySqlDataReader reader2 = cmd2.ExecuteReader();
                reader2.Read();
                var letter = reader2[0].ToString();
                db.closeConnection();
                db.openConnection();
                //опять костыли

                using (MySqlCommand cmd = new MySqlCommand("SELECT conclusion.conclusion_number, `evaluation date`, `sad`, main.inn,`name`,`reason for rating`, `subject`, `specification`," +
                "`initiator`, `object`, `result`, `price`, `status` FROM conclusion " +
                "INNER JOIN main ON main.conclusion_number = conclusion.conclusion_number " +
                "INNER JOIN organisation ON organisation.inn = main.inn WHERE letter = @name ORDER BY conclusion.conclusion_number DESC", db.getConnection()))
                {
                    cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = letter;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    dtConclusion.Load(reader);

                }
                db.closeConnection();
                dataGridView1.DataSource = dtConclusion;
            }
            else if(!checkBox1.Checked && checkBox2.Checked)
            {
                System.Data.DataTable dtConclusion = new System.Data.DataTable();

                DB db = new DB();

                
                db.openConnection();
                //опять костыли

                using (MySqlCommand cmd = new MySqlCommand("SELECT conclusion.conclusion_number, `evaluation date`, `sad`, main.inn,`name`,`reason for rating`, `subject`, `specification`," +
                "`initiator`, `object`, `result`, `price`, `status` FROM conclusion " +
                "INNER JOIN main ON main.conclusion_number = conclusion.conclusion_number " +
                "INNER JOIN organisation ON organisation.inn = main.inn ORDER BY conclusion.conclusion_number DESC", db.getConnection()))
                {
                    
                    MySqlDataReader reader = cmd.ExecuteReader();
                    dtConclusion.Load(reader);

                }
                db.closeConnection();
                dataGridView1.DataSource = dtConclusion;
            }
            else if(!checkBox1.Checked && !checkBox2.Checked)
            {
                System.Data.DataTable dtConclusion = new System.Data.DataTable();

                DB db = new DB();

                
                db.openConnection();
                //опять костыли

                using (MySqlCommand cmd = new MySqlCommand("SELECT conclusion.conclusion_number, `evaluation date`,`sad`, main.inn,`name`, `reason for rating`, `subject`, `specification`," +
                "`initiator`, `object`, `result`, `price`, `status` FROM conclusion " +
                "INNER JOIN main ON main.conclusion_number = conclusion.conclusion_number " +
                "INNER JOIN organisation ON organisation.inn = main.inn WHERE `status` = 1 ORDER BY conclusion.conclusion_number DESC", db.getConnection()))
                {
                    
                    MySqlDataReader reader = cmd.ExecuteReader();
                    dtConclusion.Load(reader);

                }
                db.closeConnection();
                dataGridView1.DataSource = dtConclusion;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            // creating Excel Application  
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application  
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // creating new Excelsheet in workbook  
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            // see the excel sheet behind the program  
            app.Visible = true;
            // get the reference of first sheet. By default its name is Sheet1.  
            // store its reference to worksheet  
            worksheet = workbook.Sheets["Лист1"];
            worksheet = workbook.ActiveSheet;
            // changing the name of active sheet  '
            DateTime a = DateTime.Now;
            worksheet.Name = a.ToShortDateString();
            // storing header part in Excel  
            for (int i = 1; i < dataGridView1.Columns.Count ; i++)
            {
                worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
            }
            // storing Each row and column value to excel sheet  
            for (int i = 0; i < dataGridView1.Rows.Count ; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count -1; j++)
                {
                    if(dataGridView1.Columns[j].HeaderText == "Дата оценки")
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString().Remove(dataGridView1.Rows[i].Cells[j].Value.ToString().IndexOf(" "));
                    }
                    else
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
            }
            // save the application  
            string date = DateTime.Now.ToString();
            date = date.Replace(':', '.');
            string path = getConfigPath(3) + "\\" + date +".xlsx";
            workbook.SaveAs(@path, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            // Exit from the application  

            //
            app.Quit();
            MessageBox.Show("Excel-файл сохранен.");
            Cursor.Current = Cursors.Default;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            (sender as PictureBox).Image = Image.FromFile("img\\plus_add_insert_append_icon_179162.png");
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            (sender as PictureBox).Image = Image.FromFile("img\\create_light.png");
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            (sender as PictureBox).Image = Image.FromFile("img\\edit_pencil_modify_write_icon_179065_.png");
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            (sender as PictureBox).Image = Image.FromFile("img\\edit_pencil_modify_write_icon_179065.png");
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            (sender as PictureBox).Image = Image.FromFile("img\\delete_red.png");
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            (sender as PictureBox).Image = Image.FromFile("img\\trash_delete_recycle_bin_remove_icon_179056.png");
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (textBox2.Text == "")
                {
                    loadDataCheckBox();
                    return;
                }
                System.Data.DataTable dtConclusion = new System.Data.DataTable();

                DB db = new DB();

                db.openConnection();

                using (MySqlCommand cmd = new MySqlCommand("SELECT conclusion.conclusion_number, `evaluation date`, `sad`, main.inn,`name`,`reason for rating`, `subject`, `specification`," +
                "`initiator`, `object`, `result`, `price`, `status` FROM conclusion " +
                "INNER JOIN main ON main.conclusion_number = conclusion.conclusion_number " +
                "INNER JOIN organisation ON organisation.inn = main.inn WHERE organisation.inn LIKE @number ", db.getConnection()))
                {
                    cmd.Parameters.Add("@number", MySqlDbType.VarChar).Value = "%" + textBox2.Text + "%";
                    MySqlDataReader reader = cmd.ExecuteReader();
                    dtConclusion.Load(reader);

                }

                db.closeConnection();
                dataGridView1.DataSource = dtConclusion;
                this.dataGridView1.Columns["Status"].Visible = false;
                dataGridView1.Refresh();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (textBox3.Text == "")
                {
                    loadDataCheckBox();
                    return;
                }
                System.Data.DataTable dtConclusion = new System.Data.DataTable();

                DB db = new DB();

                db.openConnection();

                using (MySqlCommand cmd = new MySqlCommand("SELECT conclusion.conclusion_number, `evaluation date`, `sad`, main.inn,`name`,`reason for rating`, `subject`, `specification`," +
                "`initiator`, `object`, `result`, `price`, `status` FROM conclusion " +
                "INNER JOIN main ON main.conclusion_number = conclusion.conclusion_number " +
                "INNER JOIN organisation ON organisation.inn = main.inn WHERE organisation.name LIKE @number ", db.getConnection()))
                {
                    cmd.Parameters.Add("@number", MySqlDbType.VarChar).Value = "%" + textBox3.Text + "%";
                    MySqlDataReader reader = cmd.ExecuteReader();
                    dtConclusion.Load(reader);

                }

                db.closeConnection();
                dataGridView1.DataSource = dtConclusion;
                this.dataGridView1.Columns["Status"].Visible = false;
                dataGridView1.Refresh();
            }
        }

        private void pictureBox7_MouseEnter(object sender, EventArgs e)
        {
            (sender as PictureBox).Image = Image.FromFile("img\\ticket_paper_icon_179218_.png");
        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            (sender as PictureBox).Image = Image.FromFile("img\\ticket_paper_icon_179218.png");
        }

        private void pictureBox7_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.InitialDelay = 0;
            tt.SetToolTip(this.pictureBox1, "Скопировать выбранное заключение");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
