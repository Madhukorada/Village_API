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
                var user = await _userRepository
                    .GetUserByUsernameAsync(request.UserName);

                if (user == null)
                    return null;

                if (user.PasswordHash != request.Password)
                    return null;

                return new LoginResponse
                {
                    Token = _jwtTokenGenerator.GenerateToken(user)
                };
            }
        }
}
