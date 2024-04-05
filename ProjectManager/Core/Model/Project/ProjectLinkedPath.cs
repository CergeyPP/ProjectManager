using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace ProjectManager.Core.Model.Project
{
    public class ProjectLinkedPath : ConfigurationElement
    {
        [ConfigurationProperty("projectId", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string ProjectIdString { 
            get {
                return base["projectId"] as string;    
            }
            set
            {
                base["projectId"] = value;
            }
        }

        public uint ProjectId
        {
            get { return Convert.ToUInt32(ProjectIdString); }
            set { ProjectIdString = value.ToString(); }
        }

        [ConfigurationProperty("path", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Path {
            get 
            {
                return base["path"] as string;
            }
            set {
                base["path"] = value;
            } 
        }

        public ProjectLinkedPath()
        {

        }

        public ProjectLinkedPath(uint id, string path)
        {
            ProjectIdString = id.ToString();
            Path = path;
        }
    }
}
