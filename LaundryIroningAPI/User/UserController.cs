using LaundryIroningAPI.CommonMethod;
using LaundryIroningCommon;
using LaundryIroningContract.Business;
using LaundryIroningEntity.Contract;
using LaundryIroningEntity.Entity;
using LaundryIroningEntity.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
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
        [ActionName("GetUserDetailsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserByIdAsync(Guid customerId)
        {
            return Ok(await _userBusiness.GetUserByIdAsync(customerId));
        }

        [HttpGet]
        [ActionName("GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsersAsync()
        {
            return Ok(await _userBusiness.GetUsersAsync());
        }

        /// <summary>
        /// Return the admin,agent,operator users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetAdminAgentOperatorUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAdminAgentOperatorUsersAsync()
        {
            List<string> userType = new List<string>();
            userType.Add(UserTypesConstants.Admin);
            userType.Add(UserTypesConstants.Agent);
            userType.Add(UserTypesConstants.Operator);
            return Ok(await _userBusiness.GetAdminAgentOperatorUsersAsync(userType));
        }

        [HttpGet]
        [ActionName("GetAgentUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAgentUsersAsync()
        {
            List<string> userType = new List<string>();
            userType.Add(UserTypesConstants.Agent);
            return Ok(await _userBusiness.GetAgentUsersAsync(userType));
        }

        [HttpGet]
        [ActionName("GetAllOrdersForCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllOrdersForCustomerAsync(Guid customerId,int noOfDays)
        {
            return Ok(await _userBusiness.GetAllOrdersForCustomerAsync(customerId,noOfDays));
        }


        /// <summary>
        /// check username is exists 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("CheckUserName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserNameAsync(string userName)
        {
            return Ok(await _userBusiness.GetUserNameAsync(userName));
        }

        
        [HttpPost]
        [ActionName("CheckSecurityAnswers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CheckSecurityAnswersAsync(
            [FromBody, SwaggerParameter("Model containing the details of answer", Required = true)] SecurityAnswerModelViewModel answerModel)
        {
            return Ok(await _userBusiness.CheckSecurityAnswersAsync(answerModel));
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

        /// <summary>
        /// Return the user if login details match else return blank user
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetUserDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserDetailsAsync(
          [FromBody, SwaggerParameter("Model containing the details of the User to check", Required = true)] Login login)
        {
            return Ok(await _userBusiness.GetUserDetailsAsync(login));
        }


        /// <summary>
        /// Add the admin or agent users
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("AddAdminAgentUsers")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAdminAgentUsersAsync(
            [FromBody, SwaggerParameter("Model containing the details of the new User to create", Required = true)] AdminAgentUserViewModel users)
        {
            int result = await _userBusiness.AddAdminAgentUsersAsync(users);
            return commonMethods.GetResultMessages(result, MethodType.Add);
        }


        /// <summary>
        /// update admin or agent users
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("UpdateAdminAgentUsers")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAdminAgentUsersAsync(
            [FromBody, SwaggerParameter("Model containing the details of the User to update", Required = true)] AdminAgentUserViewModel users)
        {
            int result = await _userBusiness.UpdateAdminAgentUsersAsync(users);
            return commonMethods.GetResultMessages(result, MethodType.Update);
        }

        [HttpPost]
        [ActionName("UpdateUserPassword")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUserPasswordAsync(string newPass ,string conPass,string mob)
        {
            int result = await _userBusiness.UpdateUserPasswordAsync(newPass, conPass, mob);
            return commonMethods.GetResultMessages(result, MethodType.Update);
        }


        /// <summary>
        /// Update the customer profile
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("UpdateCustomerProfile")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCustomerProfileAsync(
            [FromBody, SwaggerParameter("Model containing the details of the User to update", Required = true)] CustomerProfileViewModel users)
        {
            int result = await _userBusiness.UpdateCustomerProfileAsync(users);
            return commonMethods.GetResultMessages(result, MethodType.Update);
        }
        #endregion

        #region "Delete Methods"


        /// <summary>
        /// delete admin or agent users
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("DeleteAdminAgentUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAdminAgentUsers(
            [SwaggerParameter("ID of users to be deleted", Required = true)] Guid userId)
        {
            
            int result = await _userBusiness.DeleteAdminAgentUsers(userId);
            return commonMethods.GetResultMessages(result, MethodType.Delete);
        }

        #endregion
    }
}
