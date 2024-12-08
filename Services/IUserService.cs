using Entities;

namespace Services
{
    public interface IUserService
    {
        Task<User> addUser(User user);
        User getById(int id);
        Task<User> login(string email, string password);
        Task<User> updateUser(int id, User userToUpdate);
        int checkPassword(string password);
    }
}