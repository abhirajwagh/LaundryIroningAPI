﻿using LaundryIroningEntity.Contract;
using LaundryIroningEntity.Entity;
using LaundryIroningEntity.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaundryIroningContract.Business
{
   public interface IIroningBusiness
    {
        IUnitOfWork Uow { get; set; }

        Task<List<IroningOrder>> GetAllIroningOrderAsync();
        Task<IroningOrderViewModel> GetIroningOrderAsync(int orderId);
        Task<int> AddIroningOrderAsync(IroningOrder order);
    }
}
