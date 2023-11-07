using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunlessModLoader.Classes.Classes
{
    public class AssociatedQuality
    {
        public bool? RelationshipCapable { get; set; }
        public string? OwnerName { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Notes { get; set; }
        public string? Tag { get; set; }
        public string? Cap { get; set; }
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
        public bool? GivesTrophy { get; set; }
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

        public AssociatedQuality()
        {
            QualitiesWhichAllowSecondChanceOnThis = new List<Quality>();
            Enhancements = new List<Enhancement>();
        }

        public bool IsEquals(AssociatedQuality? aq)
        {
            bool matchFound;
            // if both are null, return true immediately
            if (ReferenceEquals(aq, null) && ReferenceEquals(this, null)) { return true; }
            //if one is null, and the other is not, return false immediately
            if (ReferenceEquals(aq, null) && !ReferenceEquals (this, null)) { return false; }
            if (!ReferenceEquals(aq, null) && ReferenceEquals(this, null)) { return false; }

            //if both objects exist, check each property to see if they match.
            if (RelationshipCapable != aq.RelationshipCapable) return false;
            if (OwnerName != aq.OwnerName) return false;
            if (Description != aq.Description) return false;
            if (Image != aq.Image) return false;
            if (Notes != aq.Notes) return false;
            if (Tag != aq.Tag) return false;
            if (Cap != aq.Cap) return false;
            if (HimbleLevel != aq.HimbleLevel) return false;
            if (UsePyramidNumbers != aq.UsePyramidNumbers) return false;
            if (PyramidNumberIncreaseLimit != aq.PyramidNumberIncreaseLimit) return false;
            if (AvailableAt != aq.AvailableAt) return false;
            if (PreventNaming != aq.PreventNaming) return false;
            if (CssClasses != aq.CssClasses) return false;
            if (World != aq.World) return false;
            if (Ordering != aq.Ordering) return false;
            if (IsSlot != aq.IsSlot) return false;
            if (Persistent != aq.Persistent) return false;
            if (Visible != aq.Visible) return false;
            if (EnhancementsDescription != aq.EnhancementsDescription) return false;
            if (AllowsSecondChancesOnChallengesForQuality != aq.AllowsSecondChancesOnChallengesForQuality) return false;
            if (GivesTrophy != aq.GivesTrophy) return false;
            if (UseEvent != aq.UseEvent) return false; // Objectify This
            if (DifficultyTestType != aq.DifficultyTestType) return false;
            if (DifficultyScaler != aq.DifficultyScaler) return false;
            if (AllowedOn != aq.AllowedOn) return false;
            if (Category != aq.Category) return false;
            if (LevelDescriptionText != aq.LevelDescriptionText) return false;
            if (ChangeDescriptionText != aq.ChangeDescriptionText) return false;
            if (LevelImageText != aq.LevelImageText) return false;
            if (Name != aq.Name) return false;
            if (Id != aq.Id) return false;

            //check LimitedToArea
            if (LimitedToArea == null && aq.LimitedToArea == null) { /*Do Nothing*/ }
            else if (LimitedToArea == null && aq.LimitedToArea != null) { return false; }
            else if (LimitedToArea != null && aq.LimitedToArea == null) { return false; }
            else { if (!LimitedToArea.IsEquals(aq.LimitedToArea)) { return false; } }

            //check AssignToSlot
            if (AssignToSlot == null && aq.AssignToSlot == null) { /*Do Nothing*/ }
            else if (AssignToSlot == null && aq.AssignToSlot != null) { return false; }
            else if (AssignToSlot != null && aq.AssignToSlot == null) { return false; }
            else { if (!AssignToSlot.IsEquals(aq.AssignToSlot)) { return false; } }

            //Check UseEvent
            if (UseEvent == null && aq.UseEvent == null) { /*Do Nothing*/ }
            else if (UseEvent == null && aq.UseEvent != null) { return false; }
            else if (UseEvent != null && aq.UseEvent == null) { return false; }
            else { if (!UseEvent.IsEquals(aq.UseEvent)) { return false; } }

            //Check QualitiesWhichAllowSecondChanceOnThis
            if (QualitiesWhichAllowSecondChanceOnThis == null && aq.QualitiesWhichAllowSecondChanceOnThis == null) { /*do nothing*/ }
            else if (QualitiesWhichAllowSecondChanceOnThis == null && aq.QualitiesWhichAllowSecondChanceOnThis != null) { return false; }
            else if (QualitiesWhichAllowSecondChanceOnThis != null && aq.QualitiesWhichAllowSecondChanceOnThis == null) { return false; }
            else
            {
                foreach (Quality quality in QualitiesWhichAllowSecondChanceOnThis)
                {
                    //check against the master list of qualities and confirm the quality matches in the list.
                    //If a quality is found that doesn't match exactly, the AssignToSlots are not equal.
                    matchFound = false;
                    foreach (Quality quality2 in aq.QualitiesWhichAllowSecondChanceOnThis)
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
            if (Enhancements == null && aq.Enhancements == null) { /*do nothing*/ }
            else if (Enhancements == null && aq.Enhancements != null) { return false; }
            else if (Enhancements != null && aq.Enhancements == null) { return false; }
            else
            {
                foreach (Enhancement enchn in Enhancements)
                {
                    //check against the master list of Enhancements and confirm the childbranch Enhancements are in the list.
                    //If an Enhancement is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (Enhancement enchan2 in aq.Enhancements)
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
