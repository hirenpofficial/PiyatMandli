using Newtonsoft.Json;
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
    /// Interaction logic for UCDatePicker.xaml
    /// </summary>
    public partial class UCDatePicker : UserControl
    {
        int currentMonth;
        int currentYear;
        int totalDays;
        DayOfWeek dayOfWeek;
        List<string> days;
        DateTime? _selectedDate;
        public DateTime? SelectedDate
        {
            get
            {
                return _selectedDate;
            }
            set
            {
                _selectedDate = value;
                if (value != null)
                {
                    TXTDatePicker.Text = _selectedDate.Value.ToShortDateString().ToGujarati();
                }
            }
        }
        public UCDatePicker()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (SelectedDate.ToString().Contains("0001"))
            {
                SelectedDate = DateTime.Now;
            }
            else
            {
                SelectedDate = SelectedDate;
            }
            days = new List<string>();
            days.Add("Monday");
            days.Add("Tuesday");
            days.Add("Wednesday");
            days.Add("Thursday");
            days.Add("Friday");
            days.Add("Saturday");
            days.Add("Sunday");

            if (SelectedDate.HasValue)
            {
                currentMonth = SelectedDate.Value.Month;
                currentYear = SelectedDate.Value.Year;
            }
            else
            {
                currentMonth = DateTime.Now.Month;
                currentYear = DateTime.Now.Year;
            }
            totalDays = CalculateTotalDays(currentMonth, currentYear);
            TBTodaysDate.Text = "આજની તારીખ : " + DateTime.Now.ToString("dd-MM-yyyy").ToGujarati();
            Generate();
        }

        void Generate()
        {

            ClearControls();
            //currentYear = SelectedDate.Year;
            //currentMonth = SelectedDate.Month;
            dayOfWeek = new DateTime(currentYear, currentMonth, 1).DayOfWeek;
            LBLMonth.Text = AppManager.GetMonthInGujarati(currentMonth) + " " + currentYear.ToString().ToGujarati();
            var totalDaysInCurrentMonth = CalculateTotalDays(currentMonth, currentYear);
            var previousMonth = currentMonth - 1;
            var previousYear = currentYear;
            var nextMonth = currentMonth + 1;
            var nextYear = currentYear;
            if (previousMonth < 1)
            {
                previousMonth = 12;
                previousYear = previousYear - 1;
            }
            if (nextMonth > 12)
            {
                nextMonth = 1;
                nextYear = nextYear + 1;
            }
            var previousMonthDays = CalculateTotalDays(previousMonth, previousYear);
            var nextMonthDays = CalculateTotalDays(nextMonth, nextYear);
            var daysIndex = days.IndexOf(dayOfWeek.ToString());
            List<DateModel> dates = new List<DateModel>();
            previousMonthDays = previousMonthDays - daysIndex;
            for (var i = 0; i < daysIndex; i++)
            {
                previousMonthDays++;
                dates.Add(new DateModel
                {
                    Date = previousMonthDays.ToString(),
                    OriginalDate = new DateTime(previousYear, previousMonth, previousMonthDays),
                    IsPrevious = true
                });
            }
            for (var i = 1; i <= totalDaysInCurrentMonth; i++)
            {
                dates.Add(new DateModel
                {
                    Date = i.ToString(),
                    OriginalDate = new DateTime(currentYear, currentMonth, i)
                });
            }

            dayOfWeek = new DateTime(currentYear, currentMonth, totalDaysInCurrentMonth).DayOfWeek;
            daysIndex = days.IndexOf(dayOfWeek.ToString());
            daysIndex = 6 - daysIndex;
            if (daysIndex > 0)
            {
                for (var i = 1; i <= daysIndex; i++)
                {
                    dates.Add(new DateModel
                    {
                        Date = i.ToString(),
                        OriginalDate = new DateTime(nextYear, nextMonth, i),
                        IsNext = true
                    });
                }

            }
            for (var cnt = 0; cnt < dates.Count; cnt++)
            {
                var date = dates[cnt];
                var btn = (TextBlock)GBDates.FindName("TB_" + (cnt + 1));
                btn.Text = date.OriginalDate.ToString("dd").ToGujarati();
                btn.Tag = JsonConvert.SerializeObject(date);
                btn.HorizontalAlignment = HorizontalAlignment.Center;
                btn.VerticalAlignment = VerticalAlignment.Center;

                if (date.IsPrevious || date.IsNext)
                {
                    btn.Foreground = new SolidColorBrush(Colors.Gray);
                }
                else
                {
                    btn.Foreground = new SolidColorBrush(Colors.Black);
                    btn.FontWeight = FontWeights.Normal;
                }
                if (SelectedDate.HasValue)
                {
                    if (date.OriginalDate.Date == SelectedDate.Value.Date)
                    {
                        btn.Foreground = new SolidColorBrush(Colors.Red);
                        btn.FontWeight = FontWeights.UltraBold;
                    }
                }
            }
            GBDates.UpdateLayout();
        }

        int CalculateTotalDays(int month, int year)
        {
            int days;
            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                days = 31;
            }
            else if (month == 2)
            {
                if (DateTime.IsLeapYear(year))
                {
                    days = 29;
                }
                else
                {
                    days = 28;
                }
            }
            else
            {
                days = 30;
            }
            return days;
        }

        void ClearControls()
        {
            foreach (TextBlock tb in AppManager.FindVisualChildren<TextBlock>(GBDates))
            {
                if (tb.Name.StartsWith("TB_"))
                {
                    tb.Text = "";
                    tb.Tag = "";
                    tb.Foreground = new SolidColorBrush(Colors.Black);
                    tb.FontStyle = FontStyles.Normal;
                }
            }
            GBDates.UpdateLayout();
        }

        private void BTNPreviousMonth_Click(object sender, RoutedEventArgs e)
        {
            currentMonth = currentMonth - 1;
            if (currentMonth < 1)
            {
                currentMonth = 12;
                currentYear = currentYear - 1;
            }
            Generate();
        }

        private void TBTodaysDate_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectedDate = DateTime.Now;
            currentMonth = SelectedDate.Value.Month;
            currentYear = SelectedDate.Value.Year;
            totalDays = CalculateTotalDays(currentMonth, currentYear);
            TXTDatePicker.Text = SelectedDate.Value.ToShortDateString().ToGujarati();
            Generate();
        }

        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var lbl = sender as TextBlock;
            var dateModel = JsonConvert.DeserializeObject<DateModel>(lbl.Tag.ToString());
            this.SelectedDate = dateModel.OriginalDate;
            currentMonth = SelectedDate.Value.Month;
            currentYear = SelectedDate.Value.Year;
            TXTDatePicker.Text = SelectedDate.Value.ToShortDateString().ToGujarati();
            Generate();
        }

        private void TBPrevious_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            currentMonth = currentMonth - 1;
            if (currentMonth < 1)
            {
                currentMonth = 12;
                currentYear = currentYear - 1;
            }
            Generate();
        }

        private void TBNext_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            currentMonth = currentMonth + 1;
            if (currentMonth > 12)
            {
                currentMonth = 1;
                currentYear = currentYear + 1;
            }
            Generate();
        }
    }

    class DateModel
    {
        public string Date { get; set; }
        public DateTime OriginalDate { get; set; }
        public bool IsPrevious { get; set; }
        public bool IsNext { get; set; }
        public bool IsSelected { get; set; }
    }
}
