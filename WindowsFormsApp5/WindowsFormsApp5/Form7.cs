using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Form7 : Form
    {
        DataSet ds;
        SqlDataAdapter adapter;
        SqlDataAdapter adapter1;
        SqlDataAdapter adapter2;
        SqlCommandBuilder commandBuilder;
        string connectionString = "Server=server46;Database=Valiullin_VT-31;User Id=stud;Password=stud;";
        //string connectionString = "Data Source=COMPUTER;Initial Catalog=master;" +
        //        "Integrated Security=true;";
        string sql = "SELECT * FROM dbo.Vouchers ORDER BY Id";
        string sql1 = "SELECT [Id] ,[Surname]+' '+[FirstName]+' '+[Patronymic] as Fullname,[Address],[Telephone] FROM[dbo].[Clients]";
        string sql2 = "SELECT r.[Id], h.[Name]+' '+h.Country as HotelInfo,[Duration],r.[Cost] FROM [dbo].[Routes] as r inner join dbo.Hotels as h on r.HotelId= h.Id";
        public Form7()
        {
            InitializeComponent();


            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);
                adapter1 = new SqlDataAdapter(sql1, connection);
                adapter2 = new SqlDataAdapter(sql2, connection);
                ds = new DataSet();
                adapter.Fill(ds, "Vouchers");
                adapter1.Fill(ds, "Clients");
                adapter2.Fill(ds, "Routes");
                dataGridView1.DataSource = ds.Tables[0];
                ds.Relations.Add(new DataRelation("Cli", ds.Tables["Clients"].Columns["Id"], ds.Tables["Vouchers"].Columns["ClientId"]));
                ds.Relations.Add(new DataRelation("Rou", ds.Tables["Routes"].Columns["Id"], ds.Tables["Vouchers"].Columns["RouteId"]));
                // делаем недоступным столбец id для изменения
                dataGridView1.Columns["Id"].ReadOnly = true;
                dataGridView1.Columns["Id"].Visible = false;
                dataGridView1.Columns["RouteId"].HeaderText = "Номер маршрута";
                dataGridView1.Columns["ClientId"].HeaderText = "Номер клиента";
                dataGridView1.Columns["Date"].HeaderText = "Дата отправки";
                dataGridView1.Columns["Count"].HeaderText = "Количество билетов";
                dataGridView1.Columns["Discount"].HeaderText = "Скидка";
                dataGridView1.Columns["FinalCost"].HeaderText = "Финальная стоимость";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                var client = new DataGridViewComboBoxColumn(); // добавить новую колонку
                client.Name = "Данные клиента";
                client.DataSource = ds.Tables["Clients"];
                client.DisplayMember = "Fullname";
                client.ValueMember = "Id";
                client.DataPropertyName = "ClientId"; // Для связи с Routes
                client.MaxDropDownItems = 5;
                client.FlatStyle = FlatStyle.Flat;
                dataGridView1.Columns.Insert(2, client);
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView1.Columns["ClientId"].Visible = false;

                var route = new DataGridViewComboBoxColumn(); // добавить новую колонку
                route.Name = "Информация о маршруте";
                route.DataSource = ds.Tables["Routes"];
                route.DisplayMember = "HotelInfo";
                route.ValueMember = "Id";
                route.DataPropertyName = "RouteId"; // Для связи с Routes
                route.MaxDropDownItems = 5;
                route.FlatStyle = FlatStyle.Flat;
                dataGridView1.Columns.Insert(1, route);
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView1.Columns["RouteId"].Visible = false;

                CalendarColumn col = new CalendarColumn();
                col.HeaderText = "Дата отправки";
                col.DataPropertyName = "Date";
                dataGridView1.Columns.Insert(3, col);
                dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView1.Columns["Date"].Visible = false;



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
                        if (Convert.ToInt32(row.Cells[4].Value) > 15)
                        {
                            MessageBox.Show("На одного человека не может быть более 15 билетов!!!");
                            row.Cells[4].Style.BackColor = Color.Red;
                            return;
                        }
                    }
                    catch
                    {
                        row.Cells[2].Style.BackColor = Color.White;
                    }
                    try
                    {
                        if (Convert.ToInt32(row.Cells[5].Value.ToString().Replace("%", "")) <= 100)
                        {
                            row.Cells[5].Style.BackColor = Color.White;
                        }
                        
                        else
                        {
                            MessageBox.Show("Введите корректное число, не превыщающее 100%!!!");
                            row.Cells[5].Style.BackColor = Color.Red;
                            return;
                        }
                        if (Convert.ToInt32(row.Cells[5].Value.ToString().Replace("%", "")) < 0)
                        {
                            MessageBox.Show("Проценты не могут быть отрицательным числом!!!");
                            row.Cells[5].Style.BackColor = Color.Red;
                            return;
                        }

                    }
                    catch
                    {
                        MessageBox.Show("Введите корректное число, без лишних символов!!!");
                        row.Cells[5].Style.BackColor = Color.Red;
                        return;
                    }
                    if (row.Cells[5].Value.ToString().IndexOf("%") == -1)
                    {
                        row.Cells[5].Value += "%";
                    }
                    try
                    {
                        if (Convert.ToInt32(row.Cells[6].Value) < 5000)
                        {
                            MessageBox.Show("Стоимость путёвки с учётом всех скидок не может быть меньше 5000 рублей!!!!!!");
                            row.Cells[6].Style.BackColor = Color.Red;
                            return;
                        }
                    }
                    catch
                    {
                        row.Cells[6].Style.BackColor = Color.White;
                    }


                }
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);
                adapter1 = new SqlDataAdapter(sql1, connection);
                adapter2 = new SqlDataAdapter(sql2, connection);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("sp_CreateVoucher", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@routeid", SqlDbType.Int, 0, "RouteId"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@clientid", SqlDbType.Int, 0, "ClientId"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@date", SqlDbType.Date, 50, "date"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@count", SqlDbType.Int, 0, "Count"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@discount", SqlDbType.NVarChar, 50, "Discount"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@finalcost", SqlDbType.Int, 0, "FinalCost"));

                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
                parameter.Direction = ParameterDirection.Output;

                adapter.Update(ds, "Vouchers");
            }
            MessageBox.Show("Сохранение успешно выполнено.");
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
            MessageBox.Show("Ошибка ввода!!!");
        }

        private void Form7_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
