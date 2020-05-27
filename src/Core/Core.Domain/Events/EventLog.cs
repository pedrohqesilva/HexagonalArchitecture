using Core.Domain.ValueObjects;
using System;

namespace Core.Domain.Events
{
    public class EventLog
    {
        public Guid Id { get; private set; }
        public string EventTypeName { get; private set; }
        public EventStateEnum State { get; set; }
        public DateTime CreationDate { get; private set; }
        public string Content { get; private set; }

        private EventLog(Guid id)
        {
        }

        public EventLog(Guid eventId, DateTime timestamp, string fullName, string content)
        {
            Id = eventId;
            CreationDate = timestamp;
            EventTypeName = fullName;
            Content = content;
            State = EventStateEnum.NotPublished;
        }

        public void MarkEventAsFailed()
        {
            State = EventStateEnum.PublishedFailed;
        }

        public void MarkEventAsPublished()
        {
            if (State.HasFlag(EventStateEnum.NotPublished))
            {
                State |= EventStateEnum.Published;
            }
        }

        public void MarkEventAsProcessed()
        {
            if (State.HasFlag(EventStateEnum.Published))
            {
                State |= EventStateEnum.Processed;
            }
        }
    }
}