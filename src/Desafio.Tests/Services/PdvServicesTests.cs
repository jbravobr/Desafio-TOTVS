using Desafio.Core.Domain.Models;
using Desafio.Core.Domain.Models.Enums;
using Desafio.Core.Services.Implementations;
using Desafio.Infra.Repositories.Contracts.Base;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Desafio.Tests.Services
{
    public class PdvServicesTests
    {
        private ICollection<PdvHistory> GetTestPdvHistories => new List<PdvHistory>
        {
            new PdvHistory
            {
                 Amount = 100,
                  CreatedAt = DateTime.Now.AddDays(1),
                   OperationType = OperationType.In
            },
            new PdvHistory
            {
                 Amount = 300,
                  CreatedAt = DateTime.Now.AddDays(10),
                   OperationType = OperationType.Out
            }
        };

        private readonly Mock<IUnitOfWork> mockUnitOfWork;

        public PdvServicesTests()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public void PayBill()
        {
            var amout = 100;
            var total = 90;

            mockUnitOfWork.Setup(x => x.PdvHistoryRepository.Insert(It.IsAny<PdvHistory>()));

            var pdvServices = new PdvServices(mockUnitOfWork.Object);
            var pdvResult = pdvServices.PayBill(amout, total);

            mockUnitOfWork.Verify(x => x.PdvHistoryRepository.Insert(It.IsAny<PdvHistory>()), Times.Exactly(2));
            Assert.NotNull(pdvResult);
            Assert.True(pdvResult.IsClosed);
            Assert.Equal(1, pdvResult.BankNotesToReturn[3]);
        }

        [Fact]
        public void CheckUserExists()
        {
            mockUnitOfWork.Setup(x => x.PdvHistoryRepository.GetPaymentsHistory(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(GetTestPdvHistories);

            var pdvServices = new PdvServices(mockUnitOfWork.Object);
            var histories = pdvServices.GetPdvHistories(It.IsAny<DateTime>(), It.IsAny<DateTime>());

            Assert.NotNull(histories);
            Assert.Collection(histories,
                              item => Assert.Equal(100, item.Amount),
                              item => Assert.Equal(300, item.Amount));
        }
    }
}