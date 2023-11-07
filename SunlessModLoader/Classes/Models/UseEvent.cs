using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunlessModLoader.Classes.Classes
{
    public class UseEvent
    {
        public List<ChildBranches>? ChildBranches { get; set; }
        public string? ParentBranch { get; set; }
        public List<QualitiesAffected>? QualitiesAffected { get; set; }
        public List<QualitiesRequired>? QualitiesRequired { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public string? Tag { get; set; }
        public string? ExoticEffects { get; set; }
        public string? Note { get; set; }
        public int ChallengeLevel { get; set; }
        public string? UnclearedEditAt { get; set; }
        public string? LastEditedBy { get; set; }
        public double? Ordering { get; set; }
        public bool? ShowAsMessage { get; set; }
        public string? LivingStory { get; set; }
        public LinkToEvent? LinkToEvent { get; set; }
        public Deck? Deck { get; set; }
        public string? Category { get; set; }
        public LimitedToArea? LimitedToArea { get; set; }
        public string? World { get; set; }
        public string? Transient { get; set; }
        public int? Stickiness { get; set; }
        public int? MoveToAreaId { get; set; }
        public MoveToArea? MoveToArea { get; set; }
        public string? MoveToDomicile { get; set; }
        public SwitchToSetting? SwitchToSetting { get; set; }
        public int? FatePointsChange { get; set; }
        public int? BootyValue { get; set; }
        public string? LogInJournalAgainstQuality { get; set; }
        public Setting? Setting { get; set; }
        public string? Urgency { get; set; }
        public string? Teaser { get; set; }
        public string? OwnerName { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public int? Distribution { get; set; }
        public bool? Autofire { get; set; }
        public bool? CanGoBack { get; set; }
        public string? Name { get; set; }
        public int Id { get; set; }

        public bool IsEquals(UseEvent? useEvent)
        {
            bool matchFound = false;

            if (ReferenceEquals(useEvent, null) && ReferenceEquals(this, null)) { return true; }
            //if one is null, and the other is not, return false immediately
            if (ReferenceEquals(useEvent, null) && !ReferenceEquals(this, null)) { return false; }
            if (!ReferenceEquals(useEvent, null) && ReferenceEquals(this, null)) { return false; }

            if (Autofire != useEvent.Autofire) { return false; }
            if (CanGoBack != useEvent.CanGoBack) { return false; }
            if (Category != useEvent.Category) { return false; }
            if (Description != useEvent.Description) { return false; }
            if (Distribution != useEvent.Distribution) { return false; }
            if (ExoticEffects != useEvent.ExoticEffects) { return false; }
            if (Image != useEvent.Image) { return false; }
            if (ChallengeLevel != useEvent.ChallengeLevel) { return false; }
            if (Name != useEvent.Name) { return false; }
            if (Ordering != useEvent.Ordering) { return false; }
            if (ShowAsMessage != useEvent.ShowAsMessage) { return false; }
            if (Stickiness != useEvent.Stickiness) { return false; }
            if (Transient != useEvent.Transient) { return false; }
            if (Urgency != useEvent.Urgency) { return false; }
            if (Note != useEvent.Note) { return false; }
            if (UnclearedEditAt != useEvent.UnclearedEditAt) { return false; }
            if (LastEditedBy != useEvent.LastEditedBy) { return false; }
            if (LivingStory != useEvent.LivingStory) { return false; }
            if (World != useEvent.World) { return false; }
            if (MoveToAreaId != useEvent.MoveToAreaId) { return false; }
            if (MoveToDomicile != useEvent.MoveToDomicile) { return false; }
            if (FatePointsChange != useEvent.FatePointsChange) { return false; }
            if (BootyValue != useEvent.BootyValue) { return false; }
            if (LogInJournalAgainstQuality != useEvent.LogInJournalAgainstQuality) { return false; }
            if (Teaser != useEvent.Teaser) { return false; }
            if (OwnerName != useEvent.OwnerName) { return false; }
            if (DateTimeCreated != useEvent.DateTimeCreated) { return false; }
            if (ParentBranch != useEvent.ParentBranch) { return false; }
            if (ParentBranch != useEvent.ParentBranch) { return false; }
            if (ParentBranch != useEvent.ParentBranch) { return false; }           

            //check Deck
            if (Deck == null && useEvent.Deck == null) { /*Do Nothing*/ }
            else if (Deck == null && useEvent.Deck != null) { return false; }
            else if (Deck != null && useEvent.Deck == null) { return false; }
            else { if (!Deck.IsEquals(useEvent.Deck)) { return false; } }

            //check LimitedToArea
            if (LimitedToArea == null && useEvent.LimitedToArea == null) { /*Do Nothing*/ }
            else if (LimitedToArea == null && useEvent.LimitedToArea != null) { return false; }
            else if (LimitedToArea != null && useEvent.LimitedToArea == null) { return false; }
            else { if (!LimitedToArea.IsEquals(useEvent.LimitedToArea)) { return false; } }

            //Check Setting
            if (Setting == null && useEvent.Setting == null) { /*Do Nothing*/ }
            else if (Setting == null && useEvent.Setting != null) { return false; }
            else if (Setting != null && useEvent.Setting == null) { return false; }
            else { if (!Setting.IsEquals(useEvent.Setting)) { return false; } }

            //check LinkToEvent
            if (LinkToEvent == null && useEvent.LinkToEvent == null) { /*Do Nothing*/ }
            else if (LinkToEvent == null && useEvent.LinkToEvent != null) { return false; }
            else if (LinkToEvent != null && useEvent.LinkToEvent == null) { return false; }
            else { if (!LinkToEvent.IsEquals(useEvent.LinkToEvent)) { return false; } }

            //check MoveToArea
            if (MoveToArea == null && useEvent.MoveToArea == null) { /*Do Nothing*/ }
            else if (MoveToArea == null && useEvent.MoveToArea != null) { return false; }
            else if (MoveToArea != null && useEvent.MoveToArea == null) { return false; }
            else { if (!MoveToArea.IsEquals(useEvent.MoveToArea)) { return false; } }

            //check SwitchToSetting
            if (SwitchToSetting == null && useEvent.SwitchToSetting == null) { /*Do Nothing*/ }
            else if (SwitchToSetting == null && useEvent.SwitchToSetting != null) { return false; }
            else if (SwitchToSetting != null && useEvent.SwitchToSetting == null) { return false; }
            else { if (!SwitchToSetting.IsEquals(useEvent.SwitchToSetting)) { return false; } }

            //Check QualitiesAffected
            if (QualitiesAffected == null && useEvent.QualitiesAffected == null) { /*do nothing*/ }
            else if (QualitiesAffected == null && useEvent.QualitiesAffected != null) { return false; }
            else if (QualitiesAffected != null && useEvent.QualitiesAffected == null) { return false; }
            else
            {
                foreach (QualitiesAffected qa in QualitiesAffected)
                {
                    //check against the master list of child branches and confirm the childbranch matches in the list.
                    //If a child object is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (QualitiesAffected qa2 in useEvent.QualitiesAffected)
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

            //Check QualitiesRequired
            if (QualitiesRequired == null && useEvent.QualitiesRequired == null) { return true; }
            else if (QualitiesRequired == null && useEvent.QualitiesRequired != null) { return false; }
            else if (QualitiesRequired != null && useEvent.QualitiesRequired == null) { return false; }
            else
            {
                foreach (QualitiesRequired qr in QualitiesRequired)
                {
                    //check against the master list of child branches and confirm the childbranch matches in the list.
                    //If a child object is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (QualitiesRequired qr2 in useEvent.QualitiesRequired)
                    {
                        if (qr.IsEquals(qr2))
                        {
                            matchFound = true;
                            break;
                        };
                    }
                    if (matchFound == false) return false;
                }
            }

            //Check ChildBranches
            if (ChildBranches == null && useEvent.ChildBranches == null) { /*Do nothing*/ }
            else if (ChildBranches == null && useEvent.ChildBranches != null) { return false; }
            else if (ChildBranches != null && useEvent.ChildBranches == null) { return false; }
            else
            {
                //For each child branch required from this addon event
                foreach (ChildBranches cb in ChildBranches)
                {
                    //check against the master list of child branches and confirm the childbranch matches in the list.
                    //If a child object is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (ChildBranches cb2 in useEvent.ChildBranches)
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

            return true;
        }
    }
}
