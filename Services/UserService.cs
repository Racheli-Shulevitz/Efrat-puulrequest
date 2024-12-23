using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services;

public class UserService : IUserService
{

    IUserRepository userRepository;

    public UserService(IUserRepository _userRepository)
    {
        userRepository = _userRepository;
    }

    public async Task<User> addUser(User user)
    {
        if (checkPassword(user.Password) >= 3)
            return await userRepository.addUser(user);
        else
            return null;
    }
    public Task<User> login(string email, string password)
    {
        return userRepository.login(email, password);
    }
    public Task<User> updateUser(int id, User userToUpdate)
    {
        if (checkPassword(userToUpdate.Password) >= 3)
            return userRepository.updateUser(id, userToUpdate);
        else 
            return null;
    }
    public async Task<User> getById(int id)
    {
        return await userRepository.getById(id);
    }
    public int checkPassword(string password)
    {
        return Zxcvbn.Core.EvaluatePassword(password).Score;

    }
}
