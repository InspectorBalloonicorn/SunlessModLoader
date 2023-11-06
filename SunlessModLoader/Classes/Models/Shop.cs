using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunlessModLoader.Classes.Classes
{
    public class Shop
    {
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public double? Ordering { get; set; }
        public List<Availability>? Availabilities { get; set; }
        public List<QualitiesRequired>? QualitiesRequired { get; set; }
        public int Id { get; set; }

        public bool IsEquals(Shop shop)
        {
            if (ReferenceEquals(shop, null) && ReferenceEquals(this, null)) { return true; }
            //if one is null, and the other is not, return false immediately
            if (ReferenceEquals(shop, null) && !ReferenceEquals(this, null)) { return false; }
            if (!ReferenceEquals(shop, null) && ReferenceEquals(this, null)) { return false; }


            return true;
        }
    }

}
