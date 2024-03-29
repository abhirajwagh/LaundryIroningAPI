﻿using LaundryIroningAPI.CommonMethod;
using LaundryIroningContract.Business;
using LaundryIroningEntity.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using LaundryIroningEntity.Entity;
using LaundryIroningCommon;
using System;
using System.Collections.Generic;
using LaundryIroningEntity.ViewModels;

namespace LaundryIroningAPI.Ironing
{
    [Route("api/[controller]/[action]")]
    public class IroningController : Controller
    {
        #region Private Veriables

        private readonly IIroningBusiness _ironingBusiness;
        CommonMethods commonMethods;
        #endregion

        #region Constructor
        public IroningController(IUnitOfWork uow, IIroningBusiness ironingBusiness)
        {
            _ironingBusiness = ironingBusiness;
            _ironingBusiness.Uow = uow;
            commonMethods = new CommonMethods();
        }
        #endregion

        #region Get Methods

        [HttpGet]
        [ActionName("GetAllIroningOrders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllIroningOrderAsync()
        {
            return Ok(await _ironingBusiness.GetAllIroningOrderAsync());
        }

        [HttpGet]
        [ActionName("GetOrderById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetIroningOrderAsync(int orderId)
        {
            return Ok(await _ironingBusiness.GetIroningOrderAsync(orderId));
        }

        [HttpGet]
        [ActionName("GetIroningOrdersForAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetIroningOrdersForAdminAsync()
        {
            return Ok(await _ironingBusiness.GetIroningOrdersForAdminAsync());
        }

        [HttpGet]
        [ActionName("GetAllNewOrdersForAgent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllNewOrdersForAgentAsync(Guid? agentId)
        {
            return Ok(await _ironingBusiness.GetAllNewOrdersForAgentAsync(agentId));
        }

        [HttpGet]
        [ActionName("GetAllProcessedOrdersForAgent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProcessedOrdersForAgentAsync(Guid? agentId)
        {
            return Ok(await _ironingBusiness.GetAllProcessedOrdersForAgentAsync(agentId));
        }

        [HttpGet]
        [ActionName("GetAllPickedOrdersForOperator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPickedOrdersForOperatorAsync(Guid? operatorId)
        {
            return Ok(await _ironingBusiness.GetAllPickedOrdersForOperatorAsync(operatorId));
        }

        #endregion

        #region Post Methods

        [HttpPost]
        [ActionName("AddIroningOrder")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddIroningOrderAsync(
           [FromBody, SwaggerParameter("Model containing the details of the new order to create", Required = true)] IroningOrder order)
        {
            return Ok(await _ironingBusiness.AddIroningOrderAsync(order));
        }

        [HttpPost]
        [ActionName("UpdateOrderAssignment")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateOrderAssignmentAsync(
            [SwaggerParameter("Id for mapping the orders", Required = true)] Guid agentId,
           [FromBody, SwaggerParameter("list containing the details of the orders to update", Required = true)] List<string> orderIds)
        {
            return Ok(await _ironingBusiness.UpdateOrderAssignemnt(agentId,orderIds));
        }

        [HttpPost]
        [ActionName("UpdateOrderStatus")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateOrderStatusAsync(
            [FromBody,SwaggerParameter("orderDetailsforUpdateStatus", Required = true)] UpdateOrderStatusViewModel updateOrderStatusViewModel)
        {
            return Ok(await _ironingBusiness.UpdateOrderStatusAsync(updateOrderStatusViewModel));
        }
        #endregion
    }
}
