using LaundryIroningContract.Infrastructure;
using LaundryIroningContract.Repository;
using LaundryIroningEntity.Contract;
using LaundryIroningEntity.Entity;
using LaundryIroningRepository.CommonRepository;
using System;
using System.Collections.Generic;
using System.Text;

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
        #endregion
    }
}
