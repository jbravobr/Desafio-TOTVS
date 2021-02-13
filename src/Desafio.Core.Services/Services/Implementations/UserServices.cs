using Desafio.Core.Domain.Models;
using Desafio.Core.Services.Contracts;
using Desafio.Infra.Repositories.Contracts.Base;

namespace Desafio.Core.Services.Implementations
{
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User CheckUserExists(string email, string password)
        {
            return _unitOfWork.UserRepository.CheckUserExists(email, password);
        }
    }
}