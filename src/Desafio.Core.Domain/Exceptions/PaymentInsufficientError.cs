using System;

namespace Desafio.Core.Domain.Exceptions
{
    public class PaymentInsufficientError : ApplicationException
    {
        public PaymentInsufficientError(string message) : base(message)
        {
        }
    }
}