using Digitial_Village_Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Digital_Village_Api.Application.Interface
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
