using Desafio.Core.Domain.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Core.Domain.Services.Implementations
{
    public class UserServices : IUserServices
    {
        private readonly object _userRepository;

        public UserServices(object userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<object> CheckUserExists(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}