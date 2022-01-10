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
    /// Interaction logic for RejectedJobManagementView.xaml
    /// </summary>
    public partial class RejectedJobManagementView : Page
    {
        private RejectedJobManagementViewModel _jobVM;
        public RejectedJobManagementView()
        {
            InitializeComponent();
            _jobVM = new RejectedJobManagementViewModel();
            this.DataContext = _jobVM;
        }

        private void btnReassign_Click(object sender, RoutedEventArgs e)
        {
            if (dgRejectedJobs.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Job. ", "Selection Required");

            }
            else
            {
                NavigationService.Navigate(new AddJobContractorView(_jobVM.SelectedJob));
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RejectedJobManagementView());
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
