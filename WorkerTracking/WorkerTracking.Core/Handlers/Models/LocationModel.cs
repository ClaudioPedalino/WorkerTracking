using System;

namespace WorkerTracking.Core.Handlers.Models
{
    public class LocationModel
    {
        public LocationModel(Guid LocationId, string LocationName)
        {
            Locationid = LocationId;
            Locationname = LocationName;
        }

        public Guid Locationid { get; set; }
        public string Locationname { get; set; }
    }
}