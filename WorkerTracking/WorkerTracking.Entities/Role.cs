namespace WorkerTracking.Entities
{
    public class Role
    {
        public Role(string name, string abbreviation)
        {
            Name = name;
            Abbreviation = abbreviation;
        }

        public Role(int roleId, string name, string abbreviation)
        {
            RoleId = roleId;
            Name = name;
            Abbreviation = abbreviation;
        }

        public int RoleId { get; private set; }
        public string Name { get; private set; }
        public string Abbreviation { get; private set; }
    }
}