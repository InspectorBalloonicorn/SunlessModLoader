using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunlessModLoader.Classes.Classes
{
    public class Setting
    {
        public string? World { get; set; }
        public string? OwnerName { get; set; }
        public string? Name { get; set; }
        public List<Personae> Personae { get; set; }
        public string? StartingArea { get; set; }
        public string? StartingDomicile { get; set; }
        public bool? ItemsUsableHere { get; set; }
        public Exchange? Exchange { get; set; }
        public int? TurnLengthSeconds { get; set; }
        public int? MaxActionsAllowed { get; set; }
        public int? MaxCardsAllowed { get; set; }
        public int? ActionsInPeriodBeforeExhaustion { get; set; }
        public string? Description { get; set; }
        public int Id { get; set; }
        public bool IsEquals(Setting? setting)
        {
            bool matchFound;
            if (ReferenceEquals(setting, null) && ReferenceEquals(this, null)) { return true; }
            //if one is null, and the other is not, return false immediately
            if (ReferenceEquals(setting, null) && !ReferenceEquals(this, null)) { return false; }
            if (!ReferenceEquals(setting, null) && ReferenceEquals(this, null)) { return false; }

            if (World != setting.World) return false;
            if (OwnerName != setting.OwnerName) return false;
            if (Name != setting.Name) return false;

            foreach (Personae persona in Personae)
            {
                //check against the master list of Enhancements and confirm the childbranch Enhancements are in the list.
                //If an Enhancement is found that doesn't match exactly, the events are not equal.
                matchFound = false;
                foreach (Personae persona2 in setting.Personae)
                {
                    if (persona.IsEquals(persona2))
                    {
                        matchFound = true;
                    };
                }
                if (matchFound == false) return false;
            }

            if (StartingArea != setting.StartingArea) return false;
            if (StartingDomicile != setting.StartingDomicile) return false;
            if (ItemsUsableHere != setting.ItemsUsableHere) return false;
            if (Exchange != setting.Exchange) return false;
            if (TurnLengthSeconds != setting.TurnLengthSeconds) return false;
            if (MaxActionsAllowed != setting.MaxActionsAllowed) return false;
            if (MaxCardsAllowed != setting.MaxCardsAllowed) return false;
            if (ActionsInPeriodBeforeExhaustion !=  setting.ActionsInPeriodBeforeExhaustion) return false;
            if (Description !=  setting.Description) return false;
            if (Id != setting.Id) return false;

            return true;
        }

        //public bool Equals(Setting setting)
        //{
        //    if (World != setting.World) return false;
        //    if (OwnerName != setting.OwnerName) return false;
        //    if (Name != setting.Name) return false;
        //    if ()
        //}
    }
}
