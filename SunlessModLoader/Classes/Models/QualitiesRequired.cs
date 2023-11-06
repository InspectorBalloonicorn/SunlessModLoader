using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunlessModLoader.Classes.Classes
{
    public class QualitiesRequired
    {
        public int? MinLevel { get; set; }
        public int? MaxLevel { get; set; }
        public AssociatedQuality? AssociatedQuality { get; set; }
        public int Id { get; set; }
        public int? DifficultyLevel { get; set; }
        public bool? VisibleWhenRequirementFailed { get; set; }
        public bool? BranchVisibleWhenRequirementFailed { get; set; }
        public string? DifficultyAdvanced { get; set; }
        public string? MaxAdvanced { get; set; }
        public string? MinAdvanced { get; set; }

        public bool IsEquals(QualitiesRequired qr)
        {
            if (ReferenceEquals(qr, null) && ReferenceEquals(this, null)) { return true; }
            //if one is null, and the other is not, return false immediately
            if (ReferenceEquals(qr, null) && !ReferenceEquals(this, null)) { return false; }
            if (!ReferenceEquals(qr, null) && ReferenceEquals(this, null)) { return false; }


            if (MinLevel != qr.MinLevel) { return false; }
            if(MaxLevel != qr.MaxLevel) {  return false; }

            //Check AssociatedQuality
            if (AssociatedQuality == null && qr.AssociatedQuality == null) { /*Do Nothing*/ }
            else if (AssociatedQuality == null && qr.AssociatedQuality != null) { return false; }
            else if (AssociatedQuality != null && qr.AssociatedQuality == null) { return false; }
            else { if (!AssociatedQuality.IsEquals(qr.AssociatedQuality)) { return false; } }

            if(Id != qr.Id) {  return false; }
            if(DifficultyLevel != qr.DifficultyLevel) { return false; }
            if(VisibleWhenRequirementFailed != qr.VisibleWhenRequirementFailed) {  return false; }
            if(BranchVisibleWhenRequirementFailed!= qr.BranchVisibleWhenRequirementFailed) { return false; }
            if (DifficultyAdvanced != qr.DifficultyAdvanced) {  return false; }
            if(MaxAdvanced != qr.MaxAdvanced ) { return false; }
            if(MinAdvanced != qr.MinAdvanced) { return false;  }


            return true;
        }
    }
}
