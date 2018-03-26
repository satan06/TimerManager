using System;
using System.Collections.Generic;

namespace CTimer
{
    [Serializable]
    public class STimeManager
    {
        public int CurrentTimer { get; set; }
        public List<STimer> Timers = new List<STimer>(); 
        public STimeManager() { }
        public void AddTimer(string name, DateTime dateTime)
        {
            Timers.Add(new STimer(name, dateTime));
        }
        public void ModifyTimer(string name)
        {
            Timers[CurrentTimer].Name = name;
        }
        public STimer GetTimer
        {
            get
            {
                return Timers[CurrentTimer];
            }
        }
        public void ModifyTimer(DateTime dateTime)
        {
            Timers[CurrentTimer].DateTime = dateTime;
        }
        public void DeleteCurrentTimer()
        {
            Timers.RemoveAt(CurrentTimer);
        }
    }
}
