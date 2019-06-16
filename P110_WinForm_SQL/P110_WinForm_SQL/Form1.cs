using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; //MSSQL server
using System.Configuration;

namespace P110_WinForm_SQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCountries();
        }

        private void cmbCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCities.Items.Clear();

            string connString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (var conn = new SqlConnection(connString))
            {
                string country = cmbCountries.Text;

                string comText = "select Cities.Id, Cities.Name " +
                                 "from Cities " +
                                 "join Countries on Cities.CountryId = Countries.Id " +
                                $"where Countries.Name = '{country}'";

                using (var comm = new SqlCommand(comText, conn))
                {
                    conn.Open();
                    var reader = comm.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            cmbCities.Items.Add(reader["Name"]);
                        }
                        cmbCities.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show($"There is no City for this {country}");
                    }
                }
            }
        }

        private void btnAddCountry_Click(object sender, EventArgs e)
        {
            string newCountryName = txtCountryName.Text.Trim();

            if(newCountryName != string.Empty)
            {
                //check country name is duplicate
                string connString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

                using (var conn = new SqlConnection(connString))
                {
                    string commText = $"select * from Countries where Name = '{newCountryName}'";

                    using (var comm = new SqlCommand(commText, conn))
                    {
                        conn.Open();

                        SqlDataReader reader = comm.ExecuteReader();
                        if(reader.HasRows)
                        {
                            //country name is duplicate
                            MessageBox.Show("This country already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();
                            return;
                        }
                        reader.Close();

                        //then add this country to database
                        commText = $"insert into Countries values('{newCountryName}')";
                        comm.CommandText = commText;

                        int rowsAffected = comm.ExecuteNonQuery();
                        if(rowsAffected > 0)
                        {
                            MessageBox.Show($"{newCountryName} is successfully added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCountryName.Text = "";
                            LoadCountries();
                            return;
                        }
                    }
                }

            }
            else
            {
                MessageBox.Show("Country name should be filled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void LoadCountries()
        {
            cmbCountries.Items.Clear();
            string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            #region SQLCOnnection old way
            //SqlConnection conn = null;
            //try
            //{
            //    conn = new SqlConnection(connectionString);
            //    conn.Open();
            //}
            //catch (SqlException ex)
            //{
            //    MessageBox.Show($"Unknown error occured while connection to database. Error details: {ex.Number} {ex.Message}");
            //}
            //finally
            //{
            //    conn.Close();
            //}
            #endregion

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string commandText = "SELECT * FROM Countries";

                using (SqlCommand sqlCommand = new SqlCommand(commandText, conn))
                {
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //MessageBox.Show(reader[0].ToString());
                            cmbCountries.Items.Add(reader["Name"]);
                        }
                        cmbCountries.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("There is no Country in the list.");
                    }
                }
            }
        }
    }
}
