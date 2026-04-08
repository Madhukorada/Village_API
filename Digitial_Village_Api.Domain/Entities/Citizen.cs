using System;
using System.Collections.Generic;
using System.Text;

namespace Digitial_Village_Api.Domain.Entities
{
    public class Citizen
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string FamilyHead { get; set; } = string.Empty;
        public string Villagecode { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set;  } = string.Empty;
    }
}
