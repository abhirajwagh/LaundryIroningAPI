using System;
using System.Collections.Generic;
using System.Text;

namespace LaundryIroningEntity.Entity
{
    public class OrderAgentMapping
    {
        public Guid OrderMappingId { get; set; }
        public string OrderId { get; set; }
        public Guid AgentId { get; set; }
    }
}
