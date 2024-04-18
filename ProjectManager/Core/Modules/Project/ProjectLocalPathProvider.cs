﻿using ProjectManager.Core.Model.Project;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Core.Modules.Project
{
    public class ProjectLocalPathProvider
    {
        private Configuration _configFile;
        private ProjectLinkedPathConfigSection _configSection;

        public ProjectLinkedPath GetProjectPathById(int id)
        {
            return new ProjectLinkedPath(_configSection.LinkedPaths[id].ProjectId, _configSection.LinkedPaths[id].Path);
        }

        public void AddOrUpdateProjectPathById(ProjectLinkedPath path)
        {
            _configSection.LinkedPaths.Add(path);
            _configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(_configSection.SectionInformation.Name);
        }

        public ProjectLocalPathProvider()
        {
            _configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            _configSection = _configFile.GetSection("ProjectLinkedPathSection") as ProjectLinkedPathConfigSection;
        }


    }
}
