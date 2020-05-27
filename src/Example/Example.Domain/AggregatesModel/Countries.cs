namespace ConsoleAppRE
{
    public class Countries
    {
        private Countries()
        {
        }

        public Countries(string countryId, string countryName, Regions region) : this()
        {
            CountryId = countryId;
            CountryName = countryName;
            Region = region;
        }

        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public decimal? RegionId { get; set; }

        public virtual Regions Region { get; set; }
    }
}