namespace WorkerTracking.Core.Handlers.Models
{
    public class RoleModel
    {
        public RoleModel(int roleId, int totalWorkers)
        {
            RoleId = roleId;
            TotalWorkers = totalWorkers;
        }

        public RoleModel(int roleId, string roleName, string abbreviation)
        {
            RoleId = roleId;
            RoleName = roleName;
            Abbreviation = abbreviation;
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Abbreviation { get; set; }
        public int TotalWorkers { get; set; }
    }
}
