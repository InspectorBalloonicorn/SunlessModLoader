using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunlessModLoader.Classes.Classes
{
    public class Area
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageName { get; set; }
        public bool? MarketAccessPermitted { get; set; }
        public string? MoveMessage { get; set; }
        public bool? HideName { get; set; }
        public bool? RandomPostcard { get; set; }
        public int? MapX { get; set; }
        public int? MapY { get; set; }
        public object? UnlocksWithQuality { get; set; }
        public bool? ShowOps { get; set; }
        public bool? PremiumSubRequired { get; set; }
        public int Id { get; set; }
        public bool IsEquals(Area? area)
        {
            if (ReferenceEquals(area, null) && ReferenceEquals(this, null)) { return true; }
            //if one is null, and the other is not, return false immediately
            if (ReferenceEquals(area, null) && !ReferenceEquals(this, null)) { return false; }
            if (!ReferenceEquals(area, null) && ReferenceEquals(this, null)) { return false; }

            if (Name != area.Name) return false;
            if (Description != area.Description) return false;
            if (ImageName != area.ImageName) return false;
            if (MarketAccessPermitted != area.MarketAccessPermitted) return false;
            if (MoveMessage != area.MoveMessage) return false;
            if (HideName != area.HideName) return false;
            if (RandomPostcard != area.RandomPostcard) return false;
            if (MapX != area.MapX) return false;
            if (MapY != area.MapY) return false;
            if (UnlocksWithQuality != area.UnlocksWithQuality) return false;
            if (ShowOps != area.ShowOps) return false;
            if (PremiumSubRequired != area.PremiumSubRequired) return false;
            if (Id != area.Id) return false;            

            return true;
        }
    }
}
