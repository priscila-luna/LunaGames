using FluentValidation;
using LunaGames.Models;


namespace LunaGames.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email é obrigatório")
            .EmailAddress().WithMessage("Email inválido");

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("Senha é obrigatória")
            .MinimumLength(8).WithMessage("A senha deve ter pelo menos 8 caracteres")
            .Matches(@"[A-Za-z]").WithMessage("Senha deve conter pelo menos uma letra")
            .Matches(@"\d").WithMessage("Senha deve conter pelo menos um número")
            .Matches(@"[\!\@\#\$\%\^\&\*\(\)\-\+\=\{\}\[\]\:\;\'\""\<\>\,\.\?\|\\\/]").WithMessage("Senha deve conter pelo menos um caractere especial");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Permissão é obrigatória")
            .Must(role => role == "Admin" || role == "User").WithMessage("A permissão deve ser 'Admin' ou 'User'.");
    }
}
