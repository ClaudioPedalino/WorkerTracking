using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkerTracking.Api.Common
{
    public static class Routes
    {
        #region Worker-Controller
        public const string GetAll = "get-all";
        public const string GetById = "get-by-id";
        public const string UpdateStatus = "update-status";
        #endregion

        #region HealthCheck-Controller
        public const string Live = "live";
        public const string Ready = "ready";
        public const string UI = "ui-data";
        #endregion
    }
}
