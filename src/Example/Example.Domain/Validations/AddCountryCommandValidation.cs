using ConsoleAppRE;
using Core.Domain.Validations;
using Example.Domain.Commands;
using Example.Domain.Interfaces.Queries;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Domain.Validations
{
    public class AddCountryCommandValidation : CommandHandlerValidation<AddCountryCommand, Countries>
    {
        private readonly ICountryQuery _countryQuery;
        private readonly IRegionQuery _regionQuery;

        public AddCountryCommandValidation(
            ICountryQuery countryQuery,
            IRegionQuery regionQuery)
        {
            _countryQuery = countryQuery;
            _regionQuery = regionQuery;

            CountryIdMustBeNotNull();
            CountryNameMustBeNotNull();

            CountryMustNotExists();
            RegionMustExists();
        }

        private void CountryIdMustBeNotNull()
        {
            RuleFor(x => x.CountryId)
                .NotEmpty()
                .WithMessage("Country Id can't be null.");
        }

        private void CountryNameMustBeNotNull()
        {
            RuleFor(x => x.CountryName)
                .NotEmpty()
                .WithMessage("Country Name can't be null.");
        }

        private void CountryMustNotExists()
        {
            RuleFor(x => x)
                .MustAsync(CountryMustNotExists)
                .WithMessage("Country already exists in the database.");
        }

        private async Task<bool> CountryMustNotExists(AddCountryCommand command, CancellationToken cancellationToken)
        {
            return !string.IsNullOrEmpty(command.CountryId) && !await _countryQuery.Exists(command.CountryId, cancellationToken);
        }

        private void RegionMustExists()
        {
            When(x => x.RegionId > 0, () =>
            {
                RuleFor(x => x)
                    .MustAsync(RegionMustNotExists)
                    .WithMessage("Region must exists in the database.");
            });
        }

        private async Task<bool> RegionMustNotExists(AddCountryCommand command, CancellationToken cancellationToken)
        {
            return command.RegionId > 0 && await _regionQuery.Exists(command.RegionId.Value, cancellationToken);
        }
    }
}