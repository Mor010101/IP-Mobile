using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCalendar.Core.Models;
using XCalendar.Core.Extensions;
using System.Windows.Input;
using XCalendar.Maui.Views;
using Mobile_IP.ViewModels;
using XCalendar.Core.Enums;

namespace Mobile_IP.ViewModel
{
    public class CalendarViewModel : BaseViewModel
    {
        public Calendar<CalendarDay> MyCalendar { get; set; } = new Calendar<CalendarDay>()
        {
            NavigatedDate = DateTime.Today,
            NavigationLowerBound = DateTime.Today.AddYears(-2),
            NavigationUpperBound = DateTime.Today.AddYears(2),
            StartOfWeek = DayOfWeek.Monday,
            SelectionAction = SelectionAction.Modify,
            NavigationLoopMode = NavigationLoopMode.LoopMinimumAndMaximum,
            SelectionType = SelectionType.Single,
            PageStartMode = PageStartMode.FirstDayOfMonth,
            Rows = 2,
            AutoRows = true,
            AutoRowsIsConsistent = true,
            TodayDate = DateTime.Today
        };
        public double DaysViewHeightRequest { get; set; } = 440;
        public double DayNamesHeightRequest { get; set; } = 25;
        public double NavigationHeightRequest { get; set; } = 50;
        public double DayHeightRequest { get; set; } = 45;
        public double DayWidthRequest { get; set; } = 45;
        public bool DayAutoSetStyleBasedOnDayState { get; set; } = true;
        public int ForwardsNavigationAmount { get; set; } = 1;
        public int BackwardsNavigationAmount { get; set; } = -1;

        public ICommand NavigateCalendarCommand { get; set; }

        public CalendarViewModel()
        {
            NavigateCalendarCommand = new Command<int>(NavigateCalendar);
        }

        public void NavigateCalendar(int amount)
        {
            //Months are variable length, calculate the timespan needed to get to the result.
            DateTime targetDateTime = MyCalendar.NavigatedDate.AddMonths(amount);

            MyCalendar.Navigate(targetDateTime - MyCalendar.NavigatedDate);
        }
    }
}
