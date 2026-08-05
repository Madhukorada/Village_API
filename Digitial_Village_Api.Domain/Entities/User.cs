using System;
using System.Collections.Generic;
using System.Text;

namespace Digitial_Village_Api.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public string Email { get; set;}

        public string PasswordHash { get; set; }

        public List<string> Role { get; set; }
    }
}
