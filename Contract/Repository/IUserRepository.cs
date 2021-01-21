using LaundryIroningContract.Infrastructure;
using LaundryIroningEntity.Contract;
using LaundryIroningEntity.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaundryIroningContract.Repository
{
   public interface IUserRepository: IBaseRepository<Users>, ISelectService<Users>, ISelectFirstOrDefaultService<Users>
    {
        IUnitOfWork Uow
        {
            get; set;
        }
    }
}
