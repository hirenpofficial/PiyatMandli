using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PiyatMandli
{
    /// <summary>
    /// Interaction logic for Master.xaml
    /// </summary>[DllImport("user32.dll")] 
   
    public partial class Master : MetroWindow
    {
        private Window[] childWindows;
        public Master()
        {
            var configs = new Entities().GetAll_Configs().ToList();
            AppManager.Configs = new Dictionary<string, string>();
            foreach (var key in configs)
            {
                AppManager.Configs.Add(key.FlagName, key.FlagValue);
            }
            AppManager.Constants.ApplicatioName = AppManager.GetFlag(AppManager.ApplicationFlag.ApplicationName);

            var langs = InputLanguageManager.Current.AvailableInputLanguages;
            foreach (System.Globalization.CultureInfo lang in langs)
            {
                if (lang.Name.ToLower().StartsWith("gu-"))
                {
                    InputLanguageManager.SetInputLanguage(this, lang);
                    break;
                }
            }

            InitializeComponent();
        }
        #region Windows Method
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = AppManager.GetFlag(AppManager.ApplicationFlag.ApplicationName);
            this.MinWidth = System.Windows.SystemParameters.WorkArea.Width;
            this.MinHeight = System.Windows.SystemParameters.WorkArea.Height;
            this.MaxWidth = System.Windows.SystemParameters.WorkArea.Width;
            this.MaxHeight = System.Windows.SystemParameters.WorkArea.Height;
            _mainFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
        }
        private void MetroWindow_StateChanged(object sender, EventArgs e)
        {
            try
            {
                if (WindowState.Minimized == this.WindowState)
                {
                    int numberOfChildWindow = this.OwnedWindows.Count;
                    childWindows = new Window[numberOfChildWindow];
                    for (int count = 0; count < this.OwnedWindows.Count; count++)
                    {
                        childWindows[count] = this.OwnedWindows[count];
                    }
                }

                else if ((WindowState.Maximized == WindowState) || (System.Windows.WindowState.Normal == WindowState))
                {
                    if (childWindows != null)
                    {
                        foreach (Window aChildWindow in childWindows)
                        {
                            aChildWindow.WindowState = WindowState.Normal;
                            aChildWindow.Show();
                        }
                    }
                }
            }
            catch
            {
                
            }
        }
        #endregion Windows Method

        #region Control Evnts
        private void _mainFrame_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (((Button)e.OriginalSource).Name.ToLower().Equals("btnclose"))
                {
                    _mainFrame.Content = null;
                    _mainFrame.NavigationService.RemoveBackEntry();
                }
            }
            catch (Exception ex)
            {
            }
        }

       

        private void BTNToggleLeftPanel_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            if (STPLeftPanel.Width == BTNToggleLeftPanel.Width + 20)
            {
                da.From = BTNToggleLeftPanel.Width;
                da.To = 230;
                BTNToggleLeftPanel.Content = "3";
            }
            else
            {
                da.From = 230;
                da.To = BTNToggleLeftPanel.Width + 20;
                BTNToggleLeftPanel.Content = "8";
            }

            da.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            STPLeftPanel.BeginAnimation(DockPanel.WidthProperty, da);
        }

        #endregion Control Events

        private void BTNManageFarmers_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("/Forms/ManageFarmers.xaml", UriKind.Relative);
            _mainFrame.Navigate(uri);
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var langs = InputLanguageManager.Current.AvailableInputLanguages;
            foreach (System.Globalization.CultureInfo lang in langs)
            {
                if (lang.Name.ToLower().StartsWith("en-"))
                {
                    InputLanguageManager.SetInputLanguage(this, lang);
                    break;
                }
            }
        }

        private void BTNManageWindows_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("/Forms/ManageWindows.xaml", UriKind.Relative);
            _mainFrame.Navigate(uri);
        }
    }
}
