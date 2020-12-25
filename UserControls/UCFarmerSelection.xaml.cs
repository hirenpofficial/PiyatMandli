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

namespace PiyatMandli
{
    /// <summary>
    /// Interaction logic for UCFarmerSelection.xaml
    /// </summary>
    public partial class UCFarmerSelection : UserControl
    {
        FarmerManager manager;
        List<Farmer_model> farmers;
        Farmer_model _selectedFarmer;
        bool selected;
        public Farmer_model SelectedFarmer
        {
            get
            {
                return _selectedFarmer;
            }
            set
            {
                _selectedFarmer = value;
                if (_selectedFarmer != null)
                {
                    TXTFarmer.Text = _selectedFarmer.FarmerCode + " - " + _selectedFarmer.Name;
                }
            }
        }
        public UCFarmerSelection()
        {
            manager = new FarmerManager();
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            farmers = manager.GetAll(isDeleted: false).RecordList;
            ListCollectionView collection = new ListCollectionView(farmers);
            collection.GroupDescriptions.Add(new PropertyGroupDescription("Village"));
            LSTRecords.ItemsSource = collection;
            LSTRecords.SelectedItem = null;
            
        }
        private void TXTFarmer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //PPFarmer.IsOpen = !PPFarmer.IsOpen;
        }

        private void TXTFarmer_LostFocus(object sender, RoutedEventArgs e)
        {
            PPFarmer.IsOpen = false;
        }

        private void TXTFarmer_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void LSTRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LSTRecords.SelectedItem != null)
            {
                SelectedFarmer = (Farmer_model)LSTRecords.SelectedItem;
                PPFarmer.IsOpen = false;
            }
            selected = true;
            TXTFarmer.Focus();
        }

        private void TXTFarmer_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.F2)
            {
                PPFarmer.IsOpen = !PPFarmer.IsOpen;
            }
        }

        private void TXTFarmer_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                selected = false;
                if (SelectedFarmer != null)
                {
                    selected = TXTFarmer.Name == SelectedFarmer.FarmerCode + " - " + SelectedFarmer.Name;
                }
                if (!selected)
                {
                    var data = farmers.Where(x => x.Name.Contains(TXTFarmer.Text.Trim()) || x.FarmerCode.Contains(TXTFarmer.Text.Trim()) || x.ShareNo.Contains(TXTFarmer.Text.Trim()) || x.Village.Contains(TXTFarmer.Text.Trim())).ToList();
                    ListCollectionView collection = new ListCollectionView(data);
                    collection.GroupDescriptions.Add(new PropertyGroupDescription("Village"));
                    LSTRecords.ItemsSource = collection;
                }
                else
                {
                    ListCollectionView collection = new ListCollectionView(farmers);
                    collection.GroupDescriptions.Add(new PropertyGroupDescription("Village"));
                    LSTRecords.ItemsSource = collection;
                    selected = false;
                }
            }
            catch(Exception ex) 
            { 
            }
        }
    }
}
