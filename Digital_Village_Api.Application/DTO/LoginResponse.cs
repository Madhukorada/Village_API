using System;
using System.Collections.Generic;
using System.Text;

namespace Digital_Village_Api.Application.DTO
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Name{get;set;}
        public string Role{get;set;}
        public int RegistrationId {get;set;}
    
    }
}
