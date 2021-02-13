using Desafio.Core.Domain.Models.Enums;
using System;

namespace Desafio.Core.Domain.Models
{
    public class PdvHistory : BaseEntity
    {
        public OperationType OperationType { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedAt { get; set; }

        public PdvHistory()
        {
        }
    }
}