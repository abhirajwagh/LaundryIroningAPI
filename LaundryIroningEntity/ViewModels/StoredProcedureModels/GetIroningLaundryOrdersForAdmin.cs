using System;
using System.Collections.Generic;
using System.Text;

namespace LaundryIroningEntity.ViewModels.StoredProcedureModels
{
   public class GetIroningLaundryOrdersForAdmin
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public DateTime PickUpDate { get; set; }
        public string PickUpTimeSlot { get; set; }
        public string PickUpAddress { get; set; }
        public int NoOfKgs { get; set; }
        public DateTime CreatedAt { get; set; }
        public string TotalCost { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? PickedAt { get; set; }
        public DateTime? ProcessedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public Guid? AgentId { get; set; }
        public string AgentUserName { get; set; }
        public string AgentName { get; set; }
        public string AgentMobileNo { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerUserName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobileNo { get; set; }
        public string PickedBy { get; set; }
        public string ProcessedBy { get; set; }
        public string DeliveredBy { get; set; }
    }
}
