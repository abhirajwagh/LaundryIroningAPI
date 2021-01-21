using LaundryIroningAPI.CommonMethod;
using LaundryIroningCommon;
using LaundryIroningContract.Business;
using LaundryIroningEntity.Contract;
using LaundryIroningEntity.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryIroningAPI.User
{
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        #region Private Veriables

        private readonly IUserBusiness _userBusiness;
        CommonMethods commonMethods;
        #endregion

        #region Constructor

        public UserController(IUnitOfWork uow , IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
            _userBusiness.Uow = uow;
            commonMethods = new CommonMethods();
        }

        #endregion

        #region Get Methods 
        [HttpGet]
        [ActionName("GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsersAsync()
        {
            return Ok(await _userBusiness.GetUsersAsync());
        }
        #endregion

        #region Post Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("AddUsers")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddUsersAsync(
           [FromBody, SwaggerParameter("Model containing the details of the new User to create", Required = true)] Users users)
        {
            int result = await _userBusiness.AddUserAsync(users);
            return commonMethods.GetResultMessages(result, MethodType.Add);
        }
        #endregion
    }
}
