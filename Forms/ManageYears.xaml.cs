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
    /// Interaction logic for ManageWindows.xaml
    /// </summary>
    public partial class ManageYears : Page
    {
        YearManager manager;
        int _selectedId;
        List<Year_model> years;
        public ManageYears()
        {
            InitializeComponent();
            manager = new YearManager();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DisableEntryControls();
            FillList();
        }

        void DisableEntryControls()
        {
            STPEntry.IsEnabled = false;
            STPList.IsEnabled = true;
        }
        void EnableEntryControl()
        {
            STPEntry.IsEnabled = true;
            STPList.IsEnabled = false;
        }
        void ClearForm()
        {
            TXTYear.Text = string.Empty;
            TXTRate.Text = string.Empty;
            DTPClosingDate.SelectedDate = null;
            DTPClosingDate.TXTDatePicker.Text = string.Empty;
        }
        void FillList()
        {
            var result = manager.GetAll();
            LSTRecords.ItemsSource = result.RecordList;
            years = result.RecordList;
            LSTRecords.DisplayMemberPath = "Year";
            LSTRecords.SelectedValuePath = "Id";
            LSTRecords.UpdateLayout();
            if (_selectedId > 0)
            {
                LSTRecords.SelectedValue = _selectedId;
            }
            else if (LSTRecords.Items.Count > 0)
            {
                LSTRecords.SelectedIndex = 0;
            }
        }

        private void LSTRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var record = (Year_model)LSTRecords.SelectedItem;
            if (record != null)
            {
                TXTYear.Text = record.Year;
                TXTRate.Text = record.Rate;
                DTPClosingDate.SelectedDate = Convert.ToDateTime(record.ClosingDate.ToEnglish());
            }
        }

        private void BTNAdd_Click(object sender, RoutedEventArgs e)
        {
            _selectedId = 0;
            EnableEntryControl();
            ClearForm();
            TXTYear.Focus();
        }

        private void BTNEdit_Click(object sender, RoutedEventArgs e)
        {
            var record = (Year_model)LSTRecords.SelectedItem;
            if (record != null)
            {
                _selectedId = record.Id;
                TXTYear.Text = record.Year;
                TXTRate.Text = record.Rate;
                DTPClosingDate.SelectedDate = Convert.ToDateTime(record.ClosingDate.ToEnglish());
                EnableEntryControl();
            }
        }

        private async void BTNDelete_Click(object sender, RoutedEventArgs e)
        {
            var record = (Year_model)LSTRecords.SelectedItem;
            if (record != null)
            {
                var metroWindow = (Application.Current.MainWindow as MetroWindow);
                if (await metroWindow.ShowMessageAsync(AppManager.Constants.ApplicatioName, $"શું તમે \"{record.Year}\"ની માહિતી કાઢવા માંગો છો?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
                {
                    if (manager.Delete(record.Id).ReturnCode == ResponseMessages.SuccessCode)
                    {
                        _selectedId = 0;
                        txtSearchString.Text = string.Empty;
                        FillList();
                    }
                    else
                    {
                        await metroWindow.ShowMessageAsync(AppManager.Constants.ApplicatioName, $".તોએની માહિતી કાઢવામાં સમસ્યા છે.", MessageDialogStyle.Affirmative);
                    }
                }
            }
        }

        private async void BTNSave_Click(object sender, RoutedEventArgs e)
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            if (string.IsNullOrEmpty(TXTYear.Text.Trim()))
            {
                await metroWindow.ShowMessageAsync(AppManager.Constants.ApplicatioName, "વર્ષની માહિતી આપો.", MessageDialogStyle.Affirmative);
                TXTYear.Focus();
                return;
            }
            if (string.IsNullOrEmpty(TXTRate.Text.Trim()))
            {
                await metroWindow.ShowMessageAsync(AppManager.Constants.ApplicatioName, "આકારની માહિતી આપો.", MessageDialogStyle.Affirmative);
                TXTRate.Focus();
                return;
            }
            if (DTPClosingDate.SelectedDate == null)
            {
                await metroWindow.ShowMessageAsync(AppManager.Constants.ApplicatioName, "બંધ તારીખ પસંદ કરો.", MessageDialogStyle.Affirmative);
                DTPClosingDate.TXTDatePicker.Focus();
                return;
            }
            var result = manager.Save(new Year_model
            {
                Id = _selectedId,
                Year = TXTYear.Text.Trim(),
                Rate = TXTRate.Text.Trim(),
                ClosingDate = DTPClosingDate.SelectedDate.Value.ToShortDateString().ToGujarati()
            });
            if (result.ReturnCode == ResponseMessages.SuccessCode)
            {
                txtSearchString.Text = string.Empty;
                _selectedId = Convert.ToInt32(result.ReturnValue);
                DisableEntryControls();
                ClearForm();
                FillList();
            }
            else
            {
                await metroWindow.ShowMessageAsync(AppManager.Constants.ApplicatioName, "વર્ષની માહિતી સંગ્રહ કરવામાં સમસ્યા છે.", MessageDialogStyle.Affirmative);
            }
        }

        private void BTNCancel_Click(object sender, RoutedEventArgs e)
        {
            DisableEntryControls();
            ClearForm();
        }

        private void txtSearchString_TextChanged(object sender, TextChangedEventArgs e)
        {
            LSTRecords.ItemsSource = years.Where(x => x.Year.Contains(txtSearchString.Text) || txtSearchString.Text.Contains(x.Year)).ToList();
            LSTRecords.DisplayMemberPath = "Year";
            LSTRecords.SelectedValuePath = "Id";
            LSTRecords.UpdateLayout();
            if (LSTRecords.Items.Count > 0)
            {
                LSTRecords.SelectedIndex = 0;
            }
        }
    }
}
