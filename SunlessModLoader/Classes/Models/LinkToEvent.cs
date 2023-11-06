using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunlessModLoader.Classes.Classes
{
    public class LinkToEvent
    {
        public int Id { get; set; }

        public bool IsEquals(LinkToEvent lnkToEvnt)
        {
            if (ReferenceEquals(lnkToEvnt, null) && ReferenceEquals(this, null)) { return true; }
            //if one is null, and the other is not, return false immediately
            if (ReferenceEquals(lnkToEvnt, null) && !ReferenceEquals(this, null)) { return false; }
            if (!ReferenceEquals(lnkToEvnt, null) && ReferenceEquals(this, null)) { return false; }

            return Id == lnkToEvnt.Id;
        }
    }
}
