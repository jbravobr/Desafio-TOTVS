using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Desafio.Core.Domain.Models.Validations;
using FluentValidation.Results;

namespace Desafio.Core.Domain.Models
{
    public class Pdv : BaseEntity
    {
        public List<int> BankNotes { get; private set; }
        public List<int> BankCoins { get; private set; }

        public List<int> BankNotesToReturn { get; private set; }
        public List<int> BankCoinsToReturn { get; private set; }

        public Pdv()
        {
            BankCoins = new List<int>();
            BankNotes = new List<int>();

            BankNotesToReturn = new List<int>();
            BankCoinsToReturn = new List<int>();
        }

        public bool InitiatePdv(List<int> notes = null, List<int> coins = null)
        {
            if (notes != null && notes.Any())
            {
                BankNotes = notes;
            }

            if (coins != null && coins.Any())
            {
                BankCoins = coins;
            }

            if (true)
            {
                BankNotes = BankNotes.OrderByDescending(b => b).ToList();
                BankCoins = BankCoins.OrderByDescending(b => b).ToList();

                return true;
            }
            else
            {
                throw new InvalidOperationException("Erro ao inicializar o Pdv");
            }
        }

        //public object Compute(double total, double amountPayed)
        //{
        //    if (total > amountPayed)
        //    {
        //        throw new InvalidOperationException("Valor pago não cobre o valor devido!");
        //    }

        //    var change = amountPayed - total;
        //    var interger = Convert.ToInt32(Math.Floor(change));
        //    var fractional = Convert.ToInt32(Math.Round((change - (int)change) * 100));

        //    if (change > 0)
        //    {
        //        for (int i = 0; i < BankNotes.Count; i++)
        //        {
        //            var qtd_notes = interger / BankNotes[i];
        //            //notas_entregues[i] = qtd;

        //            interger -= (qtd_notes * BankNotes[i]);
        //        }

        //        if (interger > 0)
        //        {
        //            for (int i = 0; i < BankCoins.Count; i++)
        //            {
        //                var qtd_coins = interger * 100 / BankCoins[i];
        //                //moedas_entregues[i] = qtd_coins;

        //                interger -= (qtd_coins * BankCoins[i] / 100);
        //            }
        //        }

        //        for (int i = 0; i < BankCoins.Count; i++)
        //        {
        //            var qtd_coins = fractional / BankCoins[i];
        //            //moedas_entregues[i] += qtd_moeda;

        //            fractional -= (qtd_coins * BankCoins[i]);
        //        }
        //    }
        //    else
        //    {
        //        throw new InvalidOperationException("Cliente não necessita de troco.");
        //    }

        //    if (notas_entregues.Length > 0)
        //    {
        //        Console.ForegroundColor = ConsoleColor.Blue;

        //        Console.WriteLine($"O troco será de: R$ {troco}");

        //        Console.WriteLine($"Notas de 100: {notas_entregues[0]}");
        //        Console.WriteLine($"Notas de 50: {notas_entregues[1]}");
        //        Console.WriteLine($"Notas de 20: {notas_entregues[2]}");
        //        Console.WriteLine($"Notas de 10: {notas_entregues[3]}");

        //        Console.WriteLine($"Moedas de 50 centavos: {moedas_entregues[0]}");
        //        Console.WriteLine($"Moedas de 10 centavos: {moedas_entregues[1]}");
        //        Console.WriteLine($"Moedas de 5 centavos: {moedas_entregues[2]}");
        //        Console.WriteLine($"Moedas de 1 centavos: {moedas_entregues[3]}");
        //    }

        //    Console.WriteLine("Pressione qualquer tecla para sair...");
        //    Console.ReadKey();
        //}
    }

    //private ValidationResult Validate()
    //{
    //    PdvValidator validator = new PdvValidator();
    //    ValidationResult results = validator.Validate(this);

    //    return results;
    //}
}