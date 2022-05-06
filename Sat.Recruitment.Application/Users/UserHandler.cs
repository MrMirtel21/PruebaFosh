using MediatR;
using Sat.Recruitment.Application.Exceptions;
using Sat.Recruitment.Application.Users.Commands;
using Sat.Recruitment.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.Users
{
    public class UserHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserRepository _repository;
        private readonly IUserMoneyManager _userMoneyManager;

        public UserHandler(IUserRepository repository, 
            IUserMoneyManager userMoneyManager)
        {
            _repository = repository;
            _userMoneyManager = userMoneyManager;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User newUser = new User 
            {
                Name = request.Name,
                Address = request.Address,
                Email= request.Email,
                Phone = request.Phone,
                Type = UserType.FromName(request.UserType)
            };

            var rewardedMoney = _userMoneyManager.ApplyReward(request.Money,newUser.Type);
            newUser.SetMoney(rewardedMoney);

            await AbortIfDuplicatedUser(newUser);

            await _repository.Create(newUser);
            return new Unit();
        }

        private async Task AbortIfDuplicatedUser(User user) 
        {
            var actualUsers = await _repository.GetAll();
            if (actualUsers.Any(x => x == user))
            {
                throw new DuplicateObjectException(user.GetType().Name, user.ToString());
            }
        }

        private void ValidateCommand(CreateUserCommand request)
        {
            CreateUserCommandValidator validator = new CreateUserCommandValidator();
            validator.Validate(request);
        }
    }
}
