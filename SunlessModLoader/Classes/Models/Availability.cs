using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunlessModLoader.Classes.Classes
{
    public class Availability
    {
        public Quality? Quality { get; set; }
        public int? Cost { get; set; }
        public int? SellPrice { get; set; }
        public PurchaseQuality? PurchaseQuality { get; set; }
        public int Id { get; set; }
        public string? BuyMessage { get; set; }
        public string? SellMessage { get; set; }

        public bool IsEquals(Availability? avail)
        {
            bool matchFound;

            if (ReferenceEquals(avail, null) && ReferenceEquals(this, null)) { return true; }
            //if one is null, and the other is not, return false immediately
            if (ReferenceEquals(avail, null) && !ReferenceEquals(this, null)) { return false; }
            if (!ReferenceEquals(avail, null) && ReferenceEquals(this, null)) { return false; }

            return true;
        }
    }
}
