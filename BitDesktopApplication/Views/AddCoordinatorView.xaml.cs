using BitDesktopApplication.ViewModels;
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

namespace BitDesktopApplication.Views
{
    /// <summary>
    /// Interaction logic for AddCoordinatorViewModel.xaml
    /// </summary>
    public partial class AddCoordinatorView : Page
    {
        public AddCoordinatorView()
        {
            InitializeComponent();
            this.DataContext = new AddCoordinatorViewModel();
        }

       

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CoordinatorManagementView());
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                NavigationService.Navigate(new CoordinatorManagementView());
            }
            else
            {
                MessageBox.Show("Unable to save coordinator. Please check form.");
            }
        }
        Regex regex = new Regex("^[0-9]+$");
        private bool ValidateForm()
        {
            
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
            if (txtHomePhone.Text.Length > 10)
            {
                return false;
            }
            if (txtMobile.Text.Length > 10)
            {
                return false;
            }
            if (txtStreet.Text.Length > 50 | txtStreet.Text.Length == 0)
            {
                return false;
            }
            if (txtSuburb.Text.Length > 20 | txtSuburb.Text.Length == 0)
            {
                return false;
            }
            if (txtPostCode.Text.Length > 4 | txtPostCode.Text.Length == 0)
            {
                return false;
            }
            if (cmboState.SelectedIndex == -1)
            {
                return false;
            }
            if (txtCountry.Text.Length > 74 | txtCountry.Text.Length == 0)
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
            if (txtHomePhone.Text != "")
            {
                if (!regex.IsMatch(txtHomePhone.Text))
                {
                    return false;
                }
            }
            if (!regex.IsMatch(txtPostCode.Text))
            {
                return false;
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
