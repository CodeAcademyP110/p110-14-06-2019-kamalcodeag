using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            string Address = "Data Source=(local); Initial Catalog='AutoCars'; Integrated Security=SSPI";

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(Address);
                conn.Open();

                string cmdCommand = "Select Name from Marks";
                SqlCommand comm = new SqlCommand(cmdCommand, conn);
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cmbMarks.Items.Add(reader["Name"]);
                    }
                    cmbMarks.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("There is not row like this 1");
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Oldu Qardaw");
                conn.Close();
            }

            finally
            {
                conn.Close();
            }
        }

        private void CmbMarks_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string cmbText = cmbMarks.Text;

            string Address = "Data Source=(local); Initial Catalog='AutoCars'; Integrated Security=SSPI";

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(Address);
                conn.Open();

                string cmdCommand = "Select Models.Name from Models " +
                                     "join Marks on Models.MarksId = Marks.Id " +
                                     $"where Marks.Name = '{cmbText}'";

                SqlCommand comm = new SqlCommand(cmdCommand, conn);
                SqlDataReader reader = comm.ExecuteReader();

                if (reader.HasRows)
                {
                    cmbModels.Items.Clear();

                    while (reader.Read())
                    {
                        cmbModels.Items.Add(reader["Name"]);
                    }
                    cmbModels.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("There is not row like this 2");
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Oldu Qardaw");
                conn.Close();
            }

            finally
            {
                conn.Close();
            }
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            string input = txtAdd.Text;
            string Address2 = "Data Source=(local); Initial Catalog='AutoCars'; Integrated Security=SSPI";
            SqlConnection conn2 = new SqlConnection(Address2);
            conn2.Open();

            string commandText = $"insert into Marks (name) values('{input}')";
            SqlCommand comm2 = new SqlCommand(commandText, conn2);

            SqlDataReader reader2 = comm2.ExecuteReader();


            #region Load
            string Address = "Data Source=(local); Initial Catalog='AutoCars'; Integrated Security=SSPI";

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(Address);
                conn.Open();

                string cmdCommand = "Select Name from Marks";
                SqlCommand comm = new SqlCommand(cmdCommand, conn);
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.HasRows)
                {
                    cmbMarks.Items.Clear();
                    while (reader.Read())
                    {
                        cmbMarks.Items.Add(reader["Name"]);
                    }
                    cmbMarks.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("There is not row like this");
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Oldu Qardash");
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
            #endregion

            conn2.Close();
        }
    }
}
