using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunlessModLoader.Classes.Classes
{
    public class SuccessEvent
    {
        public List<ChildBranches>? ChildBranches { get; set; }
        public List<QualitiesAffected>? QualitiesAffected { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ExoticEffects { get; set; }
        public int Id { get; set; }
        public string? Category { get; set; }
        public LinkToEvent? LinkToEvent { get; set; }
        public string? Urgency { get; set; }
        public MoveToArea? MoveToArea { get; set; }

        public bool IsEquals(SuccessEvent successEvent)
        {
            bool matchFound;
            if (ReferenceEquals(successEvent, null) && ReferenceEquals(this, null)) { return true; }
            //if one is null, and the other is not, return false immediately
            if (ReferenceEquals(successEvent, null) && !ReferenceEquals(this, null)) { return false; }
            if (!ReferenceEquals(successEvent, null) && ReferenceEquals(this, null)) { return false; }

            if (Name != successEvent.Name) return false;
            if (Description != successEvent.Description) return false;
            if (ExoticEffects != successEvent?.ExoticEffects) return false;
            if (Id != successEvent?.Id) return false;
            if (Category != successEvent?.Category) return false;
            if (Urgency != successEvent?.Urgency) return false;

            //check LinkToEvent
            if (LinkToEvent == null && successEvent.LinkToEvent == null) { /*Do Nothing*/ }
            else if (LinkToEvent == null && successEvent.LinkToEvent != null) { return false; }
            else if (LinkToEvent != null && successEvent.LinkToEvent == null) { return false; }
            else { if (!LinkToEvent.IsEquals(successEvent.LinkToEvent)) { return false; } }

            //check MoveToArea
            if (MoveToArea == null && successEvent.MoveToArea == null) { /*Do Nothing*/ }
            else if (MoveToArea == null && successEvent.MoveToArea != null) { return false; }
            else if (MoveToArea != null && successEvent.MoveToArea == null) { return false; }
            else { if (!MoveToArea.IsEquals(successEvent.MoveToArea)) { return false; } }

            //Check QualitiesAffected
            if (QualitiesAffected == null && successEvent.QualitiesAffected == null) { /*do nothing*/ }
            else if (QualitiesAffected == null && successEvent.QualitiesAffected != null) { return false; }
            else if (QualitiesAffected != null && successEvent.QualitiesAffected == null) { return false; }
            else
            {
                foreach (QualitiesAffected qa in QualitiesAffected)
                {
                    //check against the master list of child branches and confirm the childbranch matches in the list.
                    //If a child object is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (QualitiesAffected qa2 in successEvent.QualitiesAffected)
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
            if (ChildBranches == null && successEvent.ChildBranches == null) { /*Do nothing*/ }
            else if (ChildBranches == null && successEvent.ChildBranches != null) { return false; }
            else if (ChildBranches != null && successEvent.ChildBranches == null) { return false; }
            else
            {
                //For each child branch required from this addon event
                foreach (ChildBranches cb in ChildBranches)
                {
                    //check against the master list of child branches and confirm the childbranch matches in the list.
                    //If a child object is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (ChildBranches cb2 in successEvent.ChildBranches)
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
