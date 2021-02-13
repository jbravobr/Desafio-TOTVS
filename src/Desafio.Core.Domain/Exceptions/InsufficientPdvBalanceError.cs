using System;

namespace Desafio.Core.Domain.Exceptions
{
    public class InsufficientPdvBalanceError : ApplicationException
    {
        public InsufficientPdvBalanceError(string message) : base(message)
        {
        }
    }
}