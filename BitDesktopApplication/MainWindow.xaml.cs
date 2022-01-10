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
using BitDesktopApplication.Models;
using BitDesktopApplication.Views;


namespace BitDesktopApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Stack<Object> previousPages = new Stack<Object>();
        Stack<Object> nextPages = new Stack<Object>();
        FileLogger logFile = new FileLogger();
        public MainWindow()
        {
            InitializeComponent();
            //home page on initiation
            contentFrame.Navigate(new HomePageView());
            LogPageChange(new HomePageView());
        }
        public MainWindow(Login login)
        {
            InitializeComponent();
            //home page on initiation
            contentFrame.Navigate(new HomePageView());
            LogPageChange(new HomePageView());
            if (login.LastName != null && login.LastName != "" )
            {
                txtblLogoutButton.Text = login.FirstName + " " + login.LastName;
                if (login.UserType == "Administrator")
                {
                    btnCoordinatorPage.Visibility = Visibility.Visible;
                }
                else if (login.UserType == "Coordinator")
                {
                    btnCoordinatorPage.Visibility = Visibility.Collapsed;
                }
            }
        }

        #region Public Methods
        /// <summary>
        /// Logs the page change by recording the page object in the logger and the page history stack. 
        /// </summary>
        /// <param name="page">Object: The page object to be recorded.</param>
        public void LogPageChange(object page)
        {

            logFile.LogClickedPages(page.ToString());
            previousPages.Push(page);
            if (previousPages.Count > 1)
            {
                btnPreviousPage.IsEnabled = true;
            }
        }
        /// <summary>
        /// Clears the next pages stack and disables the next button on a page change.  
        /// </summary>
        private void ResetNextButton()
        {
            nextPages.Clear();
            btnNextPage.IsEnabled = false;
        }
        #endregion Public Methods

        #region TopBarButtons
        
        private void btnToggleMenu_Click(object sender, RoutedEventArgs e)
        {
            if (gridSideBar.Visibility != Visibility.Hidden)
            {
                gridSideBar.Visibility = Visibility.Hidden;
                gridFrame.SetValue(Grid.ColumnProperty, 0);
                gridFrame.SetValue(Grid.ColumnSpanProperty, 2);
            }
            else
            {
                gridSideBar.Visibility = Visibility.Visible;
                gridFrame.SetValue(Grid.ColumnProperty, 1);
                gridFrame.SetValue(Grid.ColumnSpanProperty, 1);
            }
        }
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new HomePageView());
            LogPageChange(new HomePageView());
        }
        private void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            //TODO need to click twice to get the first page move going. NEed to rethink/restructure this and the following function. 
            btnNextPage.IsEnabled = true;
            nextPages.Push(previousPages.Pop());
            if(previousPages.Count != 0)
            {
                contentFrame.Navigate(previousPages.Peek());

            }
            if (nextPages.Count == 0)
            {

                btnNextPage.IsEnabled = false;
            }
            else
            {
                btnNextPage.IsEnabled = true;
            }
            if (previousPages.Count == 0)
            {
                btnPreviousPage.IsEnabled = false;
            }
            else
            {
                btnPreviousPage.IsEnabled = true;
            }
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            
            previousPages.Push(nextPages.Pop());
            if(nextPages.Count != 0)
            {
                contentFrame.Navigate(nextPages.Peek());
            }
            if (nextPages.Count == 0)
            {

                btnNextPage.IsEnabled = false;
            }
            else
            {
                btnNextPage.IsEnabled = true;
            }
            if (previousPages.Count == 0)
            {
                btnPreviousPage.IsEnabled = false;
            }
            else
            {
                btnPreviousPage.IsEnabled = true;
            }
        }
        #endregion TopBarButtons

        #region SideBarButtons
        private void btnContractorPage_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new ContractorManagementView());
            LogPageChange(new ContractorManagementView());
            ResetNextButton();
        }


        private void btnClientsPage_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new ClientManagementView());
            LogPageChange(new ClientManagementView());
            ResetNextButton();
        }
        private void btnCoordinatorPage_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new CoordinatorManagementView());
            LogPageChange(new CoordinatorManagementView());
            ResetNextButton();
        }
        #endregion SideBarButtons

        private void btnCompletedJobs_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new CompletedJobManagementView());
            LogPageChange(new CompletedJobManagementView());
            ResetNextButton();
        }

        private void btnUnassignedJobs_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new UnassignedJobManagementView());
            LogPageChange(new UnassignedJobManagementView());
            ResetNextButton();
        }

        private void btnAllJobs_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new JobManagementView());
            LogPageChange(new JobManagementView());
            ResetNextButton();
        }

        private void btnRejectedJobsPage_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new RejectedJobManagementView());
            LogPageChange(new RejectedJobManagementView());
            ResetNextButton();
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            LoginPage logout = new LoginPage();
            logout.Show();
            this.Close();

        }
    }
}
