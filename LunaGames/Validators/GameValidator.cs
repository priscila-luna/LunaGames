using FluentValidation;
using LunaGames.Models;


namespace LunaGames.Validators;

public class GameValidator : AbstractValidator<Game>
{
    public GameValidator()
    {
        RuleFor(u => u.Description)
            .NotEmpty().WithMessage("Descrição é obrigatório")
            .MinimumLength(10).WithMessage("A descrição deve ter pelo menos 10 caracteres");

        RuleFor(u => u.Title)
            .NotEmpty().WithMessage("Titulo é obrigatória")
            .MinimumLength(3).WithMessage("O titulo deve ter pelo menos 3 caracteres");
    }
}
