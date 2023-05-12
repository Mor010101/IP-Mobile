using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCalendar.Core.Models;
using XCalendar.Core.Extensions;
using System.Windows.Input;
using XCalendar.Maui.Views;

namespace Mobile_IP.ViewModel
{
    class CalendarViewModel
    {
        public Calendar<CalendarDay> MyCalendar { get; set; } = new Calendar<CalendarDay>();
        public ICommand NavigateCalendarCommand { get; set; }
        public DaysView myDays { get; set; } = new DaysView();

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
