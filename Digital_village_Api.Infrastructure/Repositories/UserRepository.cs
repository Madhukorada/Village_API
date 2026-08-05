using Digital_Village_Api.Application.Interface;
using Digitial_Village_Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Digital_village_Api.Infrastructure.Repositories
{
    public class UserRepository:IUserRepository
    {
        public static List<User> users = new List<User>
        {
         new User{Id=1,UserName="admin",Email="admin@gmail.com",PasswordHash="Pass@123%",Role=new List<string> {"Admin","User"}},
         new User{Id=2,UserName="user",Email="user@gmail.com",PasswordHash="Pass@123%",Role=new List<string> {"User"}},
         new User{Id=3,UserName="test",Email="test@gmail.com",PasswordHash="Pass@123%",Role=new List<string> {"Admin"}},

        };

        public Task<User?> GetUserByUsernameAsync(string username)
        {
            var user = users.FirstOrDefault(x =>
                x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));

            return Task.FromResult(user);
        }
    }
}
