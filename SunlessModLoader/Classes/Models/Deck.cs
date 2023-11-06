using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunlessModLoader.Classes.Classes
{
    public class Deck
    {
        public string? Name { get; set; }
        public string? ImageName { get; set; }
        public string? Description { get; set; }
        public int Id { get; set; }
        public double? Ordering { get; set; }
        public string? Availability { get; set; }
        public int? DrawSize { get; set; }
        public int? MaxCards { get; set; }
        public bool IsEquals(Deck deck)
        {
            if (ReferenceEquals(deck, null) && ReferenceEquals(this, null)) { return true; }
            //if one is null, and the other is not, return false immediately
            if (ReferenceEquals(deck, null) && !ReferenceEquals(this, null)) { return false; }
            if (!ReferenceEquals(deck, null) && ReferenceEquals(this, null)) { return false; }

            if (Name != deck.Name) return false;
            if (ImageName != deck.ImageName) return false;
            if (Description != deck.Description) return false;
            if (Id != deck.Id) return false;
            if (Ordering != deck.Ordering) return false;
            if (Availability != deck.Availability) return false;
            if (DrawSize != deck.DrawSize) return false;
            if (MaxCards != deck.MaxCards) return false;

            return true;
        }
    }
}
