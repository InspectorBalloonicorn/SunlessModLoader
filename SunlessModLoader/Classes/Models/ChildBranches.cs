using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunlessModLoader.Classes.Classes
{
    public class ChildBranches
    {
        public DefaultEvent? DefaultEvent { get; set; }
        public ParentEvent? ParentEvent { get; set; }
        public List<QualitiesRequired>? QualitiesRequired { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public int? ActionCost { get; set; }
        public string? ButtonText { get; set; }
        public int Id { get; set; }
        public SuccessEvent? SuccessEvent { get; set; }
        public double? Ordering { get; set; }
        public RareDefaultEvent? RareDefaultEvent { get; set; }
        public int? RareDefaultEventChance { get; set; }
        public RareSuccessEvent? RareSuccessEvent { get; set; }
        public int? RareSuccessEventChance { get; set; }


        public ChildBranches()
        {
            QualitiesRequired = new List<QualitiesRequired>();
        }
        public bool IsEquals(ChildBranches? childBranches)
        {
            bool matchFound;

            if (ReferenceEquals(childBranches, null) && ReferenceEquals(this, null)) { return true; }
            //if one is null, and the other is not, return false immediately
            if (ReferenceEquals(childBranches, null) && !ReferenceEquals(this, null)) { return false; }
            if (!ReferenceEquals(childBranches, null) && ReferenceEquals(this, null)) { return false; }



            //Check each property

            //Check Default Event
            if (DefaultEvent == null && childBranches.DefaultEvent == null) { /*Do Nothing - Move on*/ }
            else if (DefaultEvent == null && childBranches.DefaultEvent != null) { return false; }
            else if (DefaultEvent != null && childBranches.DefaultEvent == null) { return false; }
            else {if (!DefaultEvent.IsEquals(childBranches.DefaultEvent)){ return false; }}

            //Check Parent Event
            if (ParentEvent == null && childBranches.ParentEvent == null) { /*Do Nothing*/ }
            else if (ParentEvent == null && childBranches.ParentEvent != null) { return false; }
            else if (ParentEvent != null && childBranches.ParentEvent == null) { return false; }
            else { if (!ParentEvent.IsEquals(childBranches.ParentEvent)) { return false; } }

            //For each quality required from this branch
            foreach (QualitiesRequired qr in QualitiesRequired)
            {
                //check against the other list of qualities required and confirm the QualityRequired matches an id in the list.
                //If a child object is found that doesn't match, they are not equal.
                matchFound = false;
                foreach (QualitiesRequired qr2 in childBranches.QualitiesRequired)
                {                   
                    if (qr.IsEquals(qr2))
                    {
                        matchFound = true;
                    };
                }
                if (matchFound == false) return false;
            }

            if(Name != childBranches.Name) return false;
            if(Image != childBranches.Image) return false;
            if(Description != childBranches.Description) return false;
            if(ActionCost != childBranches.ActionCost) return false;
            if(ButtonText != childBranches.ButtonText) return false;
            if(Id != childBranches.Id) return false;

            //Check SuccessEvent
            if (SuccessEvent == null && childBranches.SuccessEvent == null) { /*Do Nothing*/ }
            else if (SuccessEvent == null && childBranches.SuccessEvent != null) { return false; }
            else if (SuccessEvent != null && childBranches.SuccessEvent == null) { return false; }
            else { if (!SuccessEvent.IsEquals(childBranches.SuccessEvent)) { return false; } }

            if(Ordering != childBranches.Ordering) return false;

            //Check RareDefaultEvent
            if (RareDefaultEvent == null && childBranches.RareDefaultEvent == null) { /*Do Nothing*/ }
            else if (RareDefaultEvent == null && childBranches.RareDefaultEvent != null) { return false; }
            else if (RareDefaultEvent != null && childBranches.RareDefaultEvent == null) { return false; }
            else { if (!RareDefaultEvent.IsEquals(childBranches.RareDefaultEvent)) { return false; } }

            if (RareDefaultEventChance != childBranches.RareDefaultEventChance)

            //Check RareSuccessEvent
            if (RareSuccessEvent == null && childBranches.RareSuccessEvent == null) { /*Do Nothing*/ }
            else if (RareSuccessEvent == null && childBranches.RareSuccessEvent != null) { return false; }
            else if (RareSuccessEvent != null && childBranches.RareSuccessEvent == null) { return false; }
            else { if (!RareSuccessEvent.IsEquals(childBranches.RareSuccessEvent)) { return false; } }

            if (RareSuccessEventChance != childBranches.RareSuccessEventChance) return false;

            return true;
        }

    }
}
