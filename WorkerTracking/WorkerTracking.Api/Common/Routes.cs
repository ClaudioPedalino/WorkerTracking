using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkerTracking.Api.Common
{
    public static class Routes
    {
        #region Worker-Controller
        public const string Get_All_Workers = "get-all";
        public const string Get_Worker_By_Id = "get-by-id";
        public const string Update_Worker_Status = "update-status";
        #endregion

        #region Status-Controller
        public const string Get_All_Status = "get-all";
        public const string Get_Status_By_Id = "get-by-id";
        public const string Create_Status = "create-status";
        public const string Update_Status = "update-status";
        public const string Delete_Status = "delete-status";
        #endregion

        #region Role-Controller
        public const string Get_All_Roles = "get-all";
        public const string Get_Role_By_Id = "get-by-id";
        public const string Create_Role = "create-role";
        public const string Update_Role = "update-role";
        public const string Delete_Role = "delete-role";
        #endregion

        #region Team-Controller
        public const string Get_All_Teams = "get-all";
        public const string Delete_Team = "delete-role";
        #endregion

        #region HealthCheck-Controller
        public const string Live = "live";
        public const string Ready = "ready";
        public const string UI = "ui-data";
        #endregion
    }
}
