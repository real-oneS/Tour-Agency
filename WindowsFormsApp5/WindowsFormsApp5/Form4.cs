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
    public partial class Form4 : Form
    {
        DataSet ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        string connectionString = "Server=server46;Database=Valiullin_VT-31;User Id=stud;Password=stud;";
        //string connectionString = "Data Source=COMPUTER;Initial Catalog=master;" +
        //        "Integrated Security=true;";
        string sql = "SELECT * FROM dbo.Discounts ORDER BY Id";
        public Form4()
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
                dataGridView1.Columns["Id"].ReadOnly = true;
                dataGridView1.Columns["Id"].Visible = false;
                dataGridView1.Columns["Condition"].HeaderText = "Условие";
                dataGridView1.Columns["Condition"].Width = 200;
                dataGridView1.Columns["Count"].HeaderText = "Количество";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Columns["Condition"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            }
        }

        

        

        private void button8_Click_1(object sender, EventArgs e)
        {
            Form f1 = new Form2();
            f1.Show();
            this.Hide();
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
                        Convert.ToInt32(row.Cells[1].Value);
                        MessageBox.Show("Введите условие, а не просто цифры!!!");
                        row.Cells[1].Style.BackColor = Color.Red;
                        return;
                    }
                    catch
                    {
                        row.Cells[1].Style.BackColor = Color.White;
                    }
                    
                    try
                    {
                        if (Convert.ToInt32(row.Cells[2].Value.ToString().Replace("%", "")) <= 100)
                        {
                            row.Cells[2].Style.BackColor = Color.White;
                        }
                        
                        else
                        {
                            MessageBox.Show("Введите корректное число, не превыщающее 100%!!!");
                            row.Cells[2].Style.BackColor = Color.Red;
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
                        row.Cells[2].Style.BackColor = Color.Red;
                        return;
                    }
                    if (row.Cells[2].Value.ToString().IndexOf("%") == -1)
                    {
                        row.Cells[2].Value += "%";
                    }
                    


                }
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    adapter = new SqlDataAdapter(sql, connection);
                    commandBuilder = new SqlCommandBuilder(adapter);
                    adapter.InsertCommand = new SqlCommand("sp_CreateDiscount", connection);
                    adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@condition", SqlDbType.NVarChar, 50, "Condition"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@count", SqlDbType.NVarChar, 50, "Count"));

                    SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
                    parameter.Direction = ParameterDirection.Output;

                    adapter.Update(ds);
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

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
