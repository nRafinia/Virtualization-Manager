using FluentValidation;
using SharedKernel.Base;

namespace Authorizer.Local.Application.Users.Commands.AddUser;

public class AddUserCommandValidator:RequestValidator<AddUserCommand>
{
    public AddUserCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.DisplayName).NotEmpty();
    }
}