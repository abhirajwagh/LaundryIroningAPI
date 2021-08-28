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
   public interface IPromoCodesRepository : IBaseRepository<PromoCodes>, ISelectService<PromoCodes>, ISelectFirstOrDefaultService<PromoCodes>
    {
        IUnitOfWork Uow
        {
            get; set;
        }
    }
}
