using MediatR;

namespace Core.Domain.Events
{
    public class Message<TReseponse> : IRequest<TReseponse>
    {
        protected string MessageType { get; set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}