using System;
using System.Collections.Generic;
using System.Text;

namespace LaundryIroningEntity.ViewModels
{
   public class IroningOrderViewModel
    {
        public int Id { get; set; }

        public string OrderId { get; set; }
        public DateTime PickUpDate { get; set; }
        public string PickUpTimeSlot { get; set; }
        public string PickUpAddress { get; set; }
        public int NoOfCloths { get; set; }
        public Guid OrderBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? ClothsTypeId { get; set; }
        public bool? IsDelivered { get; set; }
        public string TotalCost { get; set; }
        public string AgentComment { get; set; }
        public string OperatorComment { get; set; }
    }
}
