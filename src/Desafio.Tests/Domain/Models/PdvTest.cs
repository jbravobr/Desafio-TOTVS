using Desafio.Core.Domain.Exceptions;
using Desafio.Core.Domain.Models;
using System.Collections.Generic;
using Xunit;

namespace Desafio.Tests.Domain.Models
{
    public class PdvTest
    {
        private List<int> GenerateTestBankNotes => new List<int> { 100, 50, 20, 10 };
        private List<int> GenerateTestBankCoins => new List<int> { 50, 10, 05, 01 };
        private List<int> GenerateTestBankNotesInsufficient => new List<int> { 50, 10, 05, 01 };

        [Fact]
        public void InitiatePdv_With_Positive_Balance_For_BankNotes()
        {
            Pdv _pdv = new Pdv();
            _pdv.InitiatePdv(GenerateTestBankNotes, null);

            Assert.True(_pdv.IsValid);
        }

        [Fact]
        public void InitiatePdv_With_Positive_Balance_For_BankCoins()
        {
            Pdv _pdv = new Pdv();
            _pdv.InitiatePdv(null, GenerateTestBankCoins);

            Assert.True(_pdv.IsValid);
        }

        [Fact]
        public void InitiatePdv_With_Invalid_Balance_For_BankNotes()
        {
            List<int> notes = new List<int>
            {
                0,
                -10,
                -50
            };

            Pdv _pdv = new Pdv();

            Assert.Throws<PdvIniatializationError>(() => _pdv.InitiatePdv(notes, null));
        }

        [Fact]
        public void InitiatePdv_With_Invalid_Balance_For_BankCoins()
        {
            List<int> coins = new List<int>
            {
                0,
                -01,
                -05
            };

            Pdv _pdv = new Pdv();

            Assert.Throws<PdvIniatializationError>(() => _pdv.InitiatePdv(coins, null));
        }

        [Fact]
        public void Compute_With_Insufficient_Amout_Payed()
        {
            Pdv _pdv = new Pdv();

            Assert.Throws<PaymentInsufficientError>(() => _pdv.Compute(100, 90));
        }

        [Fact]
        public void Compute_With_Insufficient_Pdv_Balance()
        {
            Pdv _pdv = new Pdv();
            _pdv.InitiatePdv(GenerateTestBankNotesInsufficient);

            Assert.Throws<InsufficientPdvBalanceError>(() => _pdv.Compute(80, 100));
        }

        [Fact]
        public void Compute_With_Suficient_Pdv_Balance()
        {
            Pdv _pdv = new Pdv();
            _pdv.InitiatePdv(GenerateTestBankNotes);
            _pdv.Compute(80, 100);

            Assert.True(_pdv.IsClosed);
        }
    }
}