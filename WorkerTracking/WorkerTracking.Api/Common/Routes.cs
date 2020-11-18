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
        public const string Create_Worker = "create-worker";
        public const string Update_Worker_Status = "update-status";
        public const string Delete_Worker = "delete-worker";
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
        public const string Get_Team_By_Id = "get-by-id";
        public const string Create_Team = "create-team";
        public const string Update_Team = "update-team";
        public const string Delete_Team = "delete-team";
        #endregion

        #region Location-Controller
        public const string Get_All_Location = "get-all";
        public const string Create_Location = "create-location";

        #endregion



        #region HealthCheck-Controller
        public const string Live = "live";
        public const string Ready = "ready";
        public const string UI = "ui-data";
        #endregion
    }
}
