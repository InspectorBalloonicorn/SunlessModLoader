using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunlessModLoader.Classes.Classes
{
    public class AssignToSlot
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

        public AssignToSlot()
        {
            QualitiesWhichAllowSecondChanceOnThis = new List<Quality>();
            Enhancements = new List<Enhancement>();
        }

        public bool IsEquals(AssignToSlot? aTS)
        {
            bool matchFound;

            if (ReferenceEquals(aTS, null) && ReferenceEquals(this, null)) { return true; }
            //if one is null, and the other is not, return false immediately
            if (ReferenceEquals(aTS, null) && !ReferenceEquals(this, null)) { return false; }
            if (!ReferenceEquals(aTS, null) && ReferenceEquals(this, null)) { return false; }

            if (RelationshipCapable != aTS.RelationshipCapable) return false;
            if (OwnerName != aTS.OwnerName) return false;
            if (Description != aTS.Description) return false;
            if (Image != aTS.Image) return false;
            if (Notes != aTS.Notes) return false;
            if (Tag != aTS.Tag) return false;
            if (Cap != aTS.Cap) return false;
            if (HimbleLevel != aTS.HimbleLevel) return false;
            if (UsePyramidNumbers != aTS.UsePyramidNumbers) return false;
            if (PyramidNumberIncreaseLimit != aTS.PyramidNumberIncreaseLimit) return false;
            if (AvailableAt != aTS.AvailableAt) return false;
            if (PreventNaming != aTS.PreventNaming) return false;
            if (CssClasses != aTS.CssClasses) return false;
            if (World != aTS.World) return false;
            if (Ordering != aTS.Ordering) return false;
            if (IsSlot != aTS.IsSlot) return false;
            if (Persistent != aTS.Persistent) return false;
            if (Visible != aTS.Visible) return false;
            if (EnhancementsDescription != aTS.EnhancementsDescription) return false;
            if (AllowsSecondChancesOnChallengesForQuality != aTS.AllowsSecondChancesOnChallengesForQuality) return false;
            if (GivesTrophy != aTS.GivesTrophy) return false;
            if (DifficultyTestType != aTS.DifficultyTestType) return false;
            if (DifficultyScaler != aTS.DifficultyScaler) return false;
            if (AllowedOn != aTS.AllowedOn) return false;
            if (Nature != aTS.Nature) return false;
            if (Category != aTS.Category) return false;
            if (LevelDescriptionText != aTS.LevelDescriptionText) return false;
            if (ChangeDescriptionText != aTS.ChangeDescriptionText) return false;
            if (LevelImageText != aTS.LevelImageText) return false;
            if (Name != aTS.Name) return false;
            if (Id != aTS.Id) return false;

            //check LimitedToArea
            if (LimitedToArea == null && aTS.LimitedToArea == null) { /*Do Nothing*/ }
            else if (LimitedToArea == null && aTS.LimitedToArea != null) { return false; }
            else if (LimitedToArea != null && aTS.LimitedToArea == null) { return false; }
            else { if (!LimitedToArea.IsEquals(aTS.LimitedToArea)) { return false; } }

            //Check Use Event
            if (UseEvent == null && aTS.UseEvent == null) { /*Do Nothing*/ }
            else if (UseEvent == null && aTS.UseEvent != null) { return false; }
            else if (UseEvent != null && aTS.UseEvent == null) { return false; }
            else { if (!UseEvent.IsEquals(aTS.UseEvent)) { return false; } }

            //Check QualitiesWhichAllowSecondChanceOnThis
            if (QualitiesWhichAllowSecondChanceOnThis == null && aTS.QualitiesWhichAllowSecondChanceOnThis == null) { /*do nothing*/ }
            else if (QualitiesWhichAllowSecondChanceOnThis == null && aTS.QualitiesWhichAllowSecondChanceOnThis != null) { return false; }
            else if (QualitiesWhichAllowSecondChanceOnThis != null && aTS.QualitiesWhichAllowSecondChanceOnThis == null) { return false; }
            else
            {
                foreach (Quality quality in QualitiesWhichAllowSecondChanceOnThis)
                {
                    //check against the master list of qualities and confirm the quality matches in the list.
                    //If a quality is found that doesn't match exactly, the AssignToSlots are not equal.
                    matchFound = false;
                    foreach (Quality quality2 in aTS.QualitiesWhichAllowSecondChanceOnThis)
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
            if (Enhancements == null && aTS.Enhancements == null) { /*do nothing*/ }
            else if (Enhancements == null && aTS.Enhancements != null) { return false; }
            else if (Enhancements != null && aTS.Enhancements == null) { return false; }
            else
            {
                foreach (Enhancement enchn in Enhancements)
                {
                    //check against the master list of Enhancements and confirm the childbranch Enhancements are in the list.
                    //If an Enhancement is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (Enhancement enchan2 in aTS.Enhancements)
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
