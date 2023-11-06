using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunlessModLoader.Classes.Classes
{
    public class Enhancement
    {
        public int? Level { get; set; }
        public AssociatedQuality? AssociatedQuality { get; set; }
        public int? AssociatedQualityId { get; set; }
        public string? QualityName { get; set; }
        public string? QualityDescription { get; set; }
        public string? QualityImage { get; set; }
        public string? QualityNature { get; set; }
        public string? QualityCategory { get; set; }
        public string? QualityAllowedOn { get; set; }
        public int Id { get; set; }

        public bool IsEquals(Enhancement? enh)
        {
            if (ReferenceEquals(enh, null) && ReferenceEquals(this, null)) { return true; }
            //if one is null, and the other is not, return false immediately
            if (ReferenceEquals(enh, null) && !ReferenceEquals(this, null)) { return false; }
            if (!ReferenceEquals(enh, null) && ReferenceEquals(this, null)) { return false; }

            return true;
        }
    }
}
