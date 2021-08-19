using System;
using System.Collections.Generic;
using System.Text;

namespace LaundryIroningEntity.Entity
{
   public class Users
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid UserTypeId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string AadharNo { get; set; }
        public string DrivingLicenceNo { get; set; }
        public string SecurityAnswerOne { get; set; }
        public string SecurityAnswerTwo { get; set; }
        public string PromoCode { get; set; }
        public string PromoCodePoints { get; set; }
    }
}
