using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Repositories;

public class UserRepository : IUserRepository
{
    OurStoreContext _OurStoreContext;
    public UserRepository(OurStoreContext ourStoreContext)
    {
        _OurStoreContext = ourStoreContext;
    }
    string filePath = "M:\\web-api\\OurStore\\OurStore\\users.txt";
    public User getById(int id)
    {
        using (StreamReader reader = System.IO.File.OpenText(filePath))
        {
            string? currentUserInFile;
            while ((currentUserInFile = reader.ReadLine()) != null)
            {
                User user = JsonSerializer.Deserialize<User>(currentUserInFile);
                if (user.Id == id)
                    return user;
            }
        }
        return null;
    }
    public async Task<User> addUser(User user)
    {
        await _OurStoreContext.Users.AddAsync(user);
        await _OurStoreContext.SaveChangesAsync();
        return user;
    }
    public async Task<User> login(string email, string password)
    {
       return await _OurStoreContext.Users.FirstOrDefaultAsync(user=>user.Email==email&& user.Password == password);
    }
    public async Task<User> updateUser(int id, User userToUpdate)
    {
        userToUpdate.Id = id;
         _OurStoreContext.Users.Update(userToUpdate);
        await _OurStoreContext.SaveChangesAsync();
        return userToUpdate;
    }
   
}

