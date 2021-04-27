using System;
using System.Collections.Generic;
using System.Text;

namespace LaundryIroningEntity.ViewModels
{
   public class IroningLaundryOrderViewModel
    {
        public int Id { get; set; }

        public string OrderId { get; set; }
        public DateTime PickUpDate { get; set; }
        public string PickUpTimeSlot { get; set; }
        public string PickUpAddress { get; set; }
        public int NoOfKgs { get; set; }
        public Guid OrderBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? ClothsTypeId { get; set; }
        public bool? IsDelivered { get; set; }
        public string TotalCost { get; set; }
    }
}
