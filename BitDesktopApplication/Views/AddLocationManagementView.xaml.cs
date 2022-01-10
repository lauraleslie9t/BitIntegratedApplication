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
using BitDesktopApplication.Models;
using BitDesktopApplication.ViewModels;

namespace BitDesktopApplication.Views
{
    /// <summary>
    /// Interaction logic for AddLocationManagementView.xaml
    /// </summary>
    public partial class AddLocationManagementView : Page
    {
        private Client _client;
        public AddLocationManagementView(Client client)
        {
            this._client = client;
            InitializeComponent();
            this.DataContext = new AddClientLocationViewModel(client);
        }

        private void btnAddLocation_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                NavigationService.Navigate(new AddLocationManagementView(_client));
            }
            
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                NavigationService.Navigate(new ClientManagementView());
            }   
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientManagementView());
        }
        Regex regex = new Regex("^[0-9]+$");
        private bool ValidateForm()
        {
            if(txtLocationName.Text.Length > 100)
            {
                return false;
            }
            if (txtPhone.Text.Length > 10)
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
            if (cmboRegions.SelectedIndex == -1)
            {
                return false;
            }
            if (!regex.IsMatch(txtPhone.Text) | !regex.IsMatch(txtPostCode.Text))
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
