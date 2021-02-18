using LaundryIroningCommon;
using LaundryIroningContract.Business;
using LaundryIroningContract.Repository;
using LaundryIroningEntity.Contract;
using LaundryIroningEntity.Entity;
using LaundryIroningEntity.ViewModels;
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

        #endregion

        #region Constructor

        public IroningLaundryBusiness(IIroningLaundryRepository ironingLaundryRepository)
        {
            _ironingLaundryRepository = ironingLaundryRepository;
        }

        public IUnitOfWork Uow
        {
            get
            {
                return _ironingLaundryRepository.Uow;
            }
            set
            {
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

        #endregion

        #region Add Method

        /// <summary>
        /// Add new ironing laundry order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<int> AddIroningLaundryOrderAsync(IroningLaundryOrder order)
        {
            if (order.NoOfCloths == 0 || order.PickUpDate == null || order.PickUpTimeSlot == null || order.PickUpAddress == null)
                return (int)StatusCode.ExpectationFailed;

            order.CreatedAt = DateTime.UtcNow;
            order.IsDelivered = false;

            await _ironingLaundryRepository.AddAsync(order);
            await _ironingLaundryRepository.Uow.SaveChangesAsync();
            return order.Id;
        }

        #endregion

    }
}
