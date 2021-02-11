using LaundryIroningContract.Infrastructure;
using LaundryIroningEntity.Contract;
using LaundryIroningEntity.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaundryIroningContract.Repository
{
   public interface IIroningRepository: IBaseRepository<IroningOrder>, ISelectService<IroningOrder>, ISelectFirstOrDefaultService<IroningOrder>
    {
        IUnitOfWork Uow
        {
            get; set;
        }
    }
}
