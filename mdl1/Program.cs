using System;

namespace mdl1
{
    enum DaysOfWeek
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    class CalendarEvent
    {
        private string eventName;
        public DayOfWeek eventDay;
        public string EventName
        {
            get { return eventName; }
            set { eventName = value; }
        }
        public DayOfWeek EventDay
        {
            get { return eventDay; }
            set { eventDay = value; }
        }

        public CalendarEvent()
        {
            eventName = "";
            eventDay = DayOfWeek.Monday;
        }
        public CalendarEvent(string name, DayOfWeek day)
        {
            eventName = name;
            eventDay = day;
        }

        public void PrintEventDetails()
        {
            Console.WriteLine($"Event {EventName} will take place on {EventDay}");
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            CalendarEvent event1 = new CalendarEvent();
            event1.EventName = "Meeting";
            event1.EventDay = DayOfWeek.Friday;
            event1.PrintEventDetails();

            CalendarEvent event2 = new CalendarEvent("Another meeting", DayOfWeek.Sunday);
            event2.PrintEventDetails();
        }
    }
}
