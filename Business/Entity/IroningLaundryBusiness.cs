using LaundryIroningCommon;
using LaundryIroningContract.Business;
using LaundryIroningContract.Repository;
using LaundryIroningEntity.Contract;
using LaundryIroningEntity.Entity;
using LaundryIroningEntity.ViewModels;
using LaundryIroningEntity.ViewModels.StoredProcedureModels;
using LaundryIroningHelper.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryIroningBusiness.Entity
{
    public class IroningLaundryBusiness: IIroningLaundryBusiness
    {
        #region Private Variable

        private readonly IIroningLaundryRepository _ironingLaundryRepository;
        private readonly ILaundryRepository _laundryRepository;
        private readonly IIroningRepository _ironingRepository;
        private readonly IOrderAgentMappingRepository _orderAgentMappingRepository;

        #endregion

        #region Constructor

        public IroningLaundryBusiness(IIroningLaundryRepository ironingLaundryRepository, ILaundryRepository laundryRepository,
            IIroningRepository ironingRepository, IOrderAgentMappingRepository orderAgentMappingRepository)
        {
            _ironingLaundryRepository = ironingLaundryRepository;
            _laundryRepository = laundryRepository;
            _ironingRepository = ironingRepository;
            _orderAgentMappingRepository = orderAgentMappingRepository;
        }

        public IUnitOfWork Uow
        {
            get
            {
                return _ironingLaundryRepository.Uow;
            }
            set
            {
                _ironingRepository.Uow = value;
                _orderAgentMappingRepository.Uow = value;
                _laundryRepository.Uow = value;
                _ironingLaundryRepository.Uow = value;
            }
        }

        #endregion

        #region Get Method
        public async Task<List<IroningLaundryOrder>> GetAllIroningLaundryOrderAsync()
        {
            var orderList = await _ironingLaundryRepository.SelectAsync();
            return orderList.ToList();
        }

        public async Task<IroningLaundryOrderViewModel> GetIroningLaundryOrderAsync(int orderId)
        {
            return await _ironingLaundryRepository.GetIroningLaundryOrderAsync(orderId);
        }

        public async Task<List<GetIroningLaundryOrdersForAdmin>> GetIroningLaundryOrdersForAdminAsync()
        {
            return await _ironingLaundryRepository.GetIroningLaundryOrdersForAdminAsync();
        }
        #endregion

        #region Add Method

        /// <summary>
        /// Add new ironing laundry order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<int> AddIroningLaundryOrderAsync(IroningLaundryOrder order)
        {
            if (order.NoOfCloths == 0 || order.PickUpDate == null || order.PickUpTimeSlot == null || order.PickUpAddress == null
                || order.OrderStatus == null)
                return (int)StatusCode.ExpectationFailed;

            order.CreatedAt = DateTime.UtcNow;
            await _ironingLaundryRepository.AddAsync(order);
            await _ironingLaundryRepository.Uow.SaveChangesAsync();

            var agentAssignmentCountList = (await _ironingRepository.GetAgentOrdersAssignmentCountAsync()).ToList();
            var addedOrder = await GetIroningLaundryOrderAsync(order.Id);
            if (agentAssignmentCountList.Count() > 0 && addedOrder != null)
            {
                var orderMapping = new OrderAgentMapping();
                orderMapping.OrderMappingId = Guid.NewGuid();
                orderMapping.AgentId = agentAssignmentCountList[0].AgentId;
                orderMapping.OrderId = addedOrder.OrderId;
                await _orderAgentMappingRepository.AddAsync(orderMapping);
                await _orderAgentMappingRepository.Uow.SaveChangesAsync();
            }
            return order.Id;
        }

        #endregion

    }
}
