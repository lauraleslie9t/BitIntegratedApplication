using BitDesktopApplication.Models;
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
using System.Windows.Shapes;

namespace BitDesktopApplication
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        public LoginPage()
        {
            InitializeComponent();


            //((MainWindow)System.Windows.Application.Current.MainWindow).btnClientsPage.IsEnabled = false;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login userLogin = new Login();
            userLogin.Username = txtUsername.Text.Trim();
            userLogin.Password = txtpassword.Password;
            if (userLogin.LoginUser())
            {
                MainWindow mainPage = new MainWindow(userLogin);
                mainPage.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Denied Access.");
                txtpassword.Clear();
                txtUsername.Clear();
                txtUsername.Focus();
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtUsername_GotFocus(object sender, RoutedEventArgs e)
        {
            txtUsername.SelectAll();
        }
    }
}
