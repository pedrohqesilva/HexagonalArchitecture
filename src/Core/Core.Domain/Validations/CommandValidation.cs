using Core.Domain.Commands;
using FluentValidation;

namespace Core.Domain.Validations
{
    public abstract class CommandHandlerValidation<TCommand, TResponse> : AbstractValidator<TCommand> where TCommand : Command<TResponse>
    {
    }
}