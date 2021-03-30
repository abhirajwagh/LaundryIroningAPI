using System;
using System.Collections.Generic;
using System.Text;

namespace LaundryIroningEntity.ViewModels
{
   public class AgentViewModel
    {
        public Guid? UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
    }
}
