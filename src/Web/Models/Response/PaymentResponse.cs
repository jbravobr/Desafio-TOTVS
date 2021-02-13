using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Models.Response
{
    public class PaymentResponse
    {
        public PaymentResponse(List<int> notes = null, List<int> coins = null)
        {
            var sb = new StringBuilder();

            if (notes != null && notes.Count > 0)
            {
                /*
                 * 100
                 * 50
                 * 20
                 * 10
                 */

                BankNotes.Add(new BankNoteResponse(100, notes[0]));
                BankNotes.Add(new BankNoteResponse(50, notes[1]));
                BankNotes.Add(new BankNoteResponse(20, notes[2]));
                BankNotes.Add(new BankNoteResponse(10, notes[3]));

                foreach (var item in BankNotes.Where(b => b.Quantity > 0))
                {
                    sb.Append($"Entregar {item.Quantity} nota de R${item.Value},00");
                }
            }

            if (coins != null && coins.Count > 0)
            {
                /*
                 * 50
                 * 10
                 * 05
                 * 01
                 */

                BankCoins.Add(new BankCoinsResponse(50, notes[0]));
                BankCoins.Add(new BankCoinsResponse(10, notes[1]));
                BankCoins.Add(new BankCoinsResponse(05, notes[2]));
                BankCoins.Add(new BankCoinsResponse(01, notes[3]));

                foreach (var item in BankCoins.Where(b => b.Quantity > 0))
                {
                    sb.Append($"Entregar {item.Quantity} moeda de R$0,{item.Value}");
                }
            }

            if (sb.Length > 0)
            {
                Mensagem = sb.ToString();
            }
        }

        public ICollection<BankNoteResponse> BankNotes { get; private set; }
        public ICollection<BankCoinsResponse> BankCoins { get; private set; }
        public string Mensagem { get; private set; }
    }

    public class BankNoteResponse : MoneyResponse
    {
        public BankNoteResponse(int value, int quantity) : base(value, quantity)
        {
        }
    }

    public class BankCoinsResponse : MoneyResponse
    {
        public BankCoinsResponse(int value, int quantity) : base(value, quantity)
        {
        }
    }

    public abstract class MoneyResponse
    {
        public int Value { get; set; }
        public int Quantity { get; set; }

        public MoneyResponse(int value, int quantity)
        {
            Value = value;
            Quantity = quantity;
        }
    }
}