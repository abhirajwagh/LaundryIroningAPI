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
   public class IroningRepository: BaseRepository<IroningOrder>, IIroningRepository
    {
        protected readonly IExecuterStoreProc _executerStoreProc;

        #region Public Variables

        public IroningRepository(IExecuterStoreProc executerStoreProc)
        {
            _executerStoreProc = executerStoreProc;
            Uow = _executerStoreProc.uow;
        }
        public IUnitOfWork Uow
        {
            get { return base.UnitOfWork; }
            set { base.UnitOfWork = value; }
        }


        public async Task<IroningOrderViewModel> GetIroningOrderAsync(int orderId)
        {
            List<Parameters> param = new List<Parameters>()
            {
                new Parameters("orderId", orderId)
            };
            return (await _executerStoreProc.ExecuteProcAsync<IroningOrderViewModel>(ProcedureConstants.GetIroningOrderDetailsById, param))[0];
        }
        #endregion
    }
}
