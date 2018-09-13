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

namespace Turbo_Az
{
    public partial class Form1 : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["TurboAzConnectionString"].ConnectionString;

        public Form1()
        {
            InitializeComponent();
            FillMarkaCmb();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new MarkaForm().Show(); 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int affectedRow = 0;
            int marka_id = Convert.ToInt32((cmbMarkaId.SelectedItem as ComboItem).Value);
            int model_id = Convert.ToInt32((cmbModelId.SelectedItem as ComboItem).Value);
            float price;
            if(!float.TryParse(txtPrice.Text, out price))
            {
                MessageBox.Show("Enter valid prive");
                return;
            }
            DateTime year = dtYear.Value;
            
            using(SqlConnection connection =new SqlConnection(connectionString))
            {
                connection.Open();
                string car_query = "INSERT INTO Cars(Marka_id, Model_id, Year, Price) VALUES(@marka_id, @model_id, @year, @price)";
                using(SqlCommand command =new SqlCommand(car_query, connection))
                {
                    command.Parameters.AddWithValue("@marka_id", marka_id);
                    command.Parameters.AddWithValue("@model_id", model_id);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@price", price);
                    affectedRow = command.ExecuteNonQuery();
                }
            }
            MessageBox.Show(affectedRow > 0 ? "Succes" : "Error");
        } 



        void FillMarkaCmb()
        {
            cmbMarkaId.Items.Clear();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string marka_query = "SELECT * FROM Markas";
                using(SqlCommand command =new SqlCommand(marka_query, connection))
                {
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        cmbMarkaId.Items.Add(new ComboItem(dr["Name"].ToString(), dr["Id"].ToString()));
                    }

                }
            }
        }

        private void cmbMarkaId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int markaId = Convert.ToInt32((cmbMarkaId.SelectedItem as ComboItem).Value);
            FillModelCmbById(markaId);
        }

        void FillModelCmbById(int markaId)
        {
            cmbModelId.Items.Clear();

            using(SqlConnection connection =new SqlConnection(connectionString))
            {
                connection.Open();
                string model_query = "SELECT * FROM Models WHERE Marka_id = @markaId";
                using(SqlCommand command =new SqlCommand(model_query, connection))
                {
                    command.Parameters.AddWithValue("@markaId", markaId);
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        cmbModelId.Items.Add(new ComboItem(dr["Name"].ToString(), dr["Id"].ToString()));
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new SearchForm().Show();
        }
    }
}
