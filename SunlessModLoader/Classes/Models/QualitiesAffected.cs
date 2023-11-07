using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunlessModLoader.Classes.Classes
{
    public class QualitiesAffected
    {
        public int? Level { get; set; }
        public AssociatedQuality? AssociatedQuality { get; set; }
        public int Id { get; set; }
        public int? Priority { get; set; }
        public int? SetToExactly { get; set; }
        public string? SetToExactlyAdvanced { get; set; }
        public bool? ForceEquip { get; set; }
        public int? OnlyIfNoMoreThan { get; set; }
        public string? ChangeByAdvanced { get; set; }
        public int? OnlyIfAtLeast { get; set; }

        public bool IsEquals(QualitiesAffected qa)
        {
            if (ReferenceEquals(qa, null) && ReferenceEquals(this, null)) { return true; }
            //if one is null, and the other is not, return false immediately
            if (ReferenceEquals(qa, null) && !ReferenceEquals(this, null)) { return false; }
            if (!ReferenceEquals(qa, null) && ReferenceEquals(this, null)) { return false; }

            if (Level != qa.Level) return false;

            if(AssociatedQuality != qa.AssociatedQuality) return false; //Objectify

            if(Id != qa.Id) return false;
            if(Priority != qa.Priority) return false;
            if(SetToExactly != qa.SetToExactly) return false;
            if(ForceEquip != qa.ForceEquip) return false;
            if(OnlyIfNoMoreThan != qa.OnlyIfNoMoreThan) return false;
            if(ChangeByAdvanced != qa.ChangeByAdvanced) return false;
            if(OnlyIfAtLeast != qa.OnlyIfAtLeast) return false;


            return true;
        }
    }
}

