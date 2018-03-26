using System;

namespace CTimer
{
    public class STimer
    {
        public DateTime DateTime { get; set; }
        public string Name { get; set; }
        public STimer() { }
        public STimer(string name, DateTime dateTime)
        {
            Name = name;
            DateTime = dateTime;
        }
        public DateTime GetTimeRemaining
        {
            get
            {
                return new DateTime((DateTime - DateTime.Now).Ticks);
            }
        }
    }
}
