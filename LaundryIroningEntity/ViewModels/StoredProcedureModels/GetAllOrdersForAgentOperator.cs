using System;
using System.Collections.Generic;
using System.Text;

namespace LaundryIroningEntity.ViewModels.StoredProcedureModels
{
   public class GetAllOrdersForAgentOperator
    {
        public string OrderNumber { get; set; }
        public DateTime PickUpDate { get; set; }
        public string PickUpTimeSlot { get; set; }
        public string PickUpAddress { get; set; }
        public int? NoOfCloths { get; set; }
        public int? NoOfKgs { get; set; }
        public string PaymentMode { get; set; }
        public string TotalCost { get; set; }
        public string OrderStatus { get; set; }
        public string OrderType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PickedAt { get; set; }
        public DateTime? ProcessedAt { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobileNo { get; set; }

    }
}
