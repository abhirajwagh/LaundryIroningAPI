﻿using LaundryIroningCommon;
using LaundryIroningContract.Infrastructure;
using LaundryIroningContract.Repository;
using LaundryIroningEntity.Contract;
using LaundryIroningEntity.Entity;
using LaundryIroningEntity.ViewModels;
using LaundryIroningEntity.ViewModels.StoredProcedureModels;
using LaundryIroningHelper;
using LaundryIroningRepository.CommonRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaundryIroningRepository.SQLRepository
{
   public class LaundryRepository: BaseRepository<LaundryOrder>, ILaundryRepository
    {
        protected readonly IExecuterStoreProc _executerStoreProc;

        #region Public Variables

        public LaundryRepository(IExecuterStoreProc executerStoreProc)
        {
            _executerStoreProc = executerStoreProc;
            Uow = _executerStoreProc.uow;
        }
        public IUnitOfWork Uow
        {
            get { return base.UnitOfWork; }
            set { base.UnitOfWork = value; }
        }


        public async Task<LaundryOrderViewModel> GetLaundryOrderAsync(int orderId)
        {
            List<Parameters> param = new List<Parameters>()
            {
                new Parameters("orderId", orderId)
            };
            return (await _executerStoreProc.ExecuteProcAsync<LaundryOrderViewModel>(ProcedureConstants.GetLaundryOrderDetailsById, param))[0];
        }

        public async Task<List<GetLaundryOrdersForAdmin>> GetLaundryOrdersForAdminAsync()
        {
            return (await _executerStoreProc.ExecuteProcAsync<GetLaundryOrdersForAdmin>(ProcedureConstants.GetLaundryOrdersForAdmin));
        }
        #endregion
    }
}
