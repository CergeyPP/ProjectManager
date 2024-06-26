﻿using ProjectManager.Core.Modules.Database;
using ProjectManager.Core.Model.Project;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Npgsql;

namespace ProjectManager.Core.Modules
{
    public class ProjectDatabaseProvider
    {
        private DatabaseProvider _dbProvider;

        private const string selectTemplate = "select project_id, project_name, description, link from project ";

        public IEnumerable<ProjectModel> GetAllProjects()
        {
            var connection = _dbProvider.CreateConnection();

            var command = new NpgsqlCommand(
                selectTemplate, connection);

            var reader = command.ExecuteReader();

            while (reader.Read()) 
            {
                yield return parseOneFullProjectInfo(reader);
            }
        }

        // returns id
        public int RegisterNewProject(ProjectModel project)
        {
            var connection = _dbProvider.CreateConnection();

            var command = new NpgsqlCommand(
                "insert into project(project_name, description, link) values(($1), ($2), ($3)) returning project_id",
                connection)
            {
                Parameters = {
                    new NpgsqlParameter() { Value = project.Name },
                    new NpgsqlParameter() { Value = project.Description },
                    new NpgsqlParameter() { Value = project.GitLink.Length > 0 ? project.GitLink : null }
                }
            };

            var result = command.ExecuteScalar();

            return (int)result;
        }

        public ProjectDatabaseProvider(DatabaseProvider databaseProvider)
        {
            _dbProvider = databaseProvider;
        }

        private ProjectModel parseOneFullProjectInfo(NpgsqlDataReader reader)
        {
            ProjectModel project = new ProjectModel();
            project.Id = Convert.ToUInt32(reader.GetValue(0));
            project.Name = reader.GetValue(1).ToString();
            project.Description = reader.GetValue(2).ToString();
            project.GitLink = reader.GetValue(3).ToString();
            if (project.GitLink == "") project.GitLink = null;

            return project;
        }
    }
}
