using LaundryIroningEntity.Contract;
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

        Task<int> AddUserAsync(Users user);

        Task<Users> GetUserDetailsAsync(Login login);
    }
}
