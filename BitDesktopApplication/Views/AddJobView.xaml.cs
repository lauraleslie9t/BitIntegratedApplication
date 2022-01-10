using BitDesktopApplication.Models;
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
    /// Interaction logic for AddJobView.xaml
    /// </summary>
    public partial class AddJobView : Page
    {
        private Client _client;
        private AddJobViewModel _addJobVM;

        public AddJobView(Client client)
        {
            this._client = client;
            this._addJobVM = new AddJobViewModel(_client);
            InitializeComponent();
            this.DataContext = _addJobVM;
        }
        //public AddJobView(Job job)
        //{
            
        //    this._addJobVM = new AddJobViewModel(job);
        //    InitializeComponent();
        //    this.DataContext = _addJobVM;
        //}

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (dgLocations.SelectedIndex != -1)
            {
                if (ValidateForm())
                {
                    NavigationService.Navigate(new AddJobContractorView(_addJobVM.Job));
                }
                else
                {
                    MessageBox.Show("Unable to save job. Please check form.");
                }
            }
            else
            {
                MessageBox.Show("Please select a job location. ");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientManagementView());
        }

        private bool ValidateForm()
        {
            if (cmboPriority.SelectedIndex == -1)
            {
                return false;
            }
            if (dtpkrRequiredCompDate.SelectedDate == null | dtpkrRequiredCompDate.SelectedDate < DateTime.Now.AddDays(1))
            {
                return false;
            }
            if (txtDescription.Text.Length == 0 | txtDescription.Text.Length > 500)
            {
                return false;
            }
            return true;
        }
    }
}
