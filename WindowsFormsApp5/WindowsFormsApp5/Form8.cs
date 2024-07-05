using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Form8 : Form
    {
        DataSet ds;
        SqlDataAdapter adapter;

        string connectionString = "Server=server46;Database=Valiullin_VT-31;User Id=stud;Password=stud;";
        //string connectionString = "Data Source=COMPUTER;Initial Catalog=master;" +
        //        "Integrated Security=true;";
        string sql = "SELECT * FROM dbo.VouchersView ORDER BY HotelName";

        public Form8()
        {
            InitializeComponent();

            
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);

                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                // делаем недоступным столбец id для изменения
                dataGridView1.ReadOnly = true;
                dataGridView1.Columns["HotelName"].HeaderText = "Название отеля";
                dataGridView1.Columns["HotelCountry"].HeaderText = "Страна";
                dataGridView1.Columns["HotelClimate"].HeaderText = "Климат";
                dataGridView1.Columns["RouteDuration"].HeaderText = "Длительность";
                dataGridView1.Columns["ClientSurname"].HeaderText = "Фамилия";
                dataGridView1.Columns["ClientFirstName"].HeaderText = "Имя";
                dataGridView1.Columns["ClientPatronymic"].HeaderText = "Отчество";
                dataGridView1.Columns["ClientAddress"].HeaderText = "Адрес";
                dataGridView1.Columns["ClientTelephone"].HeaderText = "Телефон";
                dataGridView1.Columns["Date"].HeaderText = "Дата отправки";
                dataGridView1.Columns["Count"].HeaderText = "Количество билетов";
                dataGridView1.Columns["Discount"].HeaderText = "Скидка";
                dataGridView1.Columns["FinalCost"].HeaderText = "Финальная стоимость";
                dataGridView1.Columns["HotelCountry"].Width=150;
                dataGridView1.Columns["HotelName"].Width=150;
                dataGridView1.Columns["ClientAddress"].Width=140;
                dataGridView1.Columns["Count"].Width=70;
                dataGridView1.Columns["FinalCost"].Width = 70;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Columns["ClientAddress"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView1.Columns["HotelName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView1.Columns["HotelCountry"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;




            }
        }

        

        

        private void button8_Click_1(object sender, EventArgs e)
        {
            Form f1 = new Form2();
            f1.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form8_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
