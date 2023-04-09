using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;

namespace kursach3
{
    public partial class Form1 : Form
    {

        private SqlConnection conn = null;
        Timer timer = new Timer();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            conn.Open();

            AppData1();
            AppData2();
            AppData3();

            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            dataGridView1.ReadOnly = true;
            dataGridView2.ReadOnly = true;
            dataGridView3.ReadOnly = true;
            dataGridView4.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView3.AllowUserToAddRows = false;
            dataGridView4.AllowUserToAddRows = false;
        }
        private void AppData1()//обновление данных в таблице 1
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Product AS [Товар], Provider AS [Поставщик], Amount AS [Количество товара], Price AS [Цена за единицу товара] FROM tovari", conn);
            DataSet set = new DataSet();
            adapter.Fill(set);
            dataGridView1.DataSource = set.Tables[0];

        }
        private void AppData2()//обновление данных в таблице 2
        {
            SqlDataAdapter adapter1 = new SqlDataAdapter("SELECT Product AS [Товар], Provider AS [Поставщик], Amount AS [Количество товара], Price AS [Цена за единицу товара] FROM tovari1", conn);
            DataSet set1 = new DataSet();
            adapter1.Fill(set1);
            dataGridView2.DataSource = set1.Tables[0];
        }
        private void AppData3()//обновление данных в таблице 3
        {
            SqlDataAdapter adapter2 = new SqlDataAdapter("SELECT Product AS [Товар], Provider AS [Поставщик], Amount AS [Количество товара], Price AS [Цена за единицу товара] FROM tovari2", conn);
            DataSet set2 = new DataSet();
            adapter2.Fill(set2);
            dataGridView3.DataSource = set2.Tables[0];
        }

        private void timer_Tick(object sender, EventArgs e)//сумма в лэйбле
        {
            SqlCommand cmd1 = new SqlCommand("SELECT SUM(Amount) FROM [tovari]", conn);
            string query1 = "SELECT Amount FROM [tovari]";
            using (SqlCommand command = new SqlCommand(query1, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Close();
                        string query11 = "SELECT SUM(Amount) FROM [tovari]";
                        SqlCommand command1 = new SqlCommand(query11, conn);
                        int sum1 = (int)command1.ExecuteScalar();
                        label1.Text = sum1.ToString();
                    }
                    else
                    {
                        reader.Close();
                        int sum1 = 0;
                        label1.Text = sum1.ToString();
                    }
                }
            }
            SqlCommand cmd2 = new SqlCommand("SELECT SUM(Amount) FROM [tovari1]", conn);
            string query2 = "SELECT Amount FROM [tovari1]";
            using (SqlCommand command = new SqlCommand(query2, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Close();
                        string query22 = "SELECT SUM(Amount) FROM [tovari1]";
                        SqlCommand command1 = new SqlCommand(query22, conn);
                        int sum1 = (int)command1.ExecuteScalar();
                        label2.Text = sum1.ToString();
                    }
                    else
                    {
                        reader.Close();
                        int sum1 = 0;
                        label2.Text = sum1.ToString();
                    }
                }
            }
            SqlCommand cmd3 = new SqlCommand("SELECT SUM(Amount) FROM [tovari2]", conn);
            string query3 = "SELECT Amount FROM [tovari2]";
            using (SqlCommand command = new SqlCommand(query3, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Close();
                        string query33 = "SELECT SUM(Amount) FROM [tovari2]";
                        SqlCommand command1 = new SqlCommand(query33, conn);
                        int sum1 = (int)command1.ExecuteScalar();
                        label3.Text = sum1.ToString();
                    }
                    else
                    {
                        reader.Close();
                        int sum1 = 0;
                        label3.Text = sum1.ToString();
                    }
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Товар LIKE '%{textBox5.Text}%'";
        }
        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Товар LIKE '%{textBox10.Text}%'";
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            (dataGridView3.DataSource as DataTable).DefaultView.RowFilter = $"Товар LIKE '%{textBox15.Text}%'";
        }
        private void button1_Click(object sender, EventArgs e) //select
        {
            
            string query1 = "SELECT Amount FROM [tovari]";
            using (SqlCommand command = new SqlCommand(query1, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Close();
                        string query2 = "SELECT SUM(Amount) FROM [tovari]";
                        SqlCommand command1 = new SqlCommand(query2, conn);
                        int total = Convert.ToInt32(command1.ExecuteScalar());

                        if (total < 10000)
                        {
                            if (Convert.ToInt32(textBox3.Text) <= (10000 - Convert.ToInt32(label1.Text)))
                            {
                                using (SqlCommand command2 = new SqlCommand($"INSERT INTO [tovari] (Product, Provider, Amount, Price) VALUES (N'{textBox1.Text}',N'{textBox2.Text}',N'{textBox3.Text}',N'{textBox4.Text}')", conn))

                                {
                                    command2.ExecuteNonQuery();
                                    AppData1();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Недостаточно места на складе", "Склад", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            
                        }
                        else
                        {
                            MessageBox.Show("Недостаточно места на складе", "Склад", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        reader.Close();
                        SqlCommand command3 = new SqlCommand($"INSERT INTO [tovari] (Product, Provider, Amount, Price) VALUES (N'{textBox1.Text}',N'{textBox2.Text}',N'{textBox3.Text}',N'{textBox4.Text}')", conn);
                        
                        command3.ExecuteNonQuery();
                        AppData1();
                        
                        
                    }
                }
            }
        }
        private void button2_Click(object sender, EventArgs e) //select
        {
            
            string query1 = "SELECT Amount FROM [tovari1]";
            using (SqlCommand command = new SqlCommand(query1, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Close();
                        string query2 = "SELECT SUM(Amount) FROM [tovari1]";
                        SqlCommand command1 = new SqlCommand(query2, conn);
                        int total = Convert.ToInt32(command1.ExecuteScalar());

                        if (total < 10000)
                        {
                            if (Convert.ToInt32(textBox8.Text) <= (10000 - Convert.ToInt32(label2.Text)))
                            {
                                using (SqlCommand command2 = new SqlCommand($"INSERT INTO [tovari1] (Product, Provider, Amount, Price) VALUES (N'{textBox6.Text}',N'{textBox7.Text}',N'{textBox8.Text}',N'{textBox9.Text}')", conn))
                                {
                                    command2.ExecuteNonQuery();
                                    AppData2();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Недостаточно места на складе", "Склад", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                                
                        }
                        else
                        {
                            MessageBox.Show("Недостаточно места на складе", "Склад", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        reader.Close();
                        SqlCommand command3 = new SqlCommand($"INSERT INTO [tovari1] (Product, Provider, Amount, Price) VALUES (N'{textBox6.Text}',N'{textBox7.Text}',N'{textBox8.Text}',N'{textBox9.Text}')", conn);
                        command3.ExecuteNonQuery();
                        AppData2();
                        
                        
                    }
                }
            }

        }


        private void button3_Click(object sender, EventArgs e) //select
        {
            
            
            string query1 = "SELECT Amount FROM [tovari2]";
            using (SqlCommand command = new SqlCommand(query1, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Close();
                        string query2 = "SELECT SUM(Amount) FROM [tovari2]";
                        SqlCommand command1 = new SqlCommand(query2, conn);
                        int total = Convert.ToInt32(command1.ExecuteScalar());

                        if (total < 10000)
                        {
                            if (Convert.ToInt32(textBox13.Text) <= (10000 - Convert.ToInt32(label3.Text)))
                            {
                                using (SqlCommand command2 = new SqlCommand($"INSERT INTO [tovari2] (Product, Provider, Amount, Price) VALUES (N'{textBox11.Text}',N'{textBox12.Text}',N'{textBox13.Text}',N'{textBox14.Text}')", conn))
                                {
                                    command2.ExecuteNonQuery();
                                    AppData3();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Недостаточно места на складе", "Склад", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                                
                        }
                        else
                        {
                            MessageBox.Show("Недостаточно места на складе", "Склад", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        reader.Close();
                        SqlCommand command3 = new SqlCommand($"INSERT INTO [tovari2] (Product, Provider, Amount, Price) VALUES (N'{textBox11.Text}',N'{textBox12.Text}',N'{textBox13.Text}',N'{textBox14.Text}')", conn);
                        command3.ExecuteNonQuery();
                        AppData3();
                        
                    }
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы хотите удалить товар со склада?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                int rowindex = dataGridView2.CurrentCell.RowIndex;
                string id = Convert.ToString(dataGridView2.Rows[rowindex].Cells["Товар"].Value);
                string query = "DELETE FROM [tovari1] WHERE Product=@product";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("Product", id);
                    command.ExecuteNonQuery();
                }
                dataGridView2.Rows.RemoveAt(rowindex);
            }
            MessageBox.Show("Товар удален со склада", "Удалено", MessageBoxButtons.OK, MessageBoxIcon.Information);

            AppData2();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы хотите удалить товар со склада?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                int rowindex = dataGridView1.CurrentCell.RowIndex;
                string id = Convert.ToString(dataGridView1.Rows[rowindex].Cells["Товар"].Value);
                string query = "DELETE FROM [tovari] WHERE Product=@product";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("Product", id);
                    command.ExecuteNonQuery();
                }
                dataGridView1.Rows.RemoveAt(rowindex);
            }
            MessageBox.Show("Товар удален со склада", "Удалено", MessageBoxButtons.OK, MessageBoxIcon.Information);

            AppData1();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы хотите удалить товар со склада?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                int rowindex = dataGridView3.CurrentCell.RowIndex;
                string id = Convert.ToString(dataGridView3.Rows[rowindex].Cells["Товар"].Value);
                string query = "DELETE FROM [tovari2] WHERE Product=@product";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("Product", id);
                    command.ExecuteNonQuery();
                }
                dataGridView3.Rows.RemoveAt(rowindex);
            }
            MessageBox.Show("Товар удален со склада", "Удалено", MessageBoxButtons.OK, MessageBoxIcon.Information);

            AppData3();
        }
        
        private void button7_Click(object sender, EventArgs e)
        {
            DataGridViewButtonColumn delete = new DataGridViewButtonColumn();
            delete.HeaderText = "Удалить";
            delete.Name = "delete";
            delete.Text = "Удалить";
            delete.UseColumnTextForButtonValue = true;
            dataGridView4.Columns.Add(delete);
            SqlCommand cmd1 = new SqlCommand("SELECT Product AS [Товар], Provider AS [Поставщик], Amount AS [Количество товара], Price AS [Цена за единицу товара], '1' AS [Номер склада] FROM tovari", conn);
            SqlCommand cmd2 = new SqlCommand("SELECT Product AS [Товар], Provider AS [Поставщик], Amount AS [Количество товара], Price AS [Цена за единицу товара], '2' AS [Номер склада] FROM tovari1", conn);
            SqlCommand cmd3 = new SqlCommand("SELECT Product AS [Товар], Provider AS [Поставщик], Amount AS [Количество товара], Price AS [Цена за единицу товара], '3' AS [Номер склада] FROM tovari2", conn);
            SqlDataAdapter ad1 = new SqlDataAdapter(cmd1);
            SqlDataAdapter ad2 = new SqlDataAdapter(cmd2);
            SqlDataAdapter ad3 = new SqlDataAdapter(cmd3);
            DataSet set3 = new DataSet();
            ad1.Fill(set3, "tovari");
            ad2.Fill(set3, "tovari1");
            ad3.Fill(set3, "tovati2");
            DataTable dt = new DataTable();
            foreach (DataColumn col in set3.Tables["tovati2"].Columns)
            {
                dt.Columns.Add(col.ColumnName, col.DataType);
            }
            dt.Merge(set3.Tables["tovari"]);
            dt.Merge(set3.Tables["tovari1"]);
            dt.Merge(set3.Tables["tovati2"]);
            dataGridView4.DataSource = dt;
        }
        
        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView4.Columns["delete"].Index && e.RowIndex >= 0)
            {
                DialogResult result = MessageBox.Show("Вы хотите удалить товар со склада?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (dataGridView4.Rows[e.RowIndex].Cells["Номер склада"].Value.ToString()=="1")
                    {
                        string deleteValue = dataGridView4.Rows[e.RowIndex].Cells["Товар"].Value.ToString();
                        SqlCommand delete = new SqlCommand("DELETE FROM [tovari] WHERE Product = @deleteValue ", conn);
                        delete.Parameters.AddWithValue("@deleteValue", deleteValue);
                        delete.ExecuteNonQuery();
                        dataGridView4.Rows.RemoveAt(e.RowIndex);
                        AppData1();
                        AppData2();
                        AppData3();
                        MessageBox.Show("Товар удален со склада", "Удалено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if(dataGridView4.Rows[e.RowIndex].Cells["Номер склада"].Value.ToString() == "2")
                    {
                        string deleteValue = dataGridView4.Rows[e.RowIndex].Cells["Товар"].Value.ToString();
                        SqlCommand delete = new SqlCommand("DELETE FROM [tovari1] WHERE Product = @deleteValue ", conn);
                        delete.Parameters.AddWithValue("@deleteValue", deleteValue);
                        delete.ExecuteNonQuery();
                        dataGridView4.Rows.RemoveAt(e.RowIndex);
                        AppData1();
                        AppData2();
                        AppData3();
                        MessageBox.Show("Товар удален со склада", "Удалено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (dataGridView4.Rows[e.RowIndex].Cells["Номер склада"].Value.ToString() == "3")
                    {
                        string deleteValue = dataGridView4.Rows[e.RowIndex].Cells["Товар"].Value.ToString();
                        SqlCommand delete = new SqlCommand("DELETE FROM [tovari2] WHERE Product = @deleteValue ", conn);
                        delete.Parameters.AddWithValue("@deleteValue", deleteValue);
                        delete.ExecuteNonQuery();
                        dataGridView4.Rows.RemoveAt(e.RowIndex);
                        AppData1();
                        AppData2();
                        AppData3();
                        MessageBox.Show("Товар удален со склада","Удалено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            (dataGridView4.DataSource as DataTable).DefaultView.RowFilter = $"Товар LIKE '%{textBox16.Text}%'";
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if(!Char.IsDigit(ch) && ch != 8) //8 - код клавиши Backspace
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox13_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox14_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }
    }
}

