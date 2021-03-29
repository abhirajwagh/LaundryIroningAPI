using System;
using System.Collections.Generic;
using System.Text;

namespace LaundryIroningEntity.ViewModels
{
   public class AdminAgentUserViewModel
    {
        public Guid? UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? IsAdmin { get; set; }
        public Guid? UserTypeId { get; set; }
        public bool? IsAgent { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string AadharNo { get; set; }
        public string DrivingLicenceNo { get; set; }
        public bool? IsOperator { get; set; }
        public string UserType { get; set; }
    }
}
