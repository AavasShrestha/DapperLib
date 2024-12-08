using DapperLib.Model;

namespace DapperLib.Repo
{
    public interface IUserRepo
    {
        Task<string> Create(User user);
        Task<List<User>> GetAll();
        Task<User?> GetById(int userId);
        Task<string> Remove(int userId);
        Task<string> Update(User user, int userId);
    }
}
