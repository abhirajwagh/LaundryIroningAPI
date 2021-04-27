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
    public class IroningBusiness: IIroningBusiness
    {
        #region Private Variable

        private readonly IIroningRepository _ironingRepository;
        private readonly IOrderAgentMappingRepository _orderAgentMappingRepository;
        private readonly ILaundryRepository _laundryRepository;
        private readonly IIroningLaundryRepository _ironingLaundryRepository;
        #endregion

        #region Constructor

        public IroningBusiness(IIroningRepository ironingRepository, IOrderAgentMappingRepository orderAgentMappingRepository,
            ILaundryRepository laundryRepository, IIroningLaundryRepository ironingLaundryRepository)
        {
            _ironingRepository = ironingRepository;
            _orderAgentMappingRepository = orderAgentMappingRepository;
            _laundryRepository = laundryRepository;
            _ironingLaundryRepository = ironingLaundryRepository;
        }

        public IUnitOfWork Uow
        {
            get
            {
                return _ironingRepository.Uow;
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
        public async Task<List<IroningOrder>> GetAllIroningOrderAsync()
        {
            var orderList = await _ironingRepository.SelectAsync();
            return orderList.ToList();
        }

        public async Task<IroningOrderViewModel> GetIroningOrderAsync(int orderId)
        {
            return await _ironingRepository.GetIroningOrderAsync(orderId);
        }

        public async Task<List<GetIroningOrdersForAdmin>> GetIroningOrdersForAdminAsync()
        {
            return await _ironingRepository.GetIroningOrdersForAdminAsync();
        }

        public async Task<List<GetAllOrdersForAgentOperator>> GetAllNewOrdersForAgentAsync(Guid? agentId)
        {
            return await _ironingRepository.GetAllNewOrdersForAgentAsync(agentId);
        }

        public async Task<List<GetAllOrdersForAgentOperator>> GetAllProcessedOrdersForAgentAsync(Guid? agentId)
        {
            return await _ironingRepository.GetAllProcessedOrdersForAgentAsync(agentId);
        }
        public async Task<List<GetAllOrdersForAgentOperator>> GetAllPickedOrdersForOperatorAsync(Guid? operatorId)
        {
            return await _ironingRepository.GetAllPickedOrdersForOperatorAsync(operatorId);
        }

        #endregion

        #region Add Method

        /// <summary>
        /// Add new ironing order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<int> AddIroningOrderAsync(IroningOrder order)
        {
            if (order.NoOfCloths == 0 || order.PickUpDate == null || order.PickUpTimeSlot == null 
                || order.PickUpAddress == null || order.OrderStatus == null)
                return (int)StatusCode.ExpectationFailed;


            order.CreatedAt = DateTime.UtcNow;
            await _ironingRepository.AddAsync(order);
            await _ironingRepository.Uow.SaveChangesAsync();

            var agentAssignmentCountList = (await _ironingRepository.GetAgentOrdersAssignmentCountAsync()).ToList();
            var addedOrder = await GetIroningOrderAsync(order.Id);
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

        public async Task<int> UpdateOrderAssignemnt(Guid agentId,List<string> OrderId)
        {
            if (OrderId.Count()<=0)
                return (int)StatusCode.ExpectationFailed;

            var existingOrderMapping = await _orderAgentMappingRepository.SelectAsync(o=> OrderId.Contains(o.OrderId));
            if (existingOrderMapping.Any())
            {
                await _orderAgentMappingRepository.DeleteRangeAsync(existingOrderMapping);
            }
                var orderMappingList = new List<OrderAgentMapping>();

                for (int i = 0; i < OrderId.Count(); i++)
                {
                    var orderMapping = new OrderAgentMapping();
                    orderMapping.OrderMappingId = Guid.NewGuid();
                    orderMapping.AgentId = agentId;
                    orderMapping.OrderId = OrderId[i];
                    orderMappingList.Add(orderMapping);
                }
                await _orderAgentMappingRepository.AddRangeAsync(orderMappingList);
                await _orderAgentMappingRepository.Uow.SaveChangesAsync();
            

            return (int)StatusCode.SuccessfulStatusCode;
        }

        public async Task<int> UpdateOrderStatusAsync(UpdateOrderStatusViewModel updateOrderStatusViewModel)
        {
            return await _ironingRepository.UpdateOrderStatusAsync(updateOrderStatusViewModel);
        }

        #endregion

    }
}
