using Digital_Village_Api.Application.DTO;
using Digital_Village_Api.Application.Interface;
using Digitial_Village_Api.Domain.Entities;
using Digitial_Village_Api.Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Digital_village_Api.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly VillageDbContext _villageDbContext;
        public UserRepository(VillageDbContext villageDbContext)
        {
            _villageDbContext = villageDbContext;
        }
        public static List<User> users = new List<User>
        {
         new User{Id=1,UserName="admin",Email="admin@gmail.com",PasswordHash="Pass@123%",Role=new List<string> {"Admin","User"}},
         new User{Id=2,UserName="user",Email="user@gmail.com",PasswordHash="Pass@123%",Role=new List<string> {"User"}},
         new User{Id=3,UserName="test",Email="test@gmail.com",PasswordHash="Pass@123%",Role=new List<string> {"Admin"}},

        };

        public async Task<UserInfo?> GetUserByUsernameAsync(string username)

        {

            return await _villageDbContext.ViRegistrations

            .Where(x => x.Email.ToLower() == username.ToLower())

            .Select(x => new UserInfo

            {

                Email = x.Email,

                Role = x.Role,

                FirstName = x.FirstName,

                LastName = x.LastName,
                Password=x.Password,
                RegistrationId=x.RegistrationId

            }).FirstOrDefaultAsync();

        }
    }
}
