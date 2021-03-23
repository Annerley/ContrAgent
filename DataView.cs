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
        string name = "";
        public DataView(string name)
        {
            InitializeComponent();

            label53.Text = "Пользователь: " + name;
            name = this.name;
            TimeUpdater();


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
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 Form1 = new Form1(name);
            Form1.Show();
        }
    }



}
