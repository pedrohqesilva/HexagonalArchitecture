using Core.Domain.Events;

namespace Core.Domain.Commands
{
    public class Command<TResponse> : Message<TResponse>
    {
        protected Command()
        {
        }
    }
}