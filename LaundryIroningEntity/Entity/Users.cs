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
        public bool IsAdmin { get; set; }
    }
}
