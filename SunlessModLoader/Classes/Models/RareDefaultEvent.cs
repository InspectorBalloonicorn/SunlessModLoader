using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunlessModLoader.Classes.Classes
{
    public class RareDefaultEvent
    {
        public List<ChildBranches>? ChildBranches { get; set; }
        public List<QualitiesAffected>? QualitiesAffected { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ExoticEffects { get; set; }
        public int Id { get; set; }
        public LinkToEvent? LinkToEvent { get; set; }
        public string? Category { get; set; }
        public SwitchToSetting? SwitchToSetting { get; set; }
        public int? SwitchToSettingId { get; set; }
        public string? Urgency { get; set; }

        public bool IsEquals(RareDefaultEvent rareDefEvent)
        {
            if (ReferenceEquals(rareDefEvent, null) && ReferenceEquals(this, null)) { return true; }
            //if one is null, and the other is not, return false immediately
            if (ReferenceEquals(rareDefEvent, null) && !ReferenceEquals(this, null)) { return false; }
            if (!ReferenceEquals(rareDefEvent, null) && ReferenceEquals(this, null)) { return false; }


            return true;
        }
    }
}
