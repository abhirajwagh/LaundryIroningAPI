using LaundryIroningCommon;
using LaundryIroningContract.Infrastructure;
using LaundryIroningContract.Repository;
using LaundryIroningEntity.Contract;
using LaundryIroningEntity.Entity;
using LaundryIroningEntity.ViewModels;
using LaundryIroningHelper;
using LaundryIroningRepository.CommonRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaundryIroningRepository.SQLRepository
{
   public class IroningLaundryRepository: BaseRepository<IroningLaundryOrder>, IIroningLaundryRepository
    {
        protected readonly IExecuterStoreProc _executerStoreProc;

        #region Public Variables

        public IroningLaundryRepository(IExecuterStoreProc executerStoreProc)
        {
            _executerStoreProc = executerStoreProc;
            Uow = _executerStoreProc.uow;
        }
        public IUnitOfWork Uow
        {
            get { return base.UnitOfWork; }
            set { base.UnitOfWork = value; }
        }


        public async Task<IroningLaundryOrderViewModel> GetIroningLaundryOrderAsync(int orderId)
        {
            List<Parameters> param = new List<Parameters>()
            {
                new Parameters("orderId", orderId)
            };
            return (await _executerStoreProc.ExecuteProcAsync<IroningLaundryOrderViewModel>(ProcedureConstants.GetIroningLaundryOrderDetailsById, param))[0];
        }
        #endregion
    }
}
