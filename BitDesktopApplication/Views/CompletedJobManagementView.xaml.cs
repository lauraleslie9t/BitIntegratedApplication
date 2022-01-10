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
    /// Interaction logic for CompletedJobManagementView.xaml
    /// </summary>
    public partial class CompletedJobManagementView : Page
    {
        public CompletedJobManagementView()
        {
            InitializeComponent();
            this.DataContext = new CompletedJobManagementViewModel();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CompletedJobManagementView());
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
