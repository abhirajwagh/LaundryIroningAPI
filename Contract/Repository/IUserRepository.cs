using LaundryIroningContract.Infrastructure;
using LaundryIroningEntity.Contract;
using LaundryIroningEntity.Entity;
using LaundryIroningEntity.ViewModels;
using LaundryIroningEntity.ViewModels.StoredProcedureModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaundryIroningContract.Repository
{
   public interface IUserRepository: IBaseRepository<Users>, ISelectService<Users>, ISelectFirstOrDefaultService<Users>
    {
        IUnitOfWork Uow
        {
            get; set;
        }

        Task<List<AdminAgentUserViewModel>> GetAdminAgentOperatorUsersAsync(List<string> userType);
        Task<List<GetAllOrdersForCustomer>> GetAllOrdersForCustomerAsync(Guid customerId, int noOfDays);
    }
}
