using LaundryIroningContract.Infrastructure;
using LaundryIroningEntity.Contract;
using LaundryIroningEntity.Entity;

namespace LaundryIroningContract.Repository
{
    public interface IOrderAgentMappingRepository : IBaseRepository<OrderAgentMapping>, ISelectService<OrderAgentMapping>, ISelectFirstOrDefaultService<OrderAgentMapping>
    {
        IUnitOfWork Uow
        {
            get; set;
        }
    }
}
