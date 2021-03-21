using LaundryIroningContract.Infrastructure;
using LaundryIroningEntity.Contract;
using LaundryIroningEntity.Entity;

namespace LaundryIroningContract.Repository
{
    public interface IUserTypesRepository: IBaseRepository<UserTypes>, ISelectService<UserTypes>, ISelectFirstOrDefaultService<UserTypes>
    {
        IUnitOfWork Uow
        {
            get; set;
        }
    }
}
