using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunlessModLoader.Classes.Classes
{
    public class DefaultEvent
    {
        public List<ChildBranches>? ChildBranches { get; set; }
        public List<QualitiesAffected>? QualitiesAffected { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ExoticEffects { get; set; }
        public int Id { get; set; }
        public LinkToEvent? LinkToEvent { get; set; }
        public string? Category { get; set; }
        public MoveToArea? MoveToArea { get; set; }
        public SwitchToSetting? SwitchToSetting { get; set; }
        public int? SwitchToSettingId { get; set; }
        public string? Urgency { get; set; }

        public bool IsEquals(DefaultEvent? defEvent)
        {
            bool matchFound;

            if (ReferenceEquals(defEvent, null) && ReferenceEquals(this, null)) { return true; }
            //if one is null, and the other is not, return false immediately
            if (ReferenceEquals(defEvent, null) && !ReferenceEquals(this, null)) { return false; }
            if (!ReferenceEquals(defEvent, null) && ReferenceEquals(this, null)) { return false; }

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

            if (Name != defEvent.Name) { return false; }
            if (Description != defEvent.Description) { return false; }
            if (ExoticEffects != defEvent.ExoticEffects) { return false; }
            if (Id != defEvent.Id) {  return false; }


            //Check LinkToEvent
            if (LinkToEvent == null && defEvent.LinkToEvent == null) { /*Do Nothing*/ }
            else if (LinkToEvent == null && defEvent.LinkToEvent != null) { return false; }
            else if (LinkToEvent != null && defEvent.LinkToEvent == null) { return false; }
            else { if (!LinkToEvent.IsEquals(defEvent.LinkToEvent)) { return false; } }

            if (Category != defEvent.Category) { return false; }

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

            if (SwitchToSettingId !=  defEvent.SwitchToSettingId) {  return false; }
            if (Urgency != defEvent.Urgency) {  return false; }

            return true;
        }
    }
}
