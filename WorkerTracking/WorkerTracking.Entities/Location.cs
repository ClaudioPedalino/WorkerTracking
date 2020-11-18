using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerTracking.Entities
{
    public class Location
    {
        public Location(string name)
        {
            LocationName = name;
        }
        public Guid LocationId { get; private set; }
        public string LocationName { get; private set; }

        
    }
}
