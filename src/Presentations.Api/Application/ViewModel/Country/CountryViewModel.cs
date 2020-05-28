namespace Presentations.Api.Application.ViewModel.Country
{
    public class CountryViewModel
    {
        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public decimal? RegionId { get; set; }

        public CountryViewModel(string countryId, string countryName, decimal? regionId)
        {
            CountryId = countryId;
            CountryName = countryName;
            RegionId = regionId;
        }
    }
}