using Dapper;
using DapperLib.Model;
using DapperLib.Model.Data;
using System.Data;

namespace DapperLib.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly DapperDbContext context;

        public UserRepo(DapperDbContext context)
        {
            this.context = context;
        }

        public async Task<string> Create(User user)
        {
            string response = string.Empty;
            string query = "INSERT INTO Users (Username, Password, Email, Role) VALUES (@Username, @Password, @Email, @Role)";
            var parameters = new DynamicParameters();
            parameters.Add("Username", user.Username, DbType.String);
            parameters.Add("Password", user.Password, DbType.String);
            parameters.Add("Email", user.Email, DbType.String);
            parameters.Add("Role", user.Role, DbType.String);

            using (var connection = this.context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                response = "pass";
            }
            return response;
        }

        public async Task<List<User>> GetAll()
        {
            string query = "SELECT * FROM Users";
            using (var connection = this.context.CreateConnection())
            {
                var usersList = await connection.QueryAsync<User>(query);
                return usersList.ToList();
            }
        }

        public async Task<User?> GetById(int userId)
        {
            string query = "SELECT * FROM Users WHERE UserId=@UserId";
            using (var connection = this.context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<User>(query, new { userId });
            }
        }

        public async Task<string> Remove(int userId)
        {
            string response = string.Empty;
            string query = "DELETE FROM Users WHERE UserId=@UserId";
            using (var connection = this.context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { userId });
                response = "pass";
            }
            return response;
        }

        public async Task<string> Update(User user, int userId)
        {
            string response = string.Empty;
            string query = "UPDATE Users SET Username = @Username, Password = @Password, Email = @Email, Role = @Role WHERE UserId = @UserId";
            var parameters = new DynamicParameters();

            parameters.Add("Username", user.Username, DbType.String);
            parameters.Add("Password", user.Password, DbType.String);
            parameters.Add("Email", user.Email, DbType.String);
            parameters.Add("Role", user.Role, DbType.String);
            parameters.Add("UserId", userId, DbType.Int32);

            using (var connection = this.context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                response = "pass";
            }
            return response;
        }
    }
}
