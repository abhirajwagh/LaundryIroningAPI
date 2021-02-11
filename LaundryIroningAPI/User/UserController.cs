using LaundryIroningAPI.CommonMethod;
using LaundryIroningCommon;
using LaundryIroningContract.Business;
using LaundryIroningEntity.Contract;
using LaundryIroningEntity.Entity;
using LaundryIroningEntity.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
        #endregion
    }
}
