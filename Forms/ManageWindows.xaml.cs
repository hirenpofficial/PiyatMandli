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
    public partial class ManageWindows : Page
    {
        WindowManager manager;
        int _selectedWindow;
        List<Window_model> windows;
        public ManageWindows()
        {
            InitializeComponent();
            manager = new WindowManager();
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
            TXTWindowName.Text = string.Empty;
        }
        void FillList()
        {
            var result = manager.GetAll();
            LSTRecords.ItemsSource = result.RecordList;
            windows = result.RecordList;
            LSTRecords.DisplayMemberPath = "WindowName";
            LSTRecords.SelectedValuePath = "Id";
            LSTRecords.UpdateLayout();
            if (_selectedWindow > 0)
            {
                LSTRecords.SelectedValue = _selectedWindow;
            }
            else if (LSTRecords.Items.Count > 0)
            {
                LSTRecords.SelectedIndex = 0;
            }
        }

        private void LSTRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var record = (Window_model)LSTRecords.SelectedItem;
            if (record != null)
            {
                TXTWindowName.Text = record.WindowName;
            }
        }

        private void BTNAdd_Click(object sender, RoutedEventArgs e)
        {
            _selectedWindow = 0;
            EnableEntryControl();
            ClearForm();
            TXTWindowName.Focus();
        }

        private void BTNEdit_Click(object sender, RoutedEventArgs e)
        {
            var record = (Window_model)LSTRecords.SelectedItem;
            if (record != null)
            {
                _selectedWindow = record.Id;
                TXTWindowName.Text = record.WindowName;
                EnableEntryControl();
            }
        }

        private async void BTNDelete_Click(object sender, RoutedEventArgs e)
        {
            var record = (Window_model)LSTRecords.SelectedItem;
            if (record != null)
            {
                var metroWindow = (Application.Current.MainWindow as MetroWindow);
                if (await metroWindow.ShowMessageAsync(AppManager.Constants.ApplicatioName, $"શું તમે \"{record.WindowName}\"ની માહિતી કાઢવા માંગો છો?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
                {
                    if (manager.Delete(record.Id).ReturnCode == ResponseMessages.SuccessCode)
                    {
                        _selectedWindow = 0;
                        txtSearchString.Text = string.Empty;
                        FillList();
                    }
                    else
                    {
                        await metroWindow.ShowMessageAsync(AppManager.Constants.ApplicatioName, $"બારીની માહિતી કાઢવામાં સમસ્યા છે.", MessageDialogStyle.Affirmative);
                    }
                }
            }
        }

        private async void BTNSave_Click(object sender, RoutedEventArgs e)
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            if (string.IsNullOrEmpty(TXTWindowName.Text.Trim()))
            {
                await metroWindow.ShowMessageAsync(AppManager.Constants.ApplicatioName, "બારીનું નામ આપો.", MessageDialogStyle.Affirmative);
                TXTWindowName.Focus();
                return;
            }
            var result = manager.Save(new Window_model
            {
                Id = _selectedWindow,
                WindowName = TXTWindowName.Text.Trim()
            });
            if (result.ReturnCode == ResponseMessages.SuccessCode)
            {
                txtSearchString.Text = string.Empty;
                _selectedWindow = Convert.ToInt32(result.ReturnValue);
                DisableEntryControls();
                ClearForm();
                FillList();
            }
            else
            {
                await metroWindow.ShowMessageAsync(AppManager.Constants.ApplicatioName, "બારીની માહિતી સંગ્રહ કરવામાં સમસ્યા છે.", MessageDialogStyle.Affirmative);
            }
        }

        private void BTNCancel_Click(object sender, RoutedEventArgs e)
        {
            DisableEntryControls();
            ClearForm();
        }

        private void txtSearchString_TextChanged(object sender, TextChangedEventArgs e)
        {
            LSTRecords.ItemsSource = windows.Where(x => x.WindowName.Contains(txtSearchString.Text) || txtSearchString.Text.Contains(x.WindowName)).ToList();
            LSTRecords.DisplayMemberPath = "WindowName";
            LSTRecords.SelectedValuePath = "Id";
            LSTRecords.UpdateLayout();
            if (LSTRecords.Items.Count > 0)
            {
                LSTRecords.SelectedIndex = 0;
            }
        }
    }
}
