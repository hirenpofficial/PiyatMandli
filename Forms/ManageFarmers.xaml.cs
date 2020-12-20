using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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

namespace PiyatMandli.Forms
{
    /// <summary>
    /// Interaction logic for ManageFarmers.xaml
    /// </summary>
    public partial class ManageFarmers : Page
    {
        FarmerManager manager;
        public ManageFarmers()
        {
            manager = new FarmerManager();
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (CustomPager.CurrentPage == 0)
            {
                CustomPager.CurrentPage = 1;
            }
            CBPageCount.SelectedIndex = 1;
            this.Height = SystemParameters.PrimaryScreenHeight;
            LSTRecord.Height = this.Height - 300;
            FillList();
        }

        public void FillList()
        {
            if (CustomPager != null)
            {
                FillList(CustomPager.CurrentPage, Convert.ToInt32(((System.Windows.Controls.ContentControl)(CBPageCount.SelectedItem)).Content.ToString().ToEnglish()), txtSearchString.Text.Trim(), null, false);
            }
        }

        private void FillList(int startIndex = -1, int fetchRecords = -1, string searchString = "", bool? isActive = null, bool? isDelete = false)
        {
            try
            {
                FARefresh.Visibility = Visibility.Visible;
                startIndex = (startIndex - 1) * Convert.ToInt32(((System.Windows.Controls.ContentControl)(CBPageCount.SelectedItem)).Content.ToString().ToEnglish());
                LSTRecord.ItemsSource = null;
                GenericRecordList<Farmer_model> GRL = manager.GetAll(null, startIndex, fetchRecords, searchString, isActive, isDelete);
                LSTRecord.ItemsSource = GRL.RecordList;
                FARefresh.Visibility = Visibility.Hidden;
                //CHKAll.IsChecked = false;

                CustomPager.ShowPager(Convert.ToInt32(GRL.TotalRecords), CustomPager.CurrentPage, Convert.ToInt32(((System.Windows.Controls.ContentControl)(CBPageCount.SelectedItem)).Content.ToString().ToEnglish()));
            }
            catch (Exception ex)
            {
                FARefresh.Visibility = Visibility.Hidden;
            }
        }

        private void DPFooter_Click_1(object sender, RoutedEventArgs e)
        {
            FillList();
        }

        private void CBPageCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillList();
        }

        private void BTNSearch_Click(object sender, RoutedEventArgs e)
        {
            FillList();
        }

        private void BTNAdd_Click(object sender, RoutedEventArgs e)
        {
            if (new AddEditFarmer().ShowDialog() == true)
            {
                FillList();
            }
        }

        private async void BTNDelete_Click(object sender, RoutedEventArgs e)
        {
            var record = (Farmer_model)LSTRecord.SelectedItem;
            if (record != null)
            {
                var metroWindow = (Application.Current.MainWindow as MetroWindow);
                if(await metroWindow.ShowMessageAsync(AppManager.Constants.ApplicatioName,$"શું તમે \"{record.Name}\"ની માહિતી કાઢવા માંગો છો?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
                {
                    if (manager.Delete(record.Id).ReturnCode == ResponseMessages.SuccessCode)
                    {
                        FillList();
                    }
                    else
                    {
                        await metroWindow.ShowMessageAsync(AppManager.Constants.ApplicatioName, $"ખેડૂતની માહિતી કાઢવામાં સમસ્યા છે.", MessageDialogStyle.Affirmative);
                    }
                }
            }
        }

        private void BTNEdit_Click(object sender, RoutedEventArgs e)
        {
            var record = (Farmer_model)LSTRecord.SelectedItem;
            if (record != null)
            {
                if(new AddEditFarmer(record.Id).ShowDialog() == true)
                {
                    FillList();
                }
            }
        }
    }
}
