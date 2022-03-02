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

namespace MD_Hansons
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const string pathToDatabase = @"C:\Users\mvhan\source\repos\MD_Hansons\MD_Hansons\baze.mdf";
        string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={pathToDatabase};Integrated Security=True";

        private void izveidot_db()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            const string createWheels = "create table Wheels(" +
            "ID_Wheel int identity primary key, " +
            "W_name nvarchar(50), " +
            "W_diam float, " +
            "W_type nvarchar(50), " +
            "W_wide int," +
            "W_price int" +
            "ID_Bike int" +
            "CONSTRAINT Bike_wheel FOREIGN KEY (ID_Bike) " +
            "REFERENCES Bikes (ID_Bike)" +
            ");";

            const string createBikes = "create table Bikes(" +
            "ID_Bike int identity primary key, " +
            "B_Name nvarchar(50), " +
            "B_Model nvarchar(50), " +
            "B_lenght int, " +
            "B_width int, " +
            ");";

            SqlCommand cmd = new SqlCommand();
            try
            {
                sqlConnection.Open();
                cmd.Connection = sqlConnection;
                cmd.CommandText = createBikes;
                cmd.ExecuteNonQuery();
                cmd.CommandText = createWheels;
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch
            {

            }
            check_if_exists();
        }

        private bool check_if_exists()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            const string test_Wheels = "select *from Wheels";
            const string test_Bikes = "select *from Bikes";
            SqlDataAdapter W_adapter = new SqlDataAdapter(test_Wheels, connection);
            SqlDataAdapter B_adapter = new SqlDataAdapter(test_Bikes, connection);
            DataSet W_ds = new DataSet();
            DataSet B_ds = new DataSet();

            try
            {
                connection.Open();
                W_adapter.Fill(W_ds);
                B_adapter.Fill(B_ds);
                connection.Close();
            }
            catch
            {
                return false;
            }

            BindingSource W_Bind = new BindingSource();
            BindingSource B_Bind = new BindingSource();
            W_Bind.DataSource = W_ds.Tables[0].DefaultView;
            B_Bind.DataSource = B_ds.Tables[0].DefaultView;
            bindingNavigator1.BindingSource = W_Bind;
            bindingNavigator2.BindingSource = B_Bind;
            tabula.DataSource = W_Bind;
            tabulina.DataSource = B_Bind;

            return true;
        }



        private void clear_Click(object sender, EventArgs e)
        {

        }

        private void test_Click(object sender, EventArgs e)
        {
            if (check_if_exists() == false) izveidot_db();
            SqlConnection connection = new();

        }

        private void load_Click(object sender, EventArgs e)
        {
            if (check_if_exists() == false) izveidot_db();
        }

        private void saglabat_Click(object sender, EventArgs e)
        {

        }

        private void izveidot_Click(object sender, EventArgs e)
        {
            if (check_if_exists() == false) izveidot_db();
        }
    }
}
