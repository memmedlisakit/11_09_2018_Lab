using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Turbo_Az
{
    public partial class MarkaForm : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["TurboAzConnectionString"].ConnectionString;

        public MarkaForm()
        {
            InitializeComponent();
            FillCombobox();
        }

        private void btnAddModel_Click(object sender, EventArgs e)
        {
            int affectedRow = 0;
            using(SqlConnection connection =new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Markas(Name) VALUES(@name)";
                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", txtMarka.Text);
                    affectedRow = command.ExecuteNonQuery();
                }
            }
            MessageBox.Show(affectedRow > 0 ? "Success" : "Error");
            FillCombobox();
        }

        private void FillCombobox()
        {
            cmbMarka.Items.Clear();
            using(SqlConnection connection =new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Markas";
                using (SqlCommand command =new SqlCommand(query, connection))
                {
                   using(SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            cmbMarka.Items.Add(new ComboItem(dr[1].ToString(), dr[0].ToString()));
                        }
                    }
                }
            }
        }
    }
}
