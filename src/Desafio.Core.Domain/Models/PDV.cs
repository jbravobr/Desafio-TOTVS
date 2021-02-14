using Desafio.Core.Domain.Exceptions;
using Desafio.Core.Domain.Models.Validations;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Desafio.Core.Domain.Models
{
    public class Pdv : BaseEntity
    {
        public List<int> BankNotes { get; private set; }
        public List<int> BankCoins { get; private set; }

        public List<int> BankNotesToReturn { get; private set; }
        public List<int> BankCoinsToReturn { get; private set; }

        public bool IsValid { get; set; }
        public bool IsOpen { get; set; }
        public bool? IsClosed { get; set; }

        public Pdv()
        {
            BankCoins = new List<int>();
            BankNotes = new List<int>();

            BankNotesToReturn = new List<int>();
            BankCoinsToReturn = new List<int>();
        }

        public void InitiatePdv(List<int> notes = null, List<int> coins = null)
        {
            if (notes != null && notes.Any())
            {
                BankNotes = notes;
            }
            else
            {
                BankNotes = new List<int> { 100, 50, 20, 10 };
            }

            if (coins != null && coins.Any())
            {
                BankCoins = coins;
            }
            else
            {
                BankCoins = new List<int> { 50, 10, 05, 01 };
            }

            if (Validate().IsValid)
            {
                BankNotes = BankNotes.OrderByDescending(b => b).ToList();
                BankCoins = BankCoins.OrderByDescending(b => b).ToList();
                IsValid = true;
                IsOpen = true;
            }
            else
            {
                throw new PdvIniatializationError("Erro ao inicializar o Pdv");
            }
        }

        public bool CheckBalance(double amount)
        {
            if (BankNotes.Sum(b => b) + BankCoins.Sum(b => b) < amount)
            {
                return false;
            }

            return true;
        }

        public void Compute(double total, double amountPayed)
        {
            if (total > amountPayed)
            {
                throw new PaymentInsufficientError("Valor pago não cobre o valor devido!");
            }

            var change = amountPayed - total;

            if (!CheckBalance(change))
            {
                throw new InsufficientPdvBalanceError("Saldo indiponível para realização do troco");
            }

            var interger = Convert.ToInt32(Math.Floor(change));
            var fractional = Convert.ToInt32(Math.Round((change - (int)change) * 100));
            var control = false;

            if (change > 0)
            {
                for (int i = 0; i < BankNotes.Count; i++)
                {
                    var qtd_notes = interger / BankNotes[i];
                    BankNotesToReturn.Add(qtd_notes);

                    interger -= (qtd_notes * BankNotes[i]);
                }

                if (interger > 0)
                {
                    for (int i = 0; i < BankCoins.Count; i++)
                    {
                        var qtd_coins = interger * 100 / BankCoins[i];
                        BankCoinsToReturn.Add(qtd_coins);

                        interger -= (qtd_coins * BankCoins[i] / 100);
                    }
                    control = true;
                }

                for (int i = 0; i < BankCoins.Count; i++)
                {
                    var qtd_coins = fractional / BankCoins[i];
                    if (control)
                    {
                        BankCoinsToReturn[i] += qtd_coins;
                    }
                    else
                    {
                        BankCoinsToReturn.Add(qtd_coins);
                    }

                    fractional -= (qtd_coins * BankCoins[i]);
                }

                IsClosed = true;
            }
            else
            {
                throw new InvalidOperationException("Cliente não necessita de troco.");
            }
        }

        private ValidationResult Validate()
        {
            PdvValidator validator = new PdvValidator();
            ValidationResult results = validator.Validate(this);

            return results;
        }
    }
}