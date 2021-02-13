using System;

namespace Desafio.Core.Domain.Models
{
    public class Log : BaseEntity
    {
        public string Request { get; set; }
        public string Response { get; set; }
        public DateTime CreateAt { get; set; }
    }
}