using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerTracking.Core.Handlers.Models
{
    public class StatusModel
    {
        public StatusModel(int statusId, string statusName)
        {
            StatusId = statusId;
            StatusName = statusName;
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }
}
