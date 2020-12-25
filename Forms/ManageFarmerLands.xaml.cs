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
using System.Windows.Shapes;

namespace PiyatMandli.Forms
{
    /// <summary>
    /// Interaction logic for ManageFarmerLands.xaml
    /// </summary>
    public partial class ManageFarmerLands : MetroWindow
    {
        FarmerLandManager manager;
        FarmerManager farmerManager;
        WindowManager windowManager;
        int _selectedId;
        List<FarmerLand_model> lands;
        Farmer_model farmer;
        int _selectedFarmerId;
        public ManageFarmerLands(int farmerId)
        {
            InitializeComponent();
            _selectedFarmerId = farmerId;
            manager = new FarmerLandManager();
            farmerManager = new FarmerManager();
            windowManager = new WindowManager();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            var result = farmerManager.GetAll(_selectedFarmerId);
            farmer = result.RecordList[0];
            LBLTItle.Content = farmer.Name + "ની જમીનની યાદી";
            DisableEntryControls();
            var windowResult = windowManager.GetAll();
            if (windowResult.RecordList != null)
            {
                CBWindow.ItemsSource = windowResult.RecordList;
                CBWindow.DisplayMemberPath = "WindowName";
                CBWindow.SelectedValuePath = "Id";
            }
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
            TXTBlockNo.Text = string.Empty;
            TXTSurveyNo.Text = string.Empty;
            TXTLandName.Text = string.Empty;
            TXTLandArea.Text = string.Empty;
            if (CBWindow.SelectedIndex > 0)
            {
                CBWindow.SelectedIndex = 0;
            }
        }
        void FillList()
        {
            var result = manager.GetAll(_selectedFarmerId);
            LSTRecords.ItemsSource = result.RecordList;
            lands = result.RecordList;
            LSTRecords.DisplayMemberPath = "LandName";
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
            var record = (FarmerLand_model)LSTRecords.SelectedItem;
            if (record != null)
            {
                TXTBlockNo.Text = record.BlockNo;
                TXTSurveyNo.Text = record.SurveyNo;
                TXTLandName.Text = record.LandName;
                TXTLandArea.Text = record.LandArea;
                CBWindow.SelectedValue = record.WindowId;
            }
        }

        private void BTNAdd_Click(object sender, RoutedEventArgs e)
        {
            _selectedId = 0;
            EnableEntryControl();
            ClearForm();
            TXTBlockNo.Focus();
        }

        private void BTNEdit_Click(object sender, RoutedEventArgs e)
        {
            var record = (FarmerLand_model)LSTRecords.SelectedItem;
            if (record != null)
            {
                _selectedId = record.Id;
                TXTBlockNo.Text = record.BlockNo;
                TXTSurveyNo.Text = record.SurveyNo;
                TXTLandName.Text = record.LandName;
                TXTLandArea.Text = record.LandArea;
                CBWindow.SelectedValue = record.WindowId;
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
                        await metroWindow.ShowMessageAsync(AppManager.Constants.ApplicatioName, $"જમીનની માહિતી કાઢવામાં સમસ્યા છે.", MessageDialogStyle.Affirmative);
                    }
                }
            }
        }

        private async void BTNSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TXTBlockNo.Text.Trim()))
            {
                await this.ShowMessageAsync(AppManager.Constants.ApplicatioName, "બ્લોક નં. આપો.", MessageDialogStyle.Affirmative);
                TXTBlockNo.Focus();
                return;
            }
            if (string.IsNullOrEmpty(TXTSurveyNo.Text.Trim()))
            {
                await this.ShowMessageAsync(AppManager.Constants.ApplicatioName, "સર્વે નં. આપો.", MessageDialogStyle.Affirmative);
                TXTSurveyNo.Focus();
                return;
            }
            if (string.IsNullOrEmpty(TXTLandName.Text.Trim()))
            {
                await this.ShowMessageAsync(AppManager.Constants.ApplicatioName, "જમીનનું નામ આપો.", MessageDialogStyle.Affirmative);
                TXTSurveyNo.Focus();
                return;
            }
            if (string.IsNullOrEmpty(TXTLandArea.Text.Trim()))
            {
                await this.ShowMessageAsync(AppManager.Constants.ApplicatioName, "જમીનનો વિસ્તાર આપો.", MessageDialogStyle.Affirmative);
                TXTSurveyNo.Focus();
                return;
            }
            if (CBWindow.SelectedValue == null)
            {
                await this.ShowMessageAsync(AppManager.Constants.ApplicatioName, "બારી પસંદ કરો.", MessageDialogStyle.Affirmative);
                TXTSurveyNo.Focus();
                return;
            }
            var result = manager.Save(new FarmerLand_model
            {
                Id = _selectedId,
                FarmerId = farmer.Id,
                BlockNo = TXTBlockNo.Text.Trim(),
                SurveyNo = TXTSurveyNo.Text.Trim(),
                LandName = TXTLandName.Text.Trim(),
                LandArea = TXTLandArea.Text.Trim(),
                WindowId = (int)CBWindow.SelectedValue
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
                await this.ShowMessageAsync(AppManager.Constants.ApplicatioName, "જમીનની માહિતી સંગ્રહ કરવામાં સમસ્યા છે.", MessageDialogStyle.Affirmative);
            }
        }

        private void BTNCancel_Click(object sender, RoutedEventArgs e)
        {
            DisableEntryControls();
            ClearForm();
        }

        private void txtSearchString_TextChanged(object sender, TextChangedEventArgs e)
        {
            LSTRecords.ItemsSource = lands.Where(x => x.LandName.Contains(txtSearchString.Text) || txtSearchString.Text.Contains(x.LandName) || x.BlockNo.Contains(txtSearchString.Text) || x.SurveyNo.Contains(txtSearchString.Text)).ToList();
            LSTRecords.DisplayMemberPath = "LandName";
            LSTRecords.SelectedValuePath = "Id";
            LSTRecords.UpdateLayout();
            if (LSTRecords.Items.Count > 0)
            {
                LSTRecords.SelectedIndex = 0;
            }
        }

        private void BTNClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
