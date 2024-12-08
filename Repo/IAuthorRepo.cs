using DapperLib.Model;

namespace DapperLib.Repo
{
    public interface IAuthorRepo
    {
        Task<List<Authors>> GetAll();
        Task<Authors> GetbyCode(int code);

        Task<string > Create (Authors authors);

        Task<string> Update(Authors authors, int code);

        Task<string> Remove(int code);



    }
}
