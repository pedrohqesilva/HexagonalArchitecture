using Core.Domain.Events;
using Core.Domain.ValueObjects;
using System;
using Xunit;

namespace Core.Test.Domain.Aggregates
{
    public class EventLogTest
    {
        [Fact]
        public void Log_de_evento_deve_ser_criado()
        {
            var timestamp = DateTime.Today;
            var fullName = typeof(EventLogTest).FullName;
            var content = "content";

            var @event = new EventLog(Guid.NewGuid(), timestamp, fullName, content);

            Assert.Equal(@event.CreationDate, timestamp);
            Assert.Equal(EventStateEnum.NotPublished, @event.State);
            Assert.Equal(@event.Content, content);
            Assert.NotEqual(@event.Id, Guid.Empty);
            Assert.Equal(@event.EventTypeName, fullName);
        }

        [Fact]
        public void Log_de_evento_deve_ser_alterado_para_publicado()
        {
            var timestamp = DateTime.Today;
            var fullName = typeof(EventLogTest).FullName;
            var content = "content";

            var @event = new EventLog(Guid.NewGuid(), timestamp, fullName, content);

            @event.MarkEventAsPublished();

            Assert.True(@event.State.HasFlag(EventStateEnum.NotPublished));
            Assert.True(@event.State.HasFlag(EventStateEnum.Published));
        }

        [Fact]
        public void Log_de_evento_deve_ser_alterado_para_publicacao_falhou()
        {
            var timestamp = DateTime.Today;
            var fullName = typeof(EventLogTest).FullName;
            var content = "content";

            var @event = new EventLog(Guid.NewGuid(), timestamp, fullName, content);

            @event.MarkEventAsFailed();

            Assert.True(@event.State.HasFlag(EventStateEnum.PublishedFailed));
        }

        [Fact]
        public void Log_de_evento_deve_ser_alterado_para_processsado()
        {
            var timestamp = DateTime.Today;
            var fullName = typeof(EventLogTest).FullName;
            var content = "content";

            var @event = new EventLog(Guid.NewGuid(), timestamp, fullName, content);

            @event.MarkEventAsPublished();
            @event.MarkEventAsProcessed();

            Assert.True(@event.State.HasFlag(EventStateEnum.NotPublished));
            Assert.True(@event.State.HasFlag(EventStateEnum.Published));
            Assert.True(@event.State.HasFlag(EventStateEnum.Processed));
        }

        [Fact]
        public void Log_de_evento_nao_deve_ser_alterado_para_processsado()
        {
            var timestamp = DateTime.Today;
            var fullName = typeof(EventLogTest).FullName;
            var content = "content";

            var @event = new EventLog(Guid.NewGuid(), timestamp, fullName, content);

            @event.MarkEventAsProcessed();

            Assert.False(@event.State.HasFlag(EventStateEnum.Processed));
        }
    }
}