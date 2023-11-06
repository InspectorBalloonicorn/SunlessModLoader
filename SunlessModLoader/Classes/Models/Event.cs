using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunlessModLoader.Classes.Classes
{
    public class Event
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

        public Event()
        {
            ChildBranches = new List<ChildBranches>();
            QualitiesAffected = new List<QualitiesAffected>();
            QualitiesRequired = new List<QualitiesRequired>();
        }

        public bool IsEquals(Event? @event)
        {
            bool matchFound = false;

            if (ReferenceEquals(@event, null) && ReferenceEquals(this, null)) { return true; }
            //if one is null, and the other is not, return false immediately
            if (ReferenceEquals(@event, null) && !ReferenceEquals(this, null)) { return false; }
            if (!ReferenceEquals(@event, null) && ReferenceEquals(this, null)) { return false; }

            if (Autofire != @event.Autofire) { return false; }
            if (CanGoBack != @event.CanGoBack) { return false; }
            if (Category != @event.Category) { return false; }

            if (@event.ChallengeLevel != @event.ChallengeLevel) { return false; }

            //Check if either object is null, if they are, check if they are equal by null reference
            //Else check both objects.
            if (ChildBranches == null && @event.ChildBranches == null) { /*Do nothing*/ }
            else if (ChildBranches == null && @event.ChildBranches != null) { return false; }
            else if (ChildBranches != null && @event.ChildBranches == null) { return false; }
            else
            {
                //For each child branch required from this addon event
                foreach (ChildBranches cb in ChildBranches)
                {
                    //check against the master list of child branches and confirm the childbranch matches in the list.
                    //If a child object is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (ChildBranches cb2 in @event.ChildBranches)
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

            //check Deck
            if (Deck == null && @event.Deck == null) { /*Do Nothing*/ }
            else if (Deck == null && @event.Deck != null) { return false; }
            else if (Deck != null && @event.Deck == null) { return false; }
            else { if (!Deck.IsEquals(@event.Deck)) { return false; } }

            if (Description != @event.Description){ return false; }
            if (Distribution != @event.Distribution){ return false; }
            if (ExoticEffects != @event.ExoticEffects){ return false; }
            if (Image != @event.Image) {  return false; }

            //check LimitedToArea
            if (LimitedToArea == null && @event.LimitedToArea == null) { /*Do Nothing*/ }
            else if (LimitedToArea == null && @event.LimitedToArea != null) { return false; }
            else if (LimitedToArea != null && @event.LimitedToArea == null) { return false; }
            else { if (!LimitedToArea.IsEquals(@event.LimitedToArea)) { return false; } }


            if (Name != @event.Name){ return false; }
            if (Ordering != @event.Ordering) { return false; }

            //Check QualitiesAffected
            if (QualitiesAffected == null && @event.QualitiesAffected == null) { /*do nothing*/ }
            else if (QualitiesAffected == null && @event.QualitiesAffected != null) { return false; }
            else if (QualitiesAffected != null && @event.QualitiesAffected == null) { return false; }
            else
            {
                foreach (QualitiesAffected qa in QualitiesAffected)
                {
                    //check against the master list of child branches and confirm the childbranch matches in the list.
                    //If a child object is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (QualitiesAffected qa2 in @event.QualitiesAffected)
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
            if (QualitiesRequired == null && @event.QualitiesRequired == null) { return true; }
            else if (QualitiesRequired == null && @event.QualitiesRequired != null) { return false; }
            else if (QualitiesRequired != null && @event.QualitiesRequired == null) { return false; }
            else
            {
                foreach (QualitiesRequired qr in QualitiesRequired)
                {
                    //check against the master list of child branches and confirm the childbranch matches in the list.
                    //If a child object is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (QualitiesRequired qr2 in @event.QualitiesRequired)
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

            //Check Setting
            if (Setting == null && @event.Setting == null) { /*Do Nothing*/ }
            else if (Setting == null && @event.Setting != null) { return false; }
            else if (Setting != null && @event.Setting == null) { return false; }
            else { if (!Setting.IsEquals(@event.Setting)) { return false; } }

            if (ShowAsMessage != @event.ShowAsMessage) { return false; }

            if (Stickiness != @event.Stickiness) { return false; }

            if (Transient != @event.Transient) { return false; }

            if (Urgency != @event.Urgency) { return false; }

            return true;
        }
    }
}
