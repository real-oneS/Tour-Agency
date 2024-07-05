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
    public partial class Form3 : Form
    {
        DataSet ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        string connectionString = "Server=server46;Database=Valiullin_VT-31;User Id=stud;Password=stud;";
        //string connectionString = "Data Source=COMPUTER;Initial Catalog=master;" +
        //        "Integrated Security=true;";
        string sql = "SELECT * FROM dbo.Clients ORDER BY Id";


        public Form3()
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
                dataGridView1.Columns["Surname"].HeaderText = "Фамилия";
                dataGridView1.Columns["FirstName"].HeaderText = "Имя";
                dataGridView1.Columns["Patronymic"].HeaderText = "Отчество";
                dataGridView1.Columns["Address"].HeaderText = "Адрес";
                dataGridView1.Columns["Telephone"].HeaderText = "Телефон";
                dataGridView1.Columns["Address"].Width = 160;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Columns["Address"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            }
        }

        
        

        private void button8_Click(object sender, EventArgs e)
        {
            Form f1 = new Form2();
            f1.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) // add
        {
            DataRow row = ds.Tables[0].NewRow(); // добавляем новую строку в DataTable
            ds.Tables[0].Rows.Add(row);

        }

        private void button3_Click(object sender, EventArgs e) // save
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
                    // Проверка на то, содержит ли в себе ячейка только цифры
                    try
                    {
                        Convert.ToInt32(row.Cells[1].Value);
                        MessageBox.Show("Введите фамилию, а не просто цифры!!!");
                        row.Cells[1].Style.BackColor = Color.Red;
                        return;
                    }
                    catch
                    {
                        row.Cells[1].Style.BackColor = Color.White;
                        // Проверка, есть ли в фамилии цифры
                        for (int k = 0; k < row.Cells[1].Value.ToString().Length; k++)
                        {
                            if (row.Cells[1].Value.ToString()[k] >= '0' && row.Cells[1].Value.ToString()[k] <= '9')
                            {
                                MessageBox.Show("В фамилии не может быть цифр!");
                                row.Cells[1].Style.BackColor = Color.Red;
                                return;
                                
                            }
                        }
                    }
                    // Проверка на то, содержит ли в себе ячейка только цифры
                    try
                    {
                        Convert.ToInt32(row.Cells[2].Value);
                        MessageBox.Show("Введите имя, а не просто цифры!!!");
                        row.Cells[2].Style.BackColor = Color.Red;
                        return;
                    }
                    catch
                    {
                        row.Cells[2].Style.BackColor = Color.White;
                        // Проверка, есть ли в имени цифры
                        for (int k = 0; k < row.Cells[2].Value.ToString().Length; k++)
                        {
                            if (row.Cells[2].Value.ToString()[k] >= '0' && row.Cells[2].Value.ToString()[k] <= '9')
                            {
                                MessageBox.Show("В имени не может быть цифр!");
                                row.Cells[2].Style.BackColor = Color.Red;
                                return;

                            }
                        }
                    }
                    // Проверка на то, содержит ли в себе ячейка только цифры
                    try
                    {
                        Convert.ToInt32(row.Cells[3].Value);
                        MessageBox.Show("Введите отчество, а не просто цифры!!!");
                        row.Cells[3].Style.BackColor = Color.Red;
                        return;
                    }
                    catch
                    {
                        row.Cells[3].Style.BackColor = Color.White;
                        // Проверка, есть ли в отчестве цифры
                        for (int k = 0; k < row.Cells[3].Value.ToString().Length; k++)
                        {
                            if (row.Cells[3].Value.ToString()[k] >= '0' && row.Cells[3].Value.ToString()[k] <= '9')
                            {
                                MessageBox.Show("В отчестве не может быть цифр!");
                                row.Cells[3].Style.BackColor = Color.Red;
                                return;

                            }
                        }
                    }
                    // Проверка на то, содержит ли в себе ячейка только цифры
                    try
                    {
                        Convert.ToInt32(row.Cells[4].Value);
                        MessageBox.Show("Введите адрес, а не просто цифры!!!");
                        row.Cells[4].Style.BackColor = Color.Red;
                        return;
                    }
                    catch
                    {
                        row.Cells[4].Style.BackColor = Color.White;
                    }
                    // Проверка на правильность номера
                    try
                    {
                        if (Convert.ToInt32(row.Cells[5].Value.ToString().Replace("+", "").Length) == 11)
                        {
                            row.Cells[5].Style.BackColor = Color.White;
                            
                        }
                        else
                        {
                            MessageBox.Show("Введите телефонный номер, состоящий из 11 цифр!!!");
                            row.Cells[5].Style.BackColor = Color.Red;
                            return;
                        }


                    }
                    catch
                    {
                        MessageBox.Show("Введите корректный номер, без лишних символов!!!");
                        row.Cells[5].Style.BackColor = Color.Red;
                        return;
                    }
                    // Если в номере нету плюса, программа добавит
                    if (row.Cells[5].Value.ToString().IndexOf("+") == -1)
                    {
                        row.Cells[5].Value= row.Cells[5].Value.ToString().Insert(0,"+");
                    }
                }
            }
            try {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    adapter = new SqlDataAdapter(sql, connection);
                    commandBuilder = new SqlCommandBuilder(adapter);
                    adapter.InsertCommand = new SqlCommand("sp_CreateClient", connection);
                    adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@surname", SqlDbType.NVarChar, 50, "Surname"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@firstname", SqlDbType.NVarChar, 50, "Firstname"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@patronymic", SqlDbType.NVarChar, 50, "Patronymic"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@address", SqlDbType.NVarChar, 50, "Address"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@telephone", SqlDbType.NVarChar, 50, "Telephone"));

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

        private void button4_Click(object sender, EventArgs e) // delete
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

  
    }
}

