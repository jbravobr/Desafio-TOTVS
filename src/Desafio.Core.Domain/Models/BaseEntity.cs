using System;

namespace Desafio.Core.Domain.Models
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}