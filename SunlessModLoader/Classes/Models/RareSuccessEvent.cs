using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunlessModLoader.Classes.Classes
{
    public class RareSuccessEvent
    {
        public List<ChildBranches>? ChildBranches { get; set; }
        public List<QualitiesAffected>? QualitiesAffected { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ExoticEffects { get; set; }
        public int Id { get; set; }
        public LinkToEvent? LinkToEvent { get; set; }
        public string? Category { get; set; }
        public string? Urgency { get; set; }

        public bool IsEquals(RareSuccessEvent rareSuccEvnt)
        {
            bool matchFound;
            if (ReferenceEquals(rareSuccEvnt, null) && ReferenceEquals(this, null)) { return true; }
            //if one is null, and the other is not, return false immediately
            if (ReferenceEquals(rareSuccEvnt, null) && !ReferenceEquals(this, null)) { return false; }
            if (!ReferenceEquals(rareSuccEvnt, null) && ReferenceEquals(this, null)) { return false; }

            if (Name != rareSuccEvnt.Name) return false;
            if (Description != rareSuccEvnt.Description) return false;
            if (ExoticEffects != rareSuccEvnt?.ExoticEffects) return false;
            if (Id != rareSuccEvnt?.Id) return false;
            if (Category != rareSuccEvnt?.Category) return false;
            if (Urgency != rareSuccEvnt?.Urgency) return false;

            //check LinkToEvent
            if (LinkToEvent == null && rareSuccEvnt.LinkToEvent == null) { /*Do Nothing*/ }
            else if (LinkToEvent == null && rareSuccEvnt.LinkToEvent != null) { return false; }
            else if (LinkToEvent != null && rareSuccEvnt.LinkToEvent == null) { return false; }
            else { if (!LinkToEvent.IsEquals(rareSuccEvnt.LinkToEvent)) { return false; } }

            //Check QualitiesAffected
            if (QualitiesAffected == null && rareSuccEvnt.QualitiesAffected == null) { /*do nothing*/ }
            else if (QualitiesAffected == null && rareSuccEvnt.QualitiesAffected != null) { return false; }
            else if (QualitiesAffected != null && rareSuccEvnt.QualitiesAffected == null) { return false; }
            else
            {
                foreach (QualitiesAffected qa in QualitiesAffected)
                {
                    //check against the master list of child branches and confirm the childbranch matches in the list.
                    //If a child object is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (QualitiesAffected qa2 in rareSuccEvnt.QualitiesAffected)
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
            if (ChildBranches == null && rareSuccEvnt.ChildBranches == null) { /*Do nothing*/ }
            else if (ChildBranches == null && rareSuccEvnt.ChildBranches != null) { return false; }
            else if (ChildBranches != null && rareSuccEvnt.ChildBranches == null) { return false; }
            else
            {
                //For each child branch required from this addon event
                foreach (ChildBranches cb in ChildBranches)
                {
                    //check against the master list of child branches and confirm the childbranch matches in the list.
                    //If a child object is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (ChildBranches cb2 in rareSuccEvnt.ChildBranches)
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
