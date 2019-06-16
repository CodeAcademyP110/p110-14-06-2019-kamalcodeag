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

namespace SQL_with_CSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'practiceDataSet1.positions' table. You can move, or remove it, as needed.
            this.positionsTableAdapter.Fill(this.practiceDataSet1.positions);
            string address = "Data Source = (local); Initial Catalog = practice; Integrated Security=SSPI";
            SqlConnection connection = new SqlConnection(address);
            connection.Open();

            //MessageBox.Show("Opened");
            //MessageBox.Show("Closed");

            string sqlText = "select * from players";
            SqlCommand sqlCommand = new SqlCommand(sqlText,connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            if(reader.HasRows)
            {
                while (reader.Read())
                {
                    //comboBox1.Items.Clear();
                    comboBox1.Items.Add(reader["name"]);
                }
            }
            else
            {
                MessageBox.Show("Unknown error occured");
            }
            

            connection.Close();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string input = comboBox1.Text;
            string address = "Data Source = (local); Initial Catalog = practice; Integrated Security=SSPI";
            SqlConnection connection = new SqlConnection(address);
            connection.Open();

            string sqlText = $"select countries.name from players join countries on players.countryId = countries.id where players.name = '{input}'";
            SqlCommand sqlCommand = new SqlCommand(sqlText, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    comboBox2.Items.Clear();
                    comboBox2.Items.Add(reader["name"]);
                }
                comboBox2.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Error");
            }


            connection.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;

            string address2 = "Data Source = (local); Initial Catalog = practice; Integrated Security = SSPI";
            SqlConnection connection2 = new SqlConnection(address2);

            connection2.Open();

            string sqlText2 = "select * from players"; //1
            //string sqlText2 = $"insert into players (name) values('{text}')";

            SqlCommand command = new SqlCommand(sqlText2, connection2); //1

            SqlDataReader reader2 = command.ExecuteReader(); //1
            reader2.Close(); //1

            //command.ExecuteNonQuery();

            sqlText2 = $"insert into players (name) values('{text}')"; //1
            command.CommandText = sqlText2; //1


            //1
            #region FillComboBoxAfterInserting
            string address = "Data Source = (local); Initial Catalog = practice; Integrated Security=SSPI";
            SqlConnection connection = new SqlConnection(address);
            connection.Open();

            //MessageBox.Show("Opened");
            //MessageBox.Show("Closed");

            string sqlText = "select * from players";
            SqlCommand sqlCommand = new SqlCommand(sqlText, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //comboBox1.Items.Clear();
                    comboBox1.Items.Add(reader["name"]);
                }
            }
            else
            {
                MessageBox.Show("Unknown error occured");
            }


            connection.Close();
            #endregion


            connection2.Close();
        }
    }
}
