
namespace ProjectManager.Core.Model
{
    public class Role
    {
        public uint Id { get; set; } = 0;

        public string Name { get; set; } = "";
        public bool CanDoTasks { get; set; } = false;
        public bool CanManageProjects { get; set; } = false;
        public bool CanManageUsers { get; set; } = false;
        public bool CanDoReports { get; set; } = false;
        public bool IsValid => Id != 0;
        public Role()
        {
        }

        public Role(uint id, string name, bool canDoTasks, bool canManageProjects, bool canManageUsers, bool canDoReports)
        {
            Id = id;
            Name = name;
            CanDoTasks = canDoTasks;
            CanManageProjects = canManageProjects;
            CanManageUsers = canManageUsers;
            CanDoReports = canDoReports;
        }
    }
}
