﻿namespace Desafio.Core.Domain.Models
{
    public class User : BaseEntity
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool Enabled { get; set; }
    }
}