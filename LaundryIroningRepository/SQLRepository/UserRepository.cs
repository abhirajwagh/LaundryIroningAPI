using LaundryIroningCommon;
using LaundryIroningContract.Infrastructure;
using LaundryIroningContract.Repository;
using LaundryIroningEntity.Contract;
using LaundryIroningEntity.Entity;
using LaundryIroningEntity.ViewModels;
using LaundryIroningEntity.ViewModels.StoredProcedureModels;
using LaundryIroningHelper;
using LaundryIroningRepository.CommonRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaundryIroningRepository.SQLRepository
{
   public class UserRepository: BaseRepository<Users>, IUserRepository
    {
        protected readonly IExecuterStoreProc _executerStoreProc;

        #region Public Variables

        public UserRepository(IExecuterStoreProc executerStoreProc)
        {
            _executerStoreProc = executerStoreProc;
            Uow = _executerStoreProc.uow;
        }
        public IUnitOfWork Uow
        {
            get { return base.UnitOfWork; }
            set { base.UnitOfWork = value; }
        }

       public async Task<List<AdminAgentUserViewModel>> GetAdminAgentOperatorUsersAsync(List<string> userType)
        {
            var userTypeJson = JsonConvert.SerializeObject(userType);
            List<Parameters> param = new List<Parameters>()
            {
                new Parameters("UserType",userTypeJson)
            };
            return await _executerStoreProc.ExecuteProcAsync<AdminAgentUserViewModel>(ProcedureConstants.GetUserByUserType, param);
        }

        public async Task<List<GetAllOrdersForCustomer>> GetAllOrdersForCustomerAsync(Guid customerId, int noOfDays)
        {
            List<Parameters> param = new List<Parameters>()
            {
                new Parameters("customerId", customerId),
                new Parameters("noOfDays", noOfDays)
            };
            return (await _executerStoreProc.ExecuteProcAsync<GetAllOrdersForCustomer>(ProcedureConstants.GetAllOrdersForCustomer, param));
        }
        #endregion
    }
}
