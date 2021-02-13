using System;
using System.ComponentModel.DataAnnotations;

namespace Desafio.Core.Domain.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}