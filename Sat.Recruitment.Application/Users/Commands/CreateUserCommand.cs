using FluentValidation;
using MediatR;
using Sat.Recruitment.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Application.Users.Commands
{
    public class CreateUserCommand : IRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public decimal Money { get; set; }
    }

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand> 
    {
        public CreateUserCommandValidator() 
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x=>x.UserType).NotEmpty()
                                  .Must(s=>UserType.ExistName(s?.ToLower()))
                                  .WithMessage("Invalid User Type");
            RuleFor(x => x.Money).GreaterThanOrEqualTo(0);
        }
    }
}
