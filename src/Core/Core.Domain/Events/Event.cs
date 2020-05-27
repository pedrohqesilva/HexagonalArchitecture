using MediatR;
using System;

namespace Core.Domain.Events
{
    public abstract class Event : Message<bool>, INotification
    {
        public DateTime Timestamp { get; private set; }
        public Guid Id { get; private set; }

        protected Event()
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.Now;
        }
    }
}