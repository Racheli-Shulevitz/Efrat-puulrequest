using Entities;

namespace Services
{
    public interface IUserService
    {
        User addUser(User user);
        User getById(int id);
        User login(string email, string password);
        User updateUser(int id, User userToUpdate);
        int checkPassword(string password);
    }
}