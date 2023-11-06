using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunlessModLoader.Classes.Classes
{
    public class Quality
    {
        public bool? RelationshipCapable { get; set; }
        public string? OwnerName { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Notes { get; set; }
        public string? Tag { get; set; }
        public int? Cap { get; set; }
        public int? HimbleLevel { get; set; }
        public bool? UsePyramidNumbers { get; set; }
        public int? PyramidNumberIncreaseLimit { get; set; }
        public string? AvailableAt { get; set; }
        public bool? PreventNaming { get; set; }
        public string? CssClasses { get; set; }
        public string? World { get; set; }
        public double? Ordering { get; set; }
        public bool? IsSlot { get; set; }
        public LimitedToArea? LimitedToArea { get; set; }
        public AssignToSlot? AssignToSlot { get; set; }
        public bool? Persistent { get; set; }
        public List<Quality>? QualitiesWhichAllowSecondChanceOnThis { get; set; }
        public bool? Visible { get; set; }
        public List<Enhancement>? Enhancements { get; set; }
        public string? EnhancementsDescription { get; set; }
        public bool? AllowsSecondChancesOnChallengesForQuality { get; set; }
        public object? GivesTrophy { get; set; }
        public UseEvent? UseEvent { get; set; }
        public string? DifficultyTestType { get; set; }
        public int? DifficultyScaler { get; set; }
        public string? AllowedOn { get; set; }
        public string? Nature { get; set; }
        public string? Category { get; set; }
        public string? LevelDescriptionText { get; set; }
        public string? ChangeDescriptionText { get; set; }
        public string? LevelImageText { get; set; }
        public string? Name { get; set; }
        public int Id { get; set; }

        public bool IsEquals(Quality qual)       
        {
            bool matchFound;
            if (ReferenceEquals(qual, null) && ReferenceEquals(this, null)) { return true; }
            //if one is null, and the other is not, return false immediately
            if (ReferenceEquals(qual, null) && !ReferenceEquals(this, null)) { return false; }
            if (!ReferenceEquals(qual, null) && ReferenceEquals(this, null)) { return false; }

            if (RelationshipCapable != qual.RelationshipCapable) return false;
            if (OwnerName != qual.OwnerName) return false;
            if (Description != qual.Description) return false;
            if (Image != qual.Image) return false;
            if (Notes != qual.Notes) return false;
            if (Tag != qual.Tag) return false;
            if (Cap != qual.Cap) return false;
            if (HimbleLevel != qual.HimbleLevel) return false;
            if (UsePyramidNumbers != qual.UsePyramidNumbers) return false;
            if (PyramidNumberIncreaseLimit != qual.PyramidNumberIncreaseLimit) return false;
            if (AvailableAt != qual.AvailableAt) return false;
            if (PreventNaming != qual.PreventNaming) return false;
            if (CssClasses != qual.CssClasses) return false;
            if (World != qual.World) return false;
            if (Ordering != qual.Ordering) return false;
            if (IsSlot != qual.IsSlot) return false;
            if (Persistent != qual.Persistent) return false;
            if (Visible != qual.Visible) return false;
            if (EnhancementsDescription != qual.EnhancementsDescription) return false;
            if (AllowsSecondChancesOnChallengesForQuality != qual.AllowsSecondChancesOnChallengesForQuality) return false;
            if (GivesTrophy != qual.GivesTrophy) return false;
            if (DifficultyTestType != qual.DifficultyTestType) return false;
            if (DifficultyScaler != qual.DifficultyScaler) return false;
            if (AllowedOn != qual.AllowedOn) return false;
            if (Category != qual.Category) return false;
            if (LevelDescriptionText != qual.LevelDescriptionText) return false;
            if (ChangeDescriptionText != qual.ChangeDescriptionText) return false;
            if (LevelImageText != qual.LevelImageText) return false;
            if (Name != qual.Name) return false;
            if (Id != qual.Id) return false;

            //Check LimitedToArea
            if (LimitedToArea == null && qual.LimitedToArea == null) { /*Do Nothing*/ }
            else if (LimitedToArea == null && qual.LimitedToArea != null) { return false; }
            else if (LimitedToArea != null && qual.LimitedToArea == null) { return false; }
            else { if (!LimitedToArea.IsEquals(qual.LimitedToArea)) { return false; } }

            //Check AssignToSlot
            if (AssignToSlot == null && qual.AssignToSlot == null) { /*Do Nothing*/ }
            else if (AssignToSlot == null && qual.AssignToSlot != null) { return false; }
            else if (AssignToSlot != null && qual.AssignToSlot == null) { return false; }
            else { if (!AssignToSlot.IsEquals(qual.AssignToSlot)) { return false; } }

            //Check Use Event
            if (UseEvent == null && qual.UseEvent == null) { /*Do Nothing*/ }
            else if (UseEvent == null && qual.UseEvent != null) { return false; }
            else if (UseEvent != null && qual.UseEvent == null) { return false; }
            else { if (!UseEvent.IsEquals(qual.UseEvent)) { return false; } }

            //Check QualitiesWhichAllowSecondChanceOnThis
            if (QualitiesWhichAllowSecondChanceOnThis == null && qual.QualitiesWhichAllowSecondChanceOnThis == null) { /*do nothing*/ }
            else if (QualitiesWhichAllowSecondChanceOnThis == null && qual.QualitiesWhichAllowSecondChanceOnThis != null) { return false; }
            else if (QualitiesWhichAllowSecondChanceOnThis != null && qual.QualitiesWhichAllowSecondChanceOnThis == null) { return false; }
            else
            {
                foreach (Quality quality in QualitiesWhichAllowSecondChanceOnThis)
                {
                    //check against the master list of qualities and confirm the quality matches in the list.
                    //If a quality is found that doesn't match exactly, the AssignToSlots are not equal.
                    matchFound = false;
                    foreach (Quality quality2 in qual.QualitiesWhichAllowSecondChanceOnThis)
                    {
                        if (quality.IsEquals(quality2))
                        {
                            matchFound = true;
                        };
                    }
                    if (matchFound == false) return false;
                }
            }

            
            //Check Enhancements
            if (Enhancements == null && qual.Enhancements == null) { /*do nothing*/ }
            else if (Enhancements == null && qual.Enhancements != null) { return false; }
            else if (Enhancements != null && qual.Enhancements == null) { return false; }
            else
            {
                foreach (Enhancement enchn in Enhancements)
                {
                    //check against the master list of Enhancements and confirm the childbranch Enhancements are in the list.
                    //If an Enhancement is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (Enhancement enchan2 in qual.Enhancements)
                    {
                        if (enchn.IsEquals(enchan2))
                        {
                            matchFound = true;
                        };
                    }
                    if (matchFound == false) return false;
                }
            }
            return true;
        }
    }
}
