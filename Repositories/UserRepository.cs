using Entities;
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
    string filePath = "M:\\web-api\\OurStore\\OurStore\\users.txt";
    public User getById(int id)
    {
        using (StreamReader reader = System.IO.File.OpenText(filePath))
        {
            string? currentUserInFile;
            while ((currentUserInFile = reader.ReadLine()) != null)
            {
                User user = JsonSerializer.Deserialize<User>(currentUserInFile);
                if (user.userId == id)
                    return user;
            }
        }
        return null;
    }
    public User addUser(User user)
    {
        int numberOfUsers = System.IO.File.ReadLines(filePath).Count();
        user.userId = numberOfUsers + 1;
        string userJson = JsonSerializer.Serialize(user);
        System.IO.File.AppendAllText(filePath, userJson + Environment.NewLine);
        return user;
    }
    public User login(string email, string password)
    {
        using (StreamReader reader = System.IO.File.OpenText(filePath))
        {
            string? currentUserInFile;
            while ((currentUserInFile = reader.ReadLine()) != null)
            {
                User user = JsonSerializer.Deserialize<User>(currentUserInFile);
                if (user.Email == email && user.Password == password)
                    return user;
            }
        }
        return null;
    }
    public User updateUser(int id, User userToUpdate)
    {
        userToUpdate.userId = id;
        string textToReplace = string.Empty;
        using (StreamReader reader = System.IO.File.OpenText(filePath))
        {
            string currentUserInFile;
            while ((currentUserInFile = reader.ReadLine()) != null)
            {

                User user = JsonSerializer.Deserialize<User>(currentUserInFile);
                if (user.userId == id)
                    textToReplace = currentUserInFile;
            }
        }

        if (textToReplace != string.Empty)
        {
            string text = System.IO.File.ReadAllText(filePath);
            text = text.Replace(textToReplace, JsonSerializer.Serialize(userToUpdate));
            System.IO.File.WriteAllText(filePath, text);
        }
        return userToUpdate;
    }
   
}

