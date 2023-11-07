using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunlessModLoader.Classes.Classes
{
    public class Shop
    {
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public double? Ordering { get; set; }
        public List<Availability>? Availabilities { get; set; }
        public List<QualitiesRequired>? QualitiesRequired { get; set; }
        public int Id { get; set; }

        public bool IsEquals(Shop shop)
        {
            bool matchFound;
            if (ReferenceEquals(shop, null) && ReferenceEquals(this, null)) { return true; }
            //if one is null, and the other is not, return false immediately
            if (ReferenceEquals(shop, null) && !ReferenceEquals(this, null)) { return false; }
            if (!ReferenceEquals(shop, null) && ReferenceEquals(this, null)) { return false; }

            if (Name != shop.Name) { return false; }
            if (Image != shop.Image) { return false; }
            if (Description != shop.Description) {  return false; }
            if (Ordering != shop.Ordering) {  return false; }
            if (Id != shop.Id) { return false; }

            //Check QualitiesRequired
            if (QualitiesRequired == null && shop.QualitiesRequired == null) { return true; }
            else if (QualitiesRequired == null && shop.QualitiesRequired != null) { return false; }
            else if (QualitiesRequired != null && shop.QualitiesRequired == null) { return false; }
            else
            {
                foreach (QualitiesRequired qr in QualitiesRequired)
                {
                    //check against the master list of child branches and confirm the childbranch matches in the list.
                    //If a child object is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (QualitiesRequired qr2 in shop.QualitiesRequired)
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

            //Check Availabilities
            if (Availabilities == null && shop.Availabilities == null) { return true; }
            else if (Availabilities == null && shop.Availabilities != null) { return false; }
            else if (Availabilities != null && shop.Availabilities == null) { return false; }
            else
            {
                foreach (Availability av in Availabilities)
                {
                    //check against the master list of child branches and confirm the childbranch matches in the list.
                    //If a child object is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (Availability av2 in shop.Availabilities)
                    {
                        if (av.IsEquals(av2))
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
