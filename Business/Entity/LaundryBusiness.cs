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
    public class LaundryBusiness: ILaundryBusiness
    {
        #region Private Variable

        private readonly ILaundryRepository _laundryRepository;
        private readonly IIroningRepository _ironingRepository;
        private readonly IOrderAgentMappingRepository _orderAgentMappingRepository;
        private readonly IIroningLaundryRepository _ironingLaundryRepository;
        #endregion

        #region Constructor

        public LaundryBusiness(ILaundryRepository laundryRepository, IIroningRepository ironingRepository,
            IOrderAgentMappingRepository orderAgentMappingRepository, IIroningLaundryRepository ironingLaundryRepository)
        {
            _laundryRepository = laundryRepository;
            _ironingRepository = ironingRepository;
            _orderAgentMappingRepository = orderAgentMappingRepository;
            _ironingLaundryRepository = ironingLaundryRepository;
        }

        public IUnitOfWork Uow
        {
            get
            {
                return _laundryRepository.Uow;
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
        public async Task<List<LaundryOrder>> GetAllLaundryOrderAsync()
        {
            var orderList = await _laundryRepository.SelectAsync();
            return orderList.ToList();
        }

        public async Task<LaundryOrderViewModel> GetLaundryOrderAsync(int orderId)
        {
            return await _laundryRepository.GetLaundryOrderAsync(orderId);
        }

        public async Task<List<GetLaundryOrdersForAdmin>> GetLaundryOrdersForAdminAsync()
        {
            return await _laundryRepository.GetLaundryOrdersForAdminAsync();
        }

        #endregion

        #region Add Method

        /// <summary>
        /// Add new laundry order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<int> AddLaundryOrderAsync(LaundryOrder order)
        {
            if (order.NoOfCloths == 0 || order.PickUpDate == null || order.PickUpTimeSlot == null || order.PickUpAddress == null
                || order.OrderStatus == null)
                return (int)StatusCode.ExpectationFailed;

            order.CreatedAt = DateTime.UtcNow;
            await _laundryRepository.AddAsync(order);
            await _laundryRepository.Uow.SaveChangesAsync();

            var agentAssignmentCountList = (await _ironingRepository.GetAgentOrdersAssignmentCountAsync()).ToList();
            var addedOrder = await GetLaundryOrderAsync(order.Id);
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
