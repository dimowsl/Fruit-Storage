using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Fruit_Storage
{
    public partial class Form1 : Form
    {
        private string connString = @"Server=(localdb)\MSSQLLocalDB;Database=Fruit_Storage.FruitsContext;Integrated Security=true";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            string query = "SELECT * FROM Fruit";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problem s bazata danni: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string description = textBox2.Text;
            decimal price;
            int typeId;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description) ||
                !decimal.TryParse(textBox3.Text, out price) || !int.TryParse(textBox4.Text, out typeId))
            {
                MessageBox.Show("nevaliden vhod");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string insertQuery = "INSERT INTO Fruit (Name, Description, Price, FruitTypeId) VALUES (@Name, @Description, @Price, @FruitTypeId)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@FruitTypeId", typeId);
                        cmd.ExecuteNonQuery();
                    }

                    LoadData();

                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problem s vkarvane na zapisa: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("Izberi red");
                return;
            }

            int index = dataGridView1.CurrentCell.RowIndex;
            if (index < 0)
            {
                MessageBox.Show("nevaliden red");
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            var idCell = selectedRow.Cells["Id"];

            if (idCell == null || idCell.Value == DBNull.Value)
            {
                MessageBox.Show("nevaliden primary key");
                return;
            }

            int id = Convert.ToInt32(idCell.Value);

            string name = string.IsNullOrWhiteSpace(textBox1.Text) ? selectedRow.Cells["Name"].Value.ToString() : textBox1.Text;
            string description = string.IsNullOrWhiteSpace(textBox2.Text) ? selectedRow.Cells["Description"].Value.ToString() : textBox2.Text;
            decimal price;
            if (!decimal.TryParse(textBox3.Text, out price))
            {
                price = Convert.ToDecimal(selectedRow.Cells["Price"].Value);
            }
            int typeId;
            if (!int.TryParse(textBox4.Text, out typeId))
            {
                typeId = Convert.ToInt32(selectedRow.Cells["FruitTypeId"].Value);
            }

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string updateQuery = "UPDATE Fruit SET Name = @Name, Description = @Description, Price = @Price, FruitTypeId = @FruitTypeId WHERE Id = @Id";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@FruitTypeId", typeId);
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }

                    LoadData();

                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problem s update na zapisa: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("Izberi red");
                return;
            }

            int index = dataGridView1.CurrentCell.RowIndex;
            if (index < 0)
            {
                MessageBox.Show("nevaliden red");
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            var idCell = selectedRow.Cells["Id"];

            if (idCell == null || idCell.Value == DBNull.Value)
            {
                MessageBox.Show("nevaliden primary key");
                return;
            }

            int id = Convert.ToInt32(idCell.Value);

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM Fruit WHERE Id = @Id";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }

                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error v trieneto na red: " + ex.Message);
                }
            }
        }
    }
}
