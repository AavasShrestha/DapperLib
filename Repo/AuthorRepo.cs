using Dapper;
using DapperLib.Model;
using DapperLib.Model.Data;
using System.Data;

namespace DapperLib.Repo
{
    public class AuthorRepo : IAuthorRepo
    {
        private readonly DapperDbContext context;

        public AuthorRepo(DapperDbContext context)
        {
            this.context = context;
        }

        public async Task<string> Create(Authors authors)
        {
            string response = string.Empty;
            string query = "INSERT INTO Authors (Name, Bio) VALUES (@Name, @Bio)"; // Remove AuthorId from insert query
            var parameters = new DynamicParameters();
            parameters.Add("Name", authors.Name, DbType.String);
            parameters.Add("Bio", authors.Bio, DbType.String);

            using (var connection = this.context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, parameters);
                if (rowsAffected > 0)
                {
                    response = "Author successfully created.";
                }
                else
                {
                    response = "Failed to create author.";
                }
            }
            return response;
        }



        public async Task<List<Authors>> GetAll()
        {
            string query = "SELECT * FROM Authors";
            using (var connection = this.context.CreateConnection())
            {
                var authlist = await connection.QueryAsync<Authors>(query);
                return authlist.ToList();
            }
        }

#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        public async Task<Authors?> GetbyCode(int code)
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        {
            string query = "SELECT * FROM Authors WHERE AuthorId = @AuthorId";
            using (var connection = this.context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Authors>(query, new { AuthorId = code });
            }
        }


        public async Task<string> Remove(int code)
        {
            string response = string.Empty;
            string query = "DELETE FROM Authors WHERE AuthorId = @AuthorId";

            using (var connection = this.context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { AuthorId = code });
                if (rowsAffected > 0)
                {
                    response = "Successfully removed author.";
                }
                else
                {
                    response = "No author found to delete.";
                }
            }
            return response;
        }



        public async Task<string> Update(Authors authors, int code)
        {
            string response = string.Empty;
            string query = "UPDATE Authors SET AuthorId = @AuthorId, Name = @Name, Bio = @Bio WHERE AuthorId = @AuthorId"; // Add WHERE clause
            var parameters = new DynamicParameters();
            parameters.Add("AuthorId", authors.AuthorId, DbType.Int32);
            parameters.Add("Name", authors.Name, DbType.String);
            parameters.Add("Bio", authors.Bio, DbType.String);

            using (var connection = this.context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                response = "pass";
            }
            return response;
        }

    }
}
