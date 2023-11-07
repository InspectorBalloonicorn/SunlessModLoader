using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SunlessModLoader.Classes.Classes
{
    public class Availability
    {
        public Quality? Quality { get; set; }
        public int? Cost { get; set; }
        public int? SellPrice { get; set; }
        public PurchaseQuality? PurchaseQuality { get; set; }
        public int Id { get; set; }
        public string? BuyMessage { get; set; }
        public string? SellMessage { get; set; }

        public bool IsEquals(Availability? avail)
        {
            if (ReferenceEquals(avail, null) && ReferenceEquals(this, null)) { return true; }
            if (ReferenceEquals(avail, null) && !ReferenceEquals(this, null)) { return false; }
            if (!ReferenceEquals(avail, null) && ReferenceEquals(this, null)) { return false; }

            if (Cost != avail.Cost) { return false; }
            if (SellPrice != avail.SellPrice) { return false; }
            if (Id != avail.Id) { return false; }
            if (BuyMessage != avail.BuyMessage) {  return false; }
            if (SellMessage != avail.SellMessage) { return false; }

            //check Quality
            if (Quality == null && avail.Quality == null) { /*Do Nothing*/ }
            else if (Quality == null && avail.Quality != null) { return false; }
            else if (Quality != null && avail.Quality == null) { return false; }
            else { if (!Quality.IsEquals(avail.Quality)) { return false; } }

            //check PurchaseQuality
            if (PurchaseQuality == null && avail.PurchaseQuality == null) { /*Do Nothing*/ }
            else if (PurchaseQuality == null && avail.PurchaseQuality != null) { return false; }
            else if (PurchaseQuality != null && avail.PurchaseQuality == null) { return false; }
            else { if (!PurchaseQuality.IsEquals(avail.PurchaseQuality)) { return false; } }

            return true;
        }
    }
}
