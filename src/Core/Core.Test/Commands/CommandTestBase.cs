using Core.Domain.Events;
using Core.Domain.Interfaces;
using Core.Infrastructure.Data.Context;
using Core.Test.Mock;
using MediatR;
using System.Threading.Tasks;

namespace Core.Test.Commands
{
    public class CommandTestBase
    {
        protected readonly BaseContext _context;

        protected readonly IMediatorHandler _bus;
        protected readonly INotificationHandler<DomainNotification> _domainNotificationHandler;

        public CommandTestBase(BaseContext context)
        {
            _context = context;

            _domainNotificationHandler = MockDomainNotificationHandler.Get();
            _bus = MockMediatorHandler.Get();
        }

        protected Task LimparContexto()
        {
            return _context.Database.EnsureDeletedAsync();
        }

        protected Task SalvarContexto()
        {
            return _context.SaveChangesAsync();
        }
    }
}