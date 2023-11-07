using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace SunlessModLoader.Classes.Classes
{
    public class Exchange
    {
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public List<Shop>? Shops { get; set; }
        public List<int>? SettingIds { get; set; }
        public int Id { get; set; }

        public bool IsEquals(Exchange? exchg)
        {
            bool matchFound;

            if (ReferenceEquals(exchg, null) && ReferenceEquals(this, null)) { return true; }
            //if one is null, and the other is not, return false immediately
            if (ReferenceEquals(exchg, null) && !ReferenceEquals(this, null)) { return false; }
            if (!ReferenceEquals(exchg, null) && ReferenceEquals(this, null)) { return false; }

            if (Name != exchg.Name) { return false; }
            if (Title != exchg.Title) {  return false; }
            if (Image != exchg.Image) { return false; }
            if (Description != exchg.Description){ return false; }
            if (Id != exchg.Id) { return false; }

            //Check SettingIds
            if (SettingIds == null && exchg.SettingIds == null) { /*Do nothing*/ }
            else if (SettingIds == null && exchg.SettingIds != null) { return false; }
            else if (SettingIds != null && exchg.SettingIds == null) { return false; }
            else
            {
                //For each child branch required from this addon event
                foreach (int settingId in SettingIds)
                {
                    //check against the master list of child branches and confirm the childbranch matches in the list.
                    //If a child object is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (int settingId2 in exchg.SettingIds)
                    {
                        if (settingId.Equals(settingId2))
                        {
                            matchFound = true;
                            break;
                        };
                    }
                    if (matchFound == false) return false;
                }
            }

            //Check Shops
            if (Shops == null && exchg.Shops == null) { /*Do nothing*/ }
            else if (Shops == null && exchg.Shops != null) { return false; }
            else if (Shops != null && exchg.Shops == null) { return false; }
            else
            {
                //For each child branch required from this addon event
                foreach (Shop shop in Shops)
                {
                    //check against the master list of child branches and confirm the childbranch matches in the list.
                    //If a child object is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (Shop shop2 in exchg.Shops)
                    {
                        if (shop.IsEquals(shop2))
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
