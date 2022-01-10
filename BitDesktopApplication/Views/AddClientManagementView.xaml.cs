using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using BitDesktopApplication.ViewModels;

namespace BitDesktopApplication.Views
{
    /// <summary>
    /// Interaction logic for AddClientManagementView.xaml
    /// </summary>
    public partial class AddClientManagementView : Page
    {

        AddClientViewModel clientVM;

        public AddClientManagementView()
        {
            InitializeComponent();
            clientVM = new AddClientViewModel();
            this.DataContext = clientVM;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                NavigationService.Navigate(new ClientManagementView());
            }
            else
            {
                MessageBox.Show("Unable to save client. Please check form.");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientManagementView());
        }

        private void btnAddLocation_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                NavigationService.Navigate(new AddLocationManagementView(clientVM.Client));
            }
            else
            {
                MessageBox.Show("Unable to save client. Please check form.");
            }
        }
        Regex regex = new Regex("^[0-9]+$"); 
        private bool ValidateForm()
        {
            if (txtCompanyName.Text.Length > 50)
            {
                return false;
            }
            if (cmboTitle.SelectedIndex == -1)
            {
                return false;
            }
            if (txtFirstName.Text.Length > 50 | txtFirstName.Text.Length == 0)
            {
                return false;
            }
            if (txtLastName.Text.Length > 50 | txtLastName.Text.Length == 0)
            {
                return false;
            }
            if (dtpkDob.SelectedDate == null | dtpkDob.SelectedDate == Convert.ToDateTime("01/01/0001"))
            {
                return false;
            }
            if (txtHomePhone.Text.Length > 10 | txtHomePhone.Text.Length == 0)
            {
                return false;
            }
            if (txtMobile.Text.Length > 10)
            {
                return false;
            }
            if (txtComments.Text.Length > 500)
            {
                return false;
            }
            if (!regex.IsMatch(txtHomePhone.Text) )
            {
                return false;
            }
            if (txtMobile.Text != "")
            {
                if (!regex.IsMatch(txtMobile.Text))
                {
                    return false;
                }
            }
            return true;
        }

        //private bool NumericalChecker(string myNumber)
        //{
        //    foreach (char c in myNumber)
        //    {
        //        if (char.IsDigit(c))
        //        {
        //            return false;
        //        }

        //    }
        //    return true;
        //}
    }
}
