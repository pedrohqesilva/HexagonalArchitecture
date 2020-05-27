using System.Collections.Generic;

namespace ConsoleAppRE
{
    public class Regions
    {
        public Regions()
        {
            Countries = new HashSet<Countries>();
        }

        public Regions(decimal regionId, string regionName) : this()
        {
            RegionId = regionId;
            RegionName = regionName;
        }

        public decimal RegionId { get; set; }
        public string RegionName { get; set; }

        public virtual ICollection<Countries> Countries { get; set; }
    }
}