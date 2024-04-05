using System.Configuration;
using ProjectManager.Core.Model.Project;

namespace ProjectManager.Core.Modules
{
    public class ProjectLinkedPathConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("ProjectLinkedPaths")]
        public ProjectLinkedPathCollection LinkedPaths
        {
            get
            {
                return base["ProjectLinkedPaths"] as ProjectLinkedPathCollection;
            }
        }
    }

    [ConfigurationCollection(typeof(ProjectLinkedPath))]
    public class ProjectLinkedPathCollection : ConfigurationElementCollection
    {
        public ProjectLinkedPath this[int idx]
        {
            get { return (ProjectLinkedPath)BaseGet(idx); }
        }

        public ProjectLinkedPathCollection()
        {

        }
        protected override ConfigurationElement CreateNewElement()
        {
            return new ProjectLinkedPath();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ProjectLinkedPath)(element)).ProjectIdString;
        }
    }
}
