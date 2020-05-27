using ConsoleAppRE;
using Core.Domain.CommandHandlers;
using Core.Domain.Events;
using Core.Domain.Interfaces;
using Example.Domain.Commands;
using Example.Domain.Interfaces.Queries;
using Example.Domain.Interfaces.Repositories;
using Example.Domain.Validations;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Domain.CommandsHandler
{
    public class AddCountryCommandHandler : CommandHandler<AddCountryCommand, Countries>
    {
        private readonly ICountryRepository _countryRepository;

        private readonly IRegionQuery _regionQuery;

        public AddCountryCommandHandler(
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notificationHandler,
            ICountryRepository countryRepository,
            ICountryQuery countryQuery,
            IRegionQuery regionQuery)
                : base(bus, notificationHandler, new AddCountryCommandValidation(countryQuery, regionQuery))
        {
            _countryRepository = countryRepository;
            _regionQuery = regionQuery;
        }

        public override async Task<Countries> Handle(AddCountryCommand command, CancellationToken cancellationToken)
        {
            var validado = await IsValidAsync(command, cancellationToken).ConfigureAwait(false);

            if (!validado)
            {
                return null;
            }

            var region = await _regionQuery.Get(command.RegionId.Value, cancellationToken);

            var country = new Countries(command.CountryId, command.CountryName, region);
            _countryRepository.Add(country);

            await _countryRepository.SaveChanges(cancellationToken);

            return country;
        }
    }
}