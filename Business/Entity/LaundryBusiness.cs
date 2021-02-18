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
    public class LaundryBusiness: ILaundryBusiness
    {
        #region Private Variable

        private readonly ILaundryRepository _laundryRepository;

        #endregion

        #region Constructor

        public LaundryBusiness(ILaundryRepository laundryRepository)
        {
            _laundryRepository = laundryRepository;
        }

        public IUnitOfWork Uow
        {
            get
            {
                return _laundryRepository.Uow;
            }
            set
            {
                _laundryRepository.Uow = value;
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

        #endregion

        #region Add Method

        /// <summary>
        /// Add new laundry order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<int> AddLaundryOrderAsync(LaundryOrder order)
        {
            if (order.NoOfCloths == 0 || order.PickUpDate == null || order.PickUpTimeSlot == null || order.PickUpAddress == null)
                return (int)StatusCode.ExpectationFailed;

            order.CreatedAt = DateTime.UtcNow;
            order.IsDelivered = false;

            await _laundryRepository.AddAsync(order);
            await _laundryRepository.Uow.SaveChangesAsync();
            return order.Id;
        }

        #endregion

    }
}
