using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunlessModLoader.Classes.Classes
{
    public class PurchaseQuality
    {
        public int Id { get; set; }

        public bool IsEquals(PurchaseQuality purcsQual)
        {
            if (ReferenceEquals(purcsQual, null) && ReferenceEquals(this, null)) { return true; }
            //if one is null, and the other is not, return false immediately
            if (ReferenceEquals(purcsQual, null) && !ReferenceEquals(this, null)) { return false; }
            if (!ReferenceEquals(purcsQual, null) && ReferenceEquals(this, null)) { return false; }

            return Id == purcsQual.Id;
        }
    }
}
