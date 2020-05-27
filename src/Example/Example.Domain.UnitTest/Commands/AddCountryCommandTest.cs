using ConsoleAppRE;
using Core.Test.Commands;
using Example.Domain.Commands;
using Example.Domain.CommandsHandler;
using Example.Domain.Interfaces.Queries;
using Example.Domain.Interfaces.Repositories;
using Example.Infrastructure.Data.Context;
using System.Threading;
using Xunit;

namespace Example.Domain.UnitTest.Commands
{
    public class AddCountryCommandTest : CommandTestBase
    {
        private readonly ExampleContext _context;

        private readonly ICountryRepository _countryRepository;

        private readonly ICountryQuery _countryQuery;
        private readonly IRegionQuery _regionQuery;

        public AddCountryCommandTest(
            ExampleContext context,
            ICountryQuery countryQuery,
            IRegionQuery regionQuery, ICountryRepository countryRepository) : base(context)
        {
            _context = context;

            _countryRepository = countryRepository;

            _countryQuery = countryQuery;
            _regionQuery = regionQuery;
        }

        [Fact]
        public async void Country_must_be_inserted()
        {
            await _context.Regions.AddAsync(new Regions(1, "Region Test"));
            await _context.SaveChangesAsync(CancellationToken.None);

            var countryId = "1";
            var countryName = "Country Test";
            var regionId = 1;

            var command = new AddCountryCommand(countryId, countryName, regionId);
            var handler = new AddCountryCommandHandler(_bus, _domainNotificationHandler, _countryRepository, _countryQuery, _regionQuery);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);

            var country = await _countryQuery.Get(countryId, CancellationToken.None);

            Assert.NotNull(country);
            Assert.Equal(countryId, country.CountryId);
            Assert.Equal(countryName, country.CountryName);
            Assert.Equal(regionId, country.Region?.RegionId);
        }
    }
}