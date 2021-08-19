using System;
using System.Collections.Generic;
using System.Text;

namespace LaundryIroningEntity.ViewModels
{
   public class CustomerProfileViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string SecurityAnsOne { get; set; }
        public string SecurityAnsTwo { get; set; }
        public string PromoCode { get; set; }
        public int? PromoCodePoints { get; set; }
    }
}
