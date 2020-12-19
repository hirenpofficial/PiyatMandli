using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Controls
{
    /// <summary>
    /// Interaction logic for CustomDataPager.xaml
    /// </summary>
    public partial class CustomDataPager : UserControl
    {
        private Int32 CPage;
        public Int32 CurrentPage
        {
            get
            {
                return CPage;
            }
            set
            {
                CPage = value;
            }
        }
        public CustomDataPager()
        {
            InitializeComponent();
        }

        public void ShowPager(Int32 TotalCount, Int32 CurrentIndex, Int32 FetchRecords)
        {
            Int32 EndRecord = (CurrentIndex * FetchRecords);
            if (TotalCount == 0)
            {
                TXTCountMessage.Text = "No data found.";
                return;
            }
            else if (EndRecord > TotalCount)
            {
                EndRecord = TotalCount;
            }
            if (CurrentIndex == 1)
            {
                TXTCountMessage.Text = "Showing " + CurrentIndex.ToString() + " to " + EndRecord.ToString() + " out of " + TotalCount.ToString() + " Records.";
            }
            else
            {
                TXTCountMessage.Text = "Showing " + (CurrentIndex * FetchRecords + 1 - FetchRecords).ToString() + " to " + EndRecord.ToString() + "  out of " + TotalCount.ToString() + " Records.";
            }
            DPPageButtons.Children.Clear();
            TotalCount = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(TotalCount) / Convert.ToDecimal(FetchRecords)));
            for (Int32 i = 0; i < TotalCount; i++)
            {
                Button B = new Button();
                B.Content = (i + 1).ToString();
                B.Name = "BTN_" + (i + 1).ToString();
                B.Width = 25;
                B.Height = 25;
                B.Margin = new Thickness(0, 5, 5, 5);
                B.Click += Paging_Click;
                DPPageButtons.Children.Add(B);
            }
        }

        void Paging_Click(object sender, RoutedEventArgs e)
        {
            CPage = Convert.ToInt32(((Button)e.Source).Content);
        }
    }
}
