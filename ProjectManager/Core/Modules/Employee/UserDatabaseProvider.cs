using System;
using ProjectManager.Core.Modules.Database;
using ProjectManager.Core.Model;
using Npgsql;

namespace ProjectManager.Core.Modules.Employee
{
    public class UserDatabaseProvider
    {
        private DatabaseProvider _dataProvider;
        private static string _selectFullUserInfoTemplate = 
            "select user_id, family, name, last_name, git_username, token, email, r.role_id, rolename, can_do_tasks, can_manage_projects, can_manage_users, can_do_reports from public.user u " +
            "join role r on u.role_id=r.role_id  ";

        private static User parseOneFullUserInfo(NpgsqlDataReader resultReader)
        {
            Role role = new Role(
                    Convert.ToUInt32(resultReader.GetValue(7)),
                    resultReader.GetString(8),
                    resultReader.GetBoolean(9),
                    resultReader.GetBoolean(10),
                    resultReader.GetBoolean(11),
                    resultReader.GetBoolean(12));

            User user = new User(
                Convert.ToUInt32(resultReader.GetValue(0)),
                resultReader.GetString(1),
                resultReader.GetString(2),
                resultReader.GetString(3),
                resultReader.GetString(4),
                resultReader.GetString(5),
                resultReader.GetString(6));

            user.Role = role;
            return user;
        }

        public UserDatabaseProvider(DatabaseProvider dbConnection)
        {
            _dataProvider = dbConnection;
        }

        public User GetUserById(uint Id)
        {
            var connection = _dataProvider.CreateConnection();

            var command = new NpgsqlCommand(
                _selectFullUserInfoTemplate + "where u.user_id=($1)",
                connection
            )
            {
                Parameters ={
                    new NpgsqlParameter() {Value = Id}
                }
            };

            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return parseOneFullUserInfo(reader);
            }
            else
            {
                return null;
            }
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            var connection = _dataProvider.CreateConnection();

            var command = new NpgsqlCommand(
                _selectFullUserInfoTemplate + 
                "where u.email=($1) and u.password=($2)",
                connection
            )
            {
                Parameters ={
                    new NpgsqlParameter() {Value = email},
                    new NpgsqlParameter() {Value = password},
                }
            };

            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return parseOneFullUserInfo(reader);
            }
            else
            {
                return null;
            }
        }

        //public IEnumerable<User> GetAllUsers()
        //{
        //    var connection = _dataProvider.CreateConnection();


        //}

        public bool UpdateUserByID(uint id, User user, string password = "")
        {
            var connection = _dataProvider.CreateConnection();

            var command = new NpgsqlCommand(
                "update public.user " +
                "set family=($1), name=($2), last_name=($3), git_username=($4), token=($5), email=($6), " + (password.Length > 0 ? "password=($9), " : "") + "role_id=($7)" +
                "where user_id=($8);",
                connection
            )
            {
                Parameters ={
                    new NpgsqlParameter() {Value = user.Family},
                    new NpgsqlParameter() {Value = user.Name},
                    new NpgsqlParameter() {Value = user.LastName},
                    new NpgsqlParameter() {Value = user.GitUsername},
                    new NpgsqlParameter() {Value = user.Token},
                    new NpgsqlParameter() {Value = user.Email},
                    new NpgsqlParameter() {Value = (int)user.Role.Id},
                    new NpgsqlParameter() {Value = (int)id},
                    new NpgsqlParameter() {Value = password}
                }
            };

            int result = command.ExecuteNonQuery();
            return result == 1;
        }
    }
}
