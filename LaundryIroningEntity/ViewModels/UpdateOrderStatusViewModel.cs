﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LaundryIroningEntity.ViewModels
{
   public class UpdateOrderStatusViewModel
    {
        public string OrderNumber { get; set; }
        public string OrderType { get; set; }
        public string OrderStatus { get; set; }
        public Guid ConfirmBy { get; set; }
    }
}
