using BitDesktopApplication.ViewModels;
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

namespace BitDesktopApplication.Views
{
    /// <summary>
    /// Interaction logic for ClientManagementView.xaml
    /// </summary>
    public partial class ClientManagementView : Page
    {
        private ClientManagementViewModel _clientVM;

        public ClientManagementView()
        {
            InitializeComponent();
            _clientVM = new ClientManagementViewModel();
            this.DataContext = _clientVM;
        }

        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddClientManagementView());
        }

        private void btnAddJob_Click(object sender, RoutedEventArgs e)
        {
            if (dgClients.SelectedIndex == -1)
            {
                
                MessageBox.Show("Please select a client to book a job for. ", "Selection Required");
            }
            else
            {
                if (_clientVM.ClientLocations.Count == 0)
                {
                    MessageBox.Show("Clients need a location to book a job. ", "Location Required");
                }
                else
                {
                    NavigationService.Navigate(new AddJobView(_clientVM.SelectedClient));
                }
            }
        }

        private void btnAddLocation_Click(object sender, RoutedEventArgs e)
        {
            if (dgClients.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a client to add a location to. ", "Selection Required");
            }
            else
            {
                NavigationService.Navigate(new AddLocationManagementView(_clientVM.SelectedClient));
            }
        }
        //to set which searchbox is to be used. 
        private void cmboSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmboSearch.SelectedIndex == 3)
            {
                dtpkrBirthDate.Visibility = Visibility.Visible;
                txtSearch.Visibility = Visibility.Hidden;
            }
            else
            {
                dtpkrBirthDate.Visibility = Visibility.Hidden;
                txtSearch.Visibility = Visibility.Visible;
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientManagementView());
        }
    }
}
