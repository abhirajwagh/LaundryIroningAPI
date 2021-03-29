﻿using LaundryIroningEntity.Contract;
using LaundryIroningEntity.Entity;
using LaundryIroningEntity.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaundryIroningContract.Business
{
   public interface IUserBusiness
    {
        IUnitOfWork Uow { get; set; }
        Task<List<Users>> GetUsersAsync();
        Task<List<AdminAgentUserViewModel>> GetAdminAgentOperatorUsersAsync(List<string> userType);
        Task<int> AddUserAsync(Users user);
        Task<Users> GetUserDetailsAsync(Login login);
        Task<bool> GetUserNameAsync(string username);
        Task<int> AddAdminAgentUsersAsync(AdminAgentUserViewModel user);
        Task<int> UpdateAdminAgentUsersAsync(AdminAgentUserViewModel user);
        Task<int> DeleteAdminAgentUsers(Guid userId);
    }
}
