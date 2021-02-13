using System;

namespace Desafio.Core.Domain.Exceptions
{
    public class PdvIniatializationError : ApplicationException
    {
        public PdvIniatializationError(string message) : base(message)
        {
        }
    }
}