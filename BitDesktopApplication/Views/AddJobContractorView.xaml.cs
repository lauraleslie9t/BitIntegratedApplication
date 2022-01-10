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
    /// Interaction logic for AddJobContractorView.xaml
    /// </summary>
    public partial class AddJobContractorView : Page
    {
        private AddJobContractorViewModel _newJobVM;
        public AddJobContractorView(Job job)
        {
            InitializeComponent();
            _newJobVM = new AddJobContractorViewModel(job);
            this.DataContext = _newJobVM; 
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (dgAvailableContractors.SelectedIndex != -1)
            {
                if (ValidateForm())
                {
                    if (_newJobVM.CheckStartTime())
                    {
                        NavigationService.Navigate(new JobManagementView());

                    }
                    else
                    {
                        MessageBox.Show("Start time is set before availability period.");
                    }
                }
                else
                {
                    MessageBox.Show("Unable to save job. Please check form.");
                }
            }
            else
            {
                MessageBox.Show("Please enter the required skills and select an available session. ");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new JobManagementView());
        }

        private bool ValidateForm()
        {
            if (cmboStartTime.SelectedIndex == -1)
            {
                return false;
            }
            if (!int.TryParse(txtExpHours.Text, out int hours) | hours <= 0)
            {
                return false;
            }

            return true;
        }

        //private bool CheckStartTime()
        //{
        //    if (dgAvailableContractors.SelectedItem != null)
        //    {
        //        string[] timeSplit = StartTime.Split(':');
        //        int startTime = Convert.ToInt32(timeSplit[0]);
        //        if (Job.StartTime != null)
        //        {
        //            string[] selectedtimeSplit = Job.StartTime.Split(':');
        //            int selectedStartTime = Convert.ToInt32(timeSplit[0]);
        //            if (selectedStartTime >= startTime)
        //            {
        //                return true;
        //            }
        //            return false;
        //        }
        //        return false;
        //    }
        //    return false;
        //}

        //private void btnEdit_Click(object sender, RoutedEventArgs e)
        //{

        //    NavigationService.Navigate(new AddJobView(_newJobVM.Job));
        //}
    }
}
