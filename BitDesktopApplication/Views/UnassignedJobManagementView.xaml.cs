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
    /// Interaction logic for UnassignedJobManagementView.xaml
    /// </summary>
    public partial class UnassignedJobManagementView : Page
    {
        private UnassignedJobManagementViewModel _unJobManVM;
        public UnassignedJobManagementView()
        {
            InitializeComponent();
            _unJobManVM = new UnassignedJobManagementViewModel();
            this.DataContext = _unJobManVM;
        }

        private void btnAssignContractor_Click(object sender, RoutedEventArgs e)
        {
            if (dgUnassignedJobs.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Job. ", "Selection Required");

            }
            else
            {
                NavigationService.Navigate(new AddJobContractorView(_unJobManVM.SelectedJob));
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UnassignedJobManagementView());
        }

        private void cmboSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmboSearch.SelectedIndex == 1)
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
    }
}
