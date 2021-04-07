using System;
using System.Collections.Generic;
using System.Text;

namespace LaundryIroningEntity.ViewModels.StoredProcedureModels
{
   public class GetAgentOrderAssignmentCount
    {
        public Guid AgentId { get; set; }
        public int OrderCount { get; set; }
    }
}
