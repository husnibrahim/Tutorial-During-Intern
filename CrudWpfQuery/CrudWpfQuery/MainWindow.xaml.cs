using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace CrudWpfQuery
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //public string conString = "Data Source=DESKTOP-KI0MTE5;Initial Catalog=dbOne;Integrated Security=True";
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-IO9KEIP6\SQLEXPRESS;Initial Catalog=Crud;Integrated Security=True");
        //SqlCommand command = new SqlCommand();
        SqlDataReader dataSearch;

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {

           

        }
        private void search()//view database
        {
            listBox01.Items.Clear();
            listBox02.Items.Clear();
            listBox03.Items.Clear();

            con.Open();
         //   string q = "Select * from newRegs";
            SqlCommand cmd = new SqlCommand("VIEWnewRegs", con);
            dataSearch = cmd.ExecuteReader();
            if (dataSearch.HasRows)
            {
                while (dataSearch.Read())
                {
                    listBox01.Items.Add(dataSearch["lastName"].ToString());
                    listBox02.Items.Add(dataSearch["firstName"].ToString());
                    listBox03.Items.Add(dataSearch["icNumber"].ToString());
                }
            }
            con.Close();
        }


        private void listBox03_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox list = sender as ListBox;
            if (list.SelectedIndex != -1)
            {
                listBox01.SelectedIndex = list.SelectedIndex;
                listBox02.SelectedIndex = list.SelectedIndex;
                listBox03.SelectedIndex = list.SelectedIndex;

                lastName.Text = listBox01.SelectedItem.ToString();
                firstName.Text = listBox02.SelectedItem.ToString();
                icNumber.Text = listBox03.SelectedItem.ToString();

            }
        }
      

        private void submitButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (lastName.Text == "" || firstName.Text == "" || icNumber.Text == "")
            {
                MessageBox.Show("Please fill in the empty fields");
            }

            else
            {
                //SqlConnection con = new SqlConnection(conString);
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    //command.CommandText = "insert into newReg(lastName,firstName, icNumber) values('" + lastName.Text.ToString() + "','" + firstName.Text.ToString() + "','" + icNumber.Text.ToString() + "')";
                    string q = "insert into newRegs(lastName,firstName, icNumber)values('" + lastName.Text.ToString() + "','" + firstName.Text.ToString() + "','" + icNumber.Text.ToString() + "')";
                    SqlCommand cmd = new SqlCommand("CREATEnewRegs", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@firstName", firstName.Text);
                    cmd.Parameters.AddWithValue("@lastName", lastName.Text);
                    cmd.Parameters.AddWithValue("@icNumber", icNumber.Text);
                    cmd.ExecuteNonQuery();
                    //command.ExecuteNonQuery();
                    MessageBox.Show("Registration is successful");
                    lastName.Clear();
                    firstName.Clear();
                    icNumber.Clear();
                }
                con.Close();
            }
        }

        private void update_Click_1(object sender, RoutedEventArgs e)
        {
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                string q = "Update newRegs set lastName='" + lastName.Text + "', firstName='" + firstName.Text + "', icNumber='" + icNumber.Text + "' where lastName='" + listBox01.SelectedItem.ToString() + "' and  firstName='" + listBox02.SelectedItem.ToString() + "' and icNumber='" + listBox03.SelectedItem.ToString() + "'";
                SqlCommand cmd = new SqlCommand("UPDATEnewRegs", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@firstName", firstName.Text);
                cmd.Parameters.AddWithValue("@lastName",lastName.Text);
                cmd.Parameters.AddWithValue("@icNumber", icNumber.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Update is successful");
                //lastName.Clear();
                //firstName.Clear();
                //icNumber.Clear();
                //search();
            }
            con.Close();
            MessageBox.Show("Update is successful");
            lastName.Clear();
            firstName.Clear();
            icNumber.Clear();
            search();
        }

        private void view_Click_1(object sender, RoutedEventArgs e)
        {
            search();
        }

        private void delete_Click_1(object sender, RoutedEventArgs e)
        {
            if (icNumber.Text != "")
            {
                con.Open();
                string q = "Delete from newRegs where icNumber='" + icNumber.Text + "'";
                SqlCommand cmd = new SqlCommand("DELETEnewRegs", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@icNumber", icNumber.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Delete is successful");
                con.Close();
                lastName.Clear();
                firstName.Clear();
                icNumber.Clear();
                search();
            }
        }
    }
}
