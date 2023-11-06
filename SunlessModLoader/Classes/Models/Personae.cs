﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunlessModLoader.Classes.Classes
{
    public class Personae
    {
        public List<QualitiesAffected>? QualitiesAffected { get; set; }
        public List<QualitiesRequired>? QualitiesRequired { get; set; }
        public string? Description { get; set; }
        public string? OwnerName { get; set; }
        public Setting? Setting { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public string? Name { get; set; }
        public int Id { get; set; }

        public bool IsEquals(Personae personae)
        {
            bool matchFound;
            if (ReferenceEquals(personae, null) && ReferenceEquals(this, null)) { return true; }
            //if one is null, and the other is not, return false immediately
            if (ReferenceEquals(personae, null) && !ReferenceEquals(this, null)) { return false; }
            if (!ReferenceEquals(personae, null) && ReferenceEquals(this, null)) { return false; }

            foreach (QualitiesAffected qa in QualitiesAffected)
            {
                //check against the master list of child branches and confirm the childbranch matches in the list.
                //If a child object is found that doesn't match exactly, the events are not equal.
                matchFound = false;
                foreach (QualitiesAffected qa2 in personae.QualitiesAffected)
                {
                    if (qa.IsEquals(qa2))
                    {
                        matchFound = true;
                    };
                }
                if (matchFound == false) return false;
            }




            //if (!QualitiesRequired.IsEquals(@event.QualitiesRequired))
            //{
            // MergeQualitiesRequired(addonEvent, masterEvent);
            // }

            foreach (QualitiesRequired qr in QualitiesRequired)
            {
                //check against the master list of child branches and confirm the childbranch matches in the list.
                //If a child object is found that doesn't match exactly, the events are not equal.
                matchFound = false;
                foreach (QualitiesRequired qr2 in personae.QualitiesRequired)
                {
                    if (qr.IsEquals(qr2))
                    {
                        matchFound = true;
                    };
                }
                if (matchFound == false) return false;
            }


            if (Description != personae.Description) { return false; }
            if (OwnerName != personae.OwnerName) {  return false; }
            if (!Setting.IsEquals(personae.Setting)) { return false; }
            if (!DateTimeCreated.Equals(personae.DateTimeCreated)) { return false; }
            if (Name != personae.Name) { return false; }
            if (Id != personae.Id) { return false; }

            return true;
        }
    }
}
