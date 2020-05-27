using ConsoleAppRE;
using Core.Domain.Commands;

namespace Example.Domain.Commands
{
    public class AddCountryCommand : Command<Countries>
    {
        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public decimal? RegionId { get; set; }

        public AddCountryCommand(string countryId, string countryName, decimal? regionId)
        {
            CountryId = countryId;
            CountryName = countryName;
            RegionId = regionId;
        }
    }
}