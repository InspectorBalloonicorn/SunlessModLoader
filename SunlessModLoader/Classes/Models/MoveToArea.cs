using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunlessModLoader.Classes.Classes
{
    public class MoveToArea
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageName { get; set; }
        public string? MoveMessage { get; set; }
        public int Id { get; set; }

        public bool IsEquals(MoveToArea mvToArea)
        {
            if (ReferenceEquals(mvToArea, null) && ReferenceEquals(this, null)) { return true; }
            //if one is null, and the other is not, return false immediately
            if (ReferenceEquals(mvToArea, null) && !ReferenceEquals(this, null)) { return false; }
            if (!ReferenceEquals(mvToArea, null) && ReferenceEquals(this, null)) { return false; }

            if (Name != mvToArea.Name) return false;
            if (Description != mvToArea.Description) return false;
            if (ImageName != mvToArea.ImageName) return false;
            if (MoveMessage != mvToArea.MoveMessage) return false;
            if (Id != mvToArea.Id) return false;

            return true;
        }
    }
}
