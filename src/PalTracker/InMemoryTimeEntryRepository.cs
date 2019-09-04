using System.Collections.Generic;

namespace PalTracker
{
    public class InMemoryTimeEntryRepository : ITimeEntryRepository
    {
        List<TimeEntry> timeEntrys=new List<TimeEntry>();
        
        public bool Contains(long id)
        {
            TimeEntry timeEnt=Find(id);
            if(timeEnt.Id!=null)
               return true;
            return false;
        }

        public TimeEntry Create(TimeEntry timeEntry)
        {
            timeEntry.Id=timeEntrys.Count+1;
            timeEntrys.Add(timeEntry);
            return timeEntrys[timeEntrys.Count-1];
        }

        public void Delete(long id)
        {
             timeEntrys.RemoveAt((int)id-1);
        }

        public TimeEntry Find(long id)
        {
            if(timeEntrys.Count>0&&id
                <= timeEntrys.Count)
               return timeEntrys[(int)id-1];
            return new TimeEntry();
        }

        public IList<TimeEntry> List()
        {
           return timeEntrys;
        }

        public TimeEntry Update(long id, TimeEntry timeEntry)
        {
            TimeEntry timeEnt=Find(id);
            timeEntry.Id=timeEnt.Id;
            timeEntrys.RemoveAt((int)id-1);
            timeEntrys.Insert((int)id-1,timeEntry);
            return timeEntry;
        }
    }
}