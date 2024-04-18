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
            set
            {
                base["ProjectLinkedPaths"] = value;
            }
        }
    }

    [ConfigurationCollection(typeof(ProjectLinkedPath))]
    public class ProjectLinkedPathCollection : ConfigurationElementCollection
    {
        public ProjectLinkedPath this[int idx]
        {
            get { return (ProjectLinkedPath)BaseGet(idx); }
            set
            {
                BaseAdd(idx, value);
            }
        }

        public void Add(ProjectLinkedPath path)
        {
            BaseAdd(path, false);
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
