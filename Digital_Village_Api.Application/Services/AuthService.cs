using Digital_Village_Api.Application.DTO;
using Digital_Village_Api.Application.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Digital_Village_Api.Application.Services
{
        public class AuthService
        {
            private readonly IUserRepository _userRepository;
            private readonly IJwtTokenGenerator _jwtTokenGenerator;

            public AuthService(
                IUserRepository userRepository,
                IJwtTokenGenerator jwtTokenGenerator)
            {
                _userRepository = userRepository;
                _jwtTokenGenerator = jwtTokenGenerator;
            }

            public async Task<LoginResponse> LoginAsync(LoginRequest request)
            {
                var UserInfo = await _userRepository
                    .GetUserByUsernameAsync(request.UserName);

                if (UserInfo == null)
                    return null;

                if (UserInfo.Password != request.Password)
                    return null;

                return new LoginResponse
                {
                    Token = _jwtTokenGenerator.GenerateToken(UserInfo),
                    Name=$"{UserInfo.FirstName} {UserInfo.LastName}",
                    Role=UserInfo.Role,
                    RegistrationId=UserInfo.RegistrationId


                };
            }
        }
}
