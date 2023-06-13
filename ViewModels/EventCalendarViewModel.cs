using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using XCalendar.Core.Collections;
using XCalendar.Core.Enums;
using XCalendar.Core.Extensions;
using XCalendar.Core.Models;
using XCalendar.Core.Interfaces;
using Mobile_IP.Models;

namespace Mobile_IP.ViewModels
{
    public class EventCalendarViewModel : BaseViewModel
    {
        public Calendar<EventDay> EventCalendar { get; set; } = new Calendar<EventDay>()
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

        public static readonly Random Random = new Random();
        public double DaysViewHeightRequest { get; set; } = 440;
        public double DayNamesHeightRequest { get; set; } = 25;
        public double NavigationHeightRequest { get; set; } = 50;
        public double DayHeightRequest { get; set; } = 45;
        public double DayWidthRequest { get; set; } = 45;
        public bool DayAutoSetStyleBasedOnDayState { get; set; } = true;
        public int ForwardsNavigationAmount { get; set; } = 1;
        public int BackwardsNavigationAmount { get; set; } = -1;
        public ObservableRangeCollection<Event> Events { get; } = new ObservableRangeCollection<Event>()
        {
            new Event() { Title = "Consultatie", Description = "Ora 12:00" },
            new Event() { Title = "Consultatie", Description = "Ora 10:00" },
            new Event() { Title = "Consultatie", Description = "Ora 9:00" },
            new Event() { Title = "Consultatie", Description = "Ora 12:00" },
            new Event() { Title = "Consultatie", Description = "Ora 12:00" }
        };
        public ObservableRangeCollection<Event> SelectedEvents { get; } = new ObservableRangeCollection<Event>();

        public ICommand NavigateCalendarCommand { get; set; }
        public ICommand ChangeDateSelectionCommand { get; set; }

        public EventCalendarViewModel()
        {
            NavigateCalendarCommand = new Command<int>(NavigateCalendar);
            ChangeDateSelectionCommand = new Command<DateTime>(ChangeDateSelection);

            foreach (Event @event in Events)
            {
                @event.DateTime = DateTime.Today.AddDays(Random.Next(-20, 21)).AddSeconds(Random.Next(86400));
                @event.Color = Color.FromRgba("#9d0101");
            }

            EventCalendar.SelectedDates.CollectionChanged += SelectedDates_CollectionChanged;
            EventCalendar.DaysUpdated += EventCalendar_DaysUpdated;
            foreach (var day in EventCalendar.Days)
            {
                day.Events.ReplaceRange(Events.Where(x => x.DateTime.Date == day.DateTime.Date));
            }
        }

        public void NavigateCalendar(int amount)
        {
            //Months are variable length, calculate the timespan needed to get to the result.
            DateTime targetDateTime = EventCalendar.NavigatedDate.AddMonths(amount);

            EventCalendar.Navigate(targetDateTime - EventCalendar.NavigatedDate);
        }

        private void EventCalendar_DaysUpdated(object sender, EventArgs e)
        {
            foreach (var day in EventCalendar.Days)
            {
                day.Events.ReplaceRange(Events.Where(x => x.DateTime.Date == day.DateTime.Date));
            }
        }
        private void SelectedDates_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SelectedEvents.ReplaceRange(Events.Where(x => EventCalendar.SelectedDates.Any(y => x.DateTime.Date == y.Date)).OrderByDescending(x => x.DateTime));
        }
        public void ChangeDateSelection(DateTime dateTime)
        {
            EventCalendar?.ChangeDateSelection(dateTime);
        }
    }
 }
