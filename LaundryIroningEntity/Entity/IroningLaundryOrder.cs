using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LaundryIroningEntity.Entity
{
    public class IroningLaundryOrder
    {
        public int Id { get; set; }
        public DateTime PickUpDate { get; set; }
        public string PickUpTimeSlot { get; set; }
        public string PickUpAddress { get; set; }
        public int NoOfKgs { get; set; }
        public Guid OrderBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string TotalCost { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? PickedAt { get; set; }
        public DateTime? ProcessedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public string AgentComment { get; set; }
        public string OperatorComment { get; set; }

        
    }
}
