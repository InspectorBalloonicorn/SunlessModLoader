using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            return true;
        }
    }
}
