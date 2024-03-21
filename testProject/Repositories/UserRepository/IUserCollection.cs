using testProject.Models;

namespace testProject.Repositories.UserRepository
{
    public interface IUserCollection
    {
        void InsertUser(User user);
        void UpdateUser(User user);
        void DeleteUser(string id);
        Task<List<User>> GetAll();
        User GetUserById(string id);
        User GetUserByUsername(string username);
    }
}
