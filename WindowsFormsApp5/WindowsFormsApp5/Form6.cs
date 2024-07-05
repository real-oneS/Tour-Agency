using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Form6 : Form

    {
        DataSet ds;
        SqlDataAdapter adapter;
        SqlDataAdapter adapter1;
        SqlCommandBuilder commandBuilder;
        string connectionString = "Server=server46;Database=Valiullin_VT-31;User Id=stud;Password=stud;";
        //string connectionString = "Data Source=COMPUTER;Initial Catalog=master;" +
        //        "Integrated Security=true;";
        string sql = "SELECT * FROM dbo.Routes ORDER BY Id";
        string sql1 = "SELECT [Id],[Name]+' '+[Country] as FullInfo,[Climate],[Cost]FROM[dbo].[Hotels]";


        public Form6()
        {
            InitializeComponent();

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);
                adapter1 = new SqlDataAdapter(sql1, connection);
                ds = new DataSet();
                adapter.Fill(ds, "Routes");
                adapter1.Fill(ds, "Hotels");
                dataGridView1.DataSource = ds.Tables[0];
                
                ds.Relations.Add(new DataRelation("Hot", ds.Tables["Hotels"].Columns["Id"], ds.Tables["Routes"].Columns["HotelId"]));
                // делаем недоступным столбец id для изменения
                dataGridView1.Columns["Id"].ReadOnly = true;
                dataGridView1.Columns["Id"].Visible = false;
                dataGridView1.Columns["HotelId"].HeaderText = "Номер отеля";
                dataGridView1.Columns["Duration"].HeaderText = "Длительность";
                dataGridView1.Columns["Cost"].HeaderText = "Стоимость";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                var hotel = new DataGridViewComboBoxColumn(); // добавить новую колонку
                hotel.Name = "Отель";
                hotel.DataSource = ds.Tables["Hotels"];
                hotel.DisplayMember = "FullInfo";
                hotel.ValueMember = "Id";
                hotel.DataPropertyName = "HotelId"; // Для связи с Routes
                hotel.FlatStyle = FlatStyle.Flat;
                dataGridView1.Columns.Insert(1, hotel);
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView1.Columns["HotelId"].Visible = false;


                connection.Close();


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

        private void button2_Click(object sender, EventArgs e)
        {
            DataRow row = ds.Tables[0].NewRow(); // добавляем новую строку в DataTable
            ds.Tables[0].Rows.Add(row);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                for (int i = 1; i < row.Cells.Count; i++)
                {
                    

                    //Проверка на пустоту ячеек
                    if (row.Cells[i].Value == null || row.Cells[i].Value == DBNull.Value)
                    {
                        MessageBox.Show("Заполните пустые ячейки!!!");
                        row.Cells[i].Style.BackColor = Color.Red;
                        return;
                    }
                    else
                    {
                        row.Cells[i].Style.BackColor = Color.White;
                    }
                    
                    try
                    {
                        if (Convert.ToInt32(row.Cells[2].Value) > 10)
                        {
                            MessageBox.Show("Длительность поездки не превышает 10 недель!!!");
                            row.Cells[2].Style.BackColor = Color.Red;
                            return;
                        }
                    }
                    catch
                    {
                        row.Cells[2].Style.BackColor = Color.White;
                    }
                    try
                    {
                        if (Convert.ToInt32(row.Cells[2].Value) < 0)
                        {
                            MessageBox.Show("Длительность поездки не может быть отрицательным числом!!!");
                            row.Cells[2].Style.BackColor = Color.Red;
                            return;
                        }
                    }
                    catch
                    {
                        row.Cells[2].Style.BackColor = Color.White;
                    }
                    try
                    {
                        if (Convert.ToInt32(row.Cells[3].Value) < 20000)
                        {
                            MessageBox.Show("Стоимость поездки не может быть меньше 20000 рублей!!!!!!");
                            row.Cells[3].Style.BackColor = Color.Red;
                            return;
                        }
                    }
                    catch
                    {
                        row.Cells[3].Style.BackColor = Color.White;
                    }
                    
                 
                }
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    adapter = new SqlDataAdapter(sql, connection);
                    adapter1 = new SqlDataAdapter(sql1, connection);
                    commandBuilder = new SqlCommandBuilder(adapter);

                    adapter.InsertCommand = new SqlCommand("sp_CreateRoute", connection);
                    adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@hotelid", SqlDbType.Int, 0, "HotelId"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@duration", SqlDbType.Int, 0, "Duration"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@cost", SqlDbType.Int, 0, "Cost"));

                    SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
                    parameter.Direction = ParameterDirection.Output;

                    adapter.Update(ds, "Routes");

                    connection.Close();
                }
                MessageBox.Show("Сохранение успешно выполнено.");

            }

            catch
            {
                MessageBox.Show("Вы не можете удалить данную строку, так как она используется в других таблицах!!!");

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Ошибка ввода данных!!!");
        }

        private void Form6_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
