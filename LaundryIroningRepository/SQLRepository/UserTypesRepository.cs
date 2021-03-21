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
   public class UserTypesRepository: BaseRepository<UserTypes>, IUserTypesRepository
    {
        protected readonly IExecuterStoreProc _executerStoreProc;

        #region Public Variables

        public UserTypesRepository(IExecuterStoreProc executerStoreProc)
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
