using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ProjectManager.Core.Model.Project
{
    public class ProjectLinkedPath : ConfigurationElement, INotifyPropertyChanged
    {
        [ConfigurationProperty("projectId", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string ProjectIdString { 
            get {
                return base["projectId"] as string;    
            }
            private set
            {
                base["projectId"] = value;
            }
        }

        public uint ProjectId
        {
            get { return Convert.ToUInt32(ProjectIdString); }
            set { ProjectIdString = value.ToString(); NotifyPropertyChanged(); }
        }

        [ConfigurationProperty("path", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Path {
            get 
            {
                return base["path"] as string;
            }
            set {
                base["path"] = value;
                NotifyPropertyChanged();
            } 
        }

        public ProjectLinkedPath()
        {

        }

        public ProjectLinkedPath(uint id, string path)
        {
            ProjectId = id;
            ProjectIdString = id.ToString();
            Path = path;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
