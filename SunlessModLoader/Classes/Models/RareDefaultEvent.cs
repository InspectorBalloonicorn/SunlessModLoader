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
            bool matchFound;
            if (ReferenceEquals(rareDefEvent, null) && ReferenceEquals(this, null)) { return true; }
            //if one is null, and the other is not, return false immediately
            if (ReferenceEquals(rareDefEvent, null) && !ReferenceEquals(this, null)) { return false; }
            if (!ReferenceEquals(rareDefEvent, null) && ReferenceEquals(this, null)) { return false; }

            if (Name != rareDefEvent.Name) return false;
            if (Description != rareDefEvent.Description) return false;
            if (ExoticEffects != rareDefEvent?.ExoticEffects) return false;
            if (Id != rareDefEvent?.Id) return false;
            if (Category != rareDefEvent?.Category) return false;
            if (Urgency != rareDefEvent?.Urgency) return false;
            if (SwitchToSettingId != rareDefEvent.SwitchToSettingId) return false;

            //check LinkToEvent
            if (LinkToEvent == null && rareDefEvent.LinkToEvent == null) { /*Do Nothing*/ }
            else if (LinkToEvent == null && rareDefEvent.LinkToEvent != null) { return false; }
            else if (LinkToEvent != null && rareDefEvent.LinkToEvent == null) { return false; }
            else { if (!LinkToEvent.IsEquals(rareDefEvent.LinkToEvent)) { return false; } }

            //check SwitchToSetting
            if (SwitchToSetting == null && rareDefEvent.SwitchToSetting == null) { /*Do Nothing*/ }
            else if (SwitchToSetting == null && rareDefEvent.SwitchToSetting != null) { return false; }
            else if (SwitchToSetting != null && rareDefEvent.SwitchToSetting == null) { return false; }
            else { if (!SwitchToSetting.IsEquals(rareDefEvent.SwitchToSetting)) { return false; } }

            //Check QualitiesAffected
            if (QualitiesAffected == null && rareDefEvent.QualitiesAffected == null) { /*do nothing*/ }
            else if (QualitiesAffected == null && rareDefEvent.QualitiesAffected != null) { return false; }
            else if (QualitiesAffected != null && rareDefEvent.QualitiesAffected == null) { return false; }
            else
            {
                foreach (QualitiesAffected qa in QualitiesAffected)
                {
                    //check against the master list of child branches and confirm the childbranch matches in the list.
                    //If a child object is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (QualitiesAffected qa2 in rareDefEvent.QualitiesAffected)
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

            //Check ChildBranches
            if (ChildBranches == null && rareDefEvent.ChildBranches == null) { /*Do nothing*/ }
            else if (ChildBranches == null && rareDefEvent.ChildBranches != null) { return false; }
            else if (ChildBranches != null && rareDefEvent.ChildBranches == null) { return false; }
            else
            {
                //For each child branch required from this addon event
                foreach (ChildBranches cb in ChildBranches)
                {
                    //check against the master list of child branches and confirm the childbranch matches in the list.
                    //If a child object is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (ChildBranches cb2 in rareDefEvent.ChildBranches)
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
