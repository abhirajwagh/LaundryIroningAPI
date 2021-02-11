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
    public class IroningBusiness: IIroningBusiness
    {
        #region Private Variable

        private readonly IIroningRepository _ironingRepository;

        #endregion

        #region Constructor

        public IroningBusiness(IIroningRepository ironingRepository)
        {
            _ironingRepository = ironingRepository;
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
            }
        }

        #endregion

        #region Get Method
        public async Task<List<IroningOrder>> GetAllIroningOrderAsync()
        {
            var orderList = await _ironingRepository.SelectAsync();
            return orderList.ToList();
        }

        public async Task<IroningOrder> GetIroningOrderAsync(int orderId)
        {
            var orderdetails = (await _ironingRepository.SelectAsync(x=>x.Id == orderId)).FirstOrDefault();
            return orderdetails;
        }

        #endregion

        #region Add Method

        /// <summary>
        /// Add new ironing user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<int> AddIroningOrderAsync(IroningOrder order)
        {
            if (order.NoOfCloths == 0 || order.PickUpDate == null || order.PickUpTimeSlot == null || order.PickUpAddress == null)
                return (int)StatusCode.ExpectationFailed;

            order.CreatedAt = DateTime.UtcNow;
            order.IsDelivered = false;

            await _ironingRepository.AddAsync(order);
            await _ironingRepository.Uow.SaveChangesAsync();
            return order.Id;
        }

        #endregion

    }
}
