using System;
using System.Collections.Generic;
using System.Text;

namespace LaundryIroningEntity.Entity
{
   public class PromoCodes
    {
        public Guid PromoCodeId { get; set; }
        public string PromoCode { get; set; }
        public int PromoCodePoints { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
