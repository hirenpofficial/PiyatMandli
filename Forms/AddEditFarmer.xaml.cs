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
    /// Interaction logic for AddEditFarmer.xaml
    /// </summary>
    public partial class AddEditFarmer : MetroWindow
    {
        int _selectedFarmerId;
        FarmerManager manager;
        public AddEditFarmer()
        {
            InitializeComponent();
            _selectedFarmerId = 0;
            manager = new FarmerManager();
        }
        public AddEditFarmer(int farmerId)
        {
            InitializeComponent();
            _selectedFarmerId = farmerId;
            manager = new FarmerManager();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (_selectedFarmerId > 0)
            {
                LBLTitle.Content = "ખેડૂતની માહિતી સુધારો";
                var result = manager.GetAll(_selectedFarmerId);
                if(result.ReturnCode == ResponseMessages.SuccessCode)
                {
                    var farmer = result.RecordList[0];
                    TXTSabhasadCode.Text = farmer.FarmerCode;
                    TXTShareNo.Text = farmer.ShareNo;
                    TXTName.Text = farmer.Name;
                    TXTAddress.Text = farmer.AddressLine1;
                    TXTVillage.Text = farmer.Village;
                    TXTCity.Text = farmer.City;
                    TXTZip.Text = farmer.Pincode;
                    TXTPhone1.Text = farmer.PhoneNo1;
                    TXTPhone2.Text = farmer.PhoneNo2;
                    TXTMobile1.Text = farmer.MobileNo1;
                    TXTMobile2.Text = farmer.MobileNo2;
                    if (!string.IsNullOrEmpty(farmer.DateOfBirth))
                    {
                        DTPDateOfBirth.SelectedDate = Convert.ToDateTime(farmer.DateOfBirth.ToEnglish());
                    }
                    if (!string.IsNullOrEmpty(farmer.DateOfRegistration))
                    {
                        DTPDateOfRegistration.SelectedDate = Convert.ToDateTime(farmer.DateOfRegistration.ToEnglish());
                    }
                }
            }
            else
            {
                LBLTitle.Content = "નવો ખેડૂત ઉમેરો";
            }
            TXTSabhasadCode.Focus();
        }

        private void BTNClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private async void BTNSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TXTSabhasadCode.Text.Trim()))
            {
                await this.ShowMessageAsync(AppManager.Constants.ApplicatioName, "સભાસદ કોડ આપો.", MessageDialogStyle.Affirmative);
                TXTSabhasadCode.Focus();
                return;
            }
            if (string.IsNullOrEmpty(TXTShareNo.Text.Trim()))
            {
                await this.ShowMessageAsync(AppManager.Constants.ApplicatioName, "શેર નં. આપો.", MessageDialogStyle.Affirmative);
                TXTShareNo.Focus();
                return;
            }
            if (string.IsNullOrEmpty(TXTName.Text.Trim()))
            {
                await this.ShowMessageAsync(AppManager.Constants.ApplicatioName, "ખેડૂતનું નામ આપો.", MessageDialogStyle.Affirmative);
                TXTName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(TXTVillage.Text.Trim()))
            {
                await this.ShowMessageAsync(AppManager.Constants.ApplicatioName, "ગામનું નામ આપો.", MessageDialogStyle.Affirmative);
                TXTVillage.Focus();
                return;
            }
            if (string.IsNullOrEmpty(TXTMobile1.Text.Trim()))
            {
                await this.ShowMessageAsync(AppManager.Constants.ApplicatioName, "મોબાઇલ નં. આપો.", MessageDialogStyle.Affirmative);
                TXTMobile1.Focus();
                return;
            }

            var result = manager.Save(new Farmer_model
            {
                Id = _selectedFarmerId,
                FarmerCode = TXTSabhasadCode.Text.Trim(),
                ShareNo = TXTShareNo.Text.Trim(),
                Name = TXTName.Text.Trim(),
                AddressLine1 = TXTAddress.Text.Trim(),
                City = TXTCity.Text.Trim(),
                Village = TXTVillage.Text.Trim(),
                Pincode = TXTZip.Text.Trim(),
                PhoneNo1 = TXTPhone1.Text.Trim(),
                PhoneNo2 = TXTPhone2.Text.Trim(),
                MobileNo1 = TXTMobile1.Text.Trim(),
                MobileNo2 = TXTMobile2.Text.Trim(),
                DateOfBirth = DTPDateOfBirth.TXTDatePicker.Text,
                DateOfRegistration = DTPDateOfRegistration.TXTDatePicker.Text
            });
            if(result.ResponseCode == ResponseCode.Success)
            {
                DialogResult = true;
                this.Close();
            }
            else
            {
                await this.ShowMessageAsync(AppManager.Constants.ApplicatioName, "ખેડૂતની માહિતી સંગ્રહ કરવામાં સમસ્યા છે.", MessageDialogStyle.Affirmative);
            }
        }

        private void MetroWindow_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(this);
            if (!(focusedControl is Button))
            {
                if (e.Key == Key.Enter)
                {
                    // Creating a FocusNavigationDirection object and setting it to a
                    // local field that contains the direction selected.
                    FocusNavigationDirection focusDirection = FocusNavigationDirection.Next;

                    // MoveFocus takes a TraveralReqest as its argument.
                    TraversalRequest request = new TraversalRequest(focusDirection);

                    // Gets the element with keyboard focus.
                    UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;

                    // Change keyboard focus.
                    if (elementWithFocus != null)
                    {
                        if (elementWithFocus.MoveFocus(request)) e.Handled = true;
                    }
                }
            }
        }

        private void MetroWindow_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            //IInputElement focusedControl = FocusManager.GetFocusedElement(this);
            //if(focusedControl is TextBox)
            //{
            //    TextBox control = (TextBox)focusedControl;
            //    if (string.IsNullOrEmpty(control.Text))
            //    {
            //        var bc = new BrushConverter();
            //        control.Background = (Brush)bc.ConvertFrom("#FF300000");
            //        control.Opacity = 0.1;
            //    }
            //}
        }

        private void MetroWindow_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            //IInputElement focusedControl = FocusManager.GetFocusedElement(this);
            //if (focusedControl is TextBox)
            //{
            //    TextBox control = (TextBox)focusedControl;
            //    if (string.IsNullOrEmpty(control.Text))
            //    {
            //        control.Background = Brushes.White;
            //    }
            //}
        }
    }
}
