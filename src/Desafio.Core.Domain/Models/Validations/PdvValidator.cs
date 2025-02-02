﻿using FluentValidation;

namespace Desafio.Core.Domain.Models.Validations
{
    public class PdvValidator : AbstractValidator<Pdv>
    {
        public PdvValidator()
        {
            RuleFor(pdv => pdv.BankNotes).ForEach(x => x.GreaterThan(0));
            RuleFor(pdv => pdv.BankCoins).ForEach(x => x.GreaterThan(0));
        }
    }
}