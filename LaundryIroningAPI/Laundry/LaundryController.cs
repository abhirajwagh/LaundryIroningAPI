using LaundryIroningAPI.CommonMethod;
using LaundryIroningContract.Business;
using LaundryIroningEntity.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using LaundryIroningEntity.Entity;
using LaundryIroningCommon;

namespace LaundryIroningAPI.Laundry
{
    [Route("api/[controller]/[action]")]
    public class LaundryController : Controller
    {
        #region Private Veriables

        private readonly ILaundryBusiness _iLaundryBusiness;
        CommonMethods commonMethods;
        #endregion

        #region Constructor
        public LaundryController(IUnitOfWork uow, ILaundryBusiness iLaundryBusiness)
        {
            _iLaundryBusiness = iLaundryBusiness;
            _iLaundryBusiness.Uow = uow;
            commonMethods = new CommonMethods();
        }
        #endregion

        #region Get Methods

        [HttpGet]
        [ActionName("GetAllLaundryOrders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllLaundryOrderAsync()
        {
            return Ok(await _iLaundryBusiness.GetAllLaundryOrderAsync());
        }

        [HttpGet]
        [ActionName("GetOrderById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLaundryOrderAsync(int orderId)
        {
            return Ok(await _iLaundryBusiness.GetLaundryOrderAsync(orderId));
        }
        #endregion

        #region Post Methods

        [HttpPost]
        [ActionName("AddLaundryOrder")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddLaundryOrderAsync(
           [FromBody, SwaggerParameter("Model containing the details of the new order to create", Required = true)] LaundryOrder order)
        {
            return Ok(await _iLaundryBusiness.AddLaundryOrderAsync(order));
        }

        #endregion
    }
}
