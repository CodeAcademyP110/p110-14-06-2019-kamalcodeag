using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeTask
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //string address = "Data Source = (local); Initial Catalog = 'iyun16'; Integrated Security = SSPI";
            //SqlConnection connection = new SqlConnection(address);

            string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);


            connection.Open();

            string sqlText = "select name from marks";
            SqlCommand command = new SqlCommand(sqlText,connection);

            SqlDataReader reader = command.ExecuteReader();

            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    comboBox1.Items.Add(reader["name"]);
                }
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Error occured");
            }

            connection.Close();

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = comboBox1.Text;

            //string address = "Data Source = (local); Initial Catalog = 'iyun16'; Integrated Security = SSPI";
            //SqlConnection connection = new SqlConnection(address);

            string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            string sqlText = $"select models.name from models join marks on models.marksId = marks.Id where marks.name = '{text}'";

            SqlCommand command = new SqlCommand(sqlText, connection);
            SqlDataReader reader = command.ExecuteReader();

            if(reader.HasRows)
            {
                comboBox2.Items.Clear();

                while (reader.Read())
                {
                    comboBox2.Items.Add(reader["name"]);
                }
               
            }
            else
            {
                MessageBox.Show("Error occured 2");
            }

            connection.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text;
            //string address2 = "Data Source = (local); Initial Catalog = 'iyun16'; Integrated Security = SSPI";
            //SqlConnection connection2 = new SqlConnection(address2);

            string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            SqlConnection connection2 = new SqlConnection(connectionString);

            connection2.Open();

            string sqlText2 = $"insert into marks (name) values('{input}')";
            SqlCommand command3 = new SqlCommand(sqlText2, connection2);
            SqlDataReader reader3 = command3.ExecuteReader();



            #region FillComboBoxAfterInsert
            //string address = "Data Source = (local); Initial Catalog = 'iyun16'; Integrated Security = SSPI";
            //SqlConnection connection = new SqlConnection(address);

            string connectionString2 = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString2);

            connection.Open();

            string sqlText = "select name from marks";
            SqlCommand command = new SqlCommand(sqlText, connection);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                comboBox1.Items.Clear();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["name"]);
                }
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Error occured");
            }

            connection.Close();
            #endregion

            connection2.Close();

        }
    }
}
