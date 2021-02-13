using Desafio.Core.Domain.Models;
using Desafio.Core.Services.Implementations;
using Desafio.Infra.Repositories.Contracts.Base;
using Moq;
using Xunit;

namespace Desafio.Tests.Services
{
    public class UserServicesTests
    {
        private User GetTestUser => new User() { Email = "jbravo.br@gmail.com", Password = "P@ssw0rd!" };

        private readonly Mock<IUnitOfWork> mockUnitOfWork;

        public UserServicesTests()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public void CheckUserExists()
        {
            var testUser = GetTestUser;
            mockUnitOfWork.Setup(x => x.UserRepository.CheckUserExists(testUser.Email, testUser.Password)).Returns(testUser);

            var userServices = new UserServices(mockUnitOfWork.Object);
            var user = userServices.CheckUserExists(testUser.Email, testUser.Password);

            Assert.NotNull(user);
            Assert.Equal("jbravo.br@gmail.com", user.Email);
            Assert.Equal("P@ssw0rd!", user.Password);
        }
    }
}