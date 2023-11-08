using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunlessModLoader.Classes.Classes
{
    public class DefaultEvent
    {
        public List<QualitiesAffected>? QualitiesAffected { get; set; }
        public List<QualitiesRequired>? QualitiesRequired { get; set; }
        public List<ChildBranches>? ChildBranches { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public double? Ordering { get; set; }
        public Deck? Deck { get; set; }
        public bool? CanGoBack { get; set; }
        public int? Distribution { get; set; }
        public string? Urgency { get; set; }
        public int Id { get; set; }
        public int? Stickiness { get; set; }
        public string? Category { get; set; }
        public string? ExoticEffects { get; set; }
        public bool? Autofire { get; set; }
        public int? ChallengeLevel { get; set; }
        public Setting? Setting { get; set; }
        public bool? Transient { get; set; }
        public LimitedToArea? LimitedToArea { get; set; }
        public bool? ShowAsMessage { get; set; }
        public string? UnclearedEditAt { get; set; }
        public string? LastEditBy { get; set; }
        public string? LivingStory { get; set; }
        public string? World { get; set; }
        public string? MoveToAreaId { get; set; }
        public MoveToArea? MoveToArea { get; set; }
        public string? MoveToDomicile { get; set; }
        public SwitchToSetting? SwitchToSetting { get; set; }
        public int? FatePointsChange { get; set; }
        public int? BootyValue { get; set; }
        public string? LogInJournalAgainstQuality { get; set; }
        public string? OwnerName { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public bool AutoFire { get; set; }
        public LinkToEvent LinkToEvent { get; set; }
        public string SwitchToSettingId { get; set; }

        public bool IsEquals(DefaultEvent? defEvent)
        {
            bool matchFound;

            if (ReferenceEquals(defEvent, null) && ReferenceEquals(this, null)) { return true; }
            //if one is null, and the other is not, return false immediately
            if (ReferenceEquals(defEvent, null) && !ReferenceEquals(this, null)) { return false; }
            if (!ReferenceEquals(defEvent, null) && ReferenceEquals(this, null)) { return false; }

            if (Autofire != defEvent.Autofire) { return false; }
            if (CanGoBack != defEvent.CanGoBack) { return false; }
            if (Category != defEvent.Category) { return false; }
            if (ChallengeLevel != defEvent.ChallengeLevel) { return false; }
            if (Description != defEvent.Description) { return false; }
            if (Distribution != defEvent.Distribution) { return false; }
            if (ExoticEffects != defEvent.ExoticEffects) { return false; }
            if (Image != defEvent.Image) { return false; }
            if (Name != defEvent.Name) { return false; }
            if (Ordering != defEvent.Ordering) { return false; }
            if (ShowAsMessage != defEvent.ShowAsMessage) { return false; }
            if (Stickiness != defEvent.Stickiness) { return false; }
            if (Transient != defEvent.Transient) { return false; }
            if (Urgency != defEvent.Urgency) { return false; }
            if (UnclearedEditAt != defEvent.UnclearedEditAt) { return false; }
            if (LastEditBy != defEvent.LastEditBy) { return false; }
            if (LivingStory != defEvent.LivingStory) { return false; }
            if (World != defEvent.World) { return false; }
            if (MoveToAreaId != defEvent.MoveToAreaId) { return false; }
            if (MoveToDomicile != defEvent.MoveToDomicile) { return false; }
            if (FatePointsChange != defEvent.FatePointsChange) { return false; }
            if (BootyValue != defEvent.BootyValue) { return false; }
            if (LogInJournalAgainstQuality != defEvent.LogInJournalAgainstQuality) { return false; }
            if (OwnerName != defEvent.OwnerName) { return false; }
            if (AutoFire != defEvent.AutoFire) { return false; }


            //check LimitedToArea
            if (LimitedToArea == null && defEvent.LimitedToArea == null) { /*Do Nothing*/ }
            else if (LimitedToArea == null && defEvent.LimitedToArea != null) { return false; }
            else if (LimitedToArea != null && defEvent.LimitedToArea == null) { return false; }
            else { if (!LimitedToArea.IsEquals(defEvent.LimitedToArea)) { return false; } }

            //Check LinkToEvent
            if (LinkToEvent == null && defEvent.LinkToEvent == null) { /*Do Nothing*/ }
            else if (LinkToEvent == null && defEvent.LinkToEvent != null) { return false; }
            else if (LinkToEvent != null && defEvent.LinkToEvent == null) { return false; }
            else { if (!LinkToEvent.IsEquals(defEvent.LinkToEvent)) { return false; } }

            //Check MoveToArea
            if (MoveToArea == null && defEvent.MoveToArea == null) { /*Do Nothing*/ }
            else if (MoveToArea == null && defEvent.MoveToArea != null) { return false; }
            else if (MoveToArea != null && defEvent.MoveToArea == null) { return false; }
            else { if (!MoveToArea.IsEquals(defEvent.MoveToArea)) { return false; } }

            //Check SwitchToSetting
            if (SwitchToSetting == null && defEvent.SwitchToSetting == null) { /*Do Nothing*/ }
            else if (SwitchToSetting == null && defEvent.SwitchToSetting != null) { return false; }
            else if (SwitchToSetting != null && defEvent.SwitchToSetting == null) { return false; }
            else { if (!SwitchToSetting.IsEquals(defEvent.SwitchToSetting)) { return false; } }

            //check DateTimeCreated
            if (DateTimeCreated == null && defEvent.DateTimeCreated == null) { /*Do Nothing*/ }
            else if (DateTimeCreated == null && defEvent.DateTimeCreated != null) { return false; }
            else if (DateTimeCreated != null && defEvent.DateTimeCreated == null) { return false; }
            else { if (!DateTimeCreated.Equals(defEvent.DateTimeCreated)) { return false; } }

            //Check ChildBranches
            if (ChildBranches == null && defEvent.ChildBranches == null) { /*Do nothing*/ }
            else if (ChildBranches == null && defEvent.ChildBranches != null) { return false; }
            else if (ChildBranches != null && defEvent.ChildBranches == null) { return false; }
            else
            {
                //For each child branch required from this addon event
                foreach (ChildBranches cb in ChildBranches)
                {
                    //check against the master list of child branches and confirm the childbranch matches in the list.
                    //If a child object is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (ChildBranches cb2 in defEvent.ChildBranches)
                    {
                        if (cb.IsEquals(cb2))
                        {
                            matchFound = true;
                            break;
                        };
                    }
                    if (matchFound == false) return false;
                }
            }

            //Check QualitiesAffected
            if (QualitiesAffected == null && defEvent.QualitiesAffected == null) { /*do nothing*/ }
            else if (QualitiesAffected == null && defEvent.QualitiesAffected != null) { return false; }
            else if (QualitiesAffected != null && defEvent.QualitiesAffected == null) { return false; }
            else
            {
                foreach (QualitiesAffected qa in QualitiesAffected)
                {
                    //check against the master list of child branches and confirm the childbranch matches in the list.
                    //If a child object is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (QualitiesAffected qa2 in defEvent.QualitiesAffected)
                    {
                        if (qa.IsEquals(qa2))
                        {
                            matchFound = true;
                            break;
                        };
                    }
                    if (matchFound == false) return false;
                }
            }

            return true;
        }
    }
}
