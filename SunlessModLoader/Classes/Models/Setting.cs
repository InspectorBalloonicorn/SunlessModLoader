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
            if (StartingArea != setting.StartingArea) return false;
            if (StartingDomicile != setting.StartingDomicile) return false;
            if (ItemsUsableHere != setting.ItemsUsableHere) return false;
            if (TurnLengthSeconds != setting.TurnLengthSeconds) return false;
            if (MaxActionsAllowed != setting.MaxActionsAllowed) return false;
            if (MaxCardsAllowed != setting.MaxCardsAllowed) return false;
            if (ActionsInPeriodBeforeExhaustion != setting.ActionsInPeriodBeforeExhaustion) return false;
            if (Description != setting.Description) return false;
            if (Id != setting.Id) return false;

            //check Exchange
            if (Exchange == null && setting.Exchange == null) { /*Do Nothing*/ }
            else if (Exchange == null && setting.Exchange != null) { return false; }
            else if (Exchange != null && setting.Exchange == null) { return false; }
            else { if (!Exchange.IsEquals(setting.Exchange)) { return false; } }

            //Check Personae
            if (Personae == null && setting.Personae == null) { /*do nothing*/ }
            else if (Personae == null && setting.Personae != null) { return false; }
            else if (Personae != null && setting.Personae == null) { return false; }
            else
            {
                foreach (Personae persona in Personae)
                {
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
            }

            return true;
        }
    }
}
