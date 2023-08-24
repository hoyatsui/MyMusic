using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MyMusic.Api.Resources;

namespace MyMusic.Api.Validators
{
    public class SaveArtistResourceValidator : AbstractValidator<SaveArtistResource>
    {
        public SaveArtistResourceValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}