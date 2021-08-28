using LaundryIroningAPI.CommonMethod;
using LaundryIroningContract.Business;
using LaundryIroningEntity.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using LaundryIroningEntity.Entity;
using LaundryIroningCommon;

namespace LaundryIroningAPI.IroningLaundry
{
    [Route("api/[controller]/[action]")]
    public class IroningLaundryController : Controller
    {
        #region Private Veriables

        private readonly IIroningLaundryBusiness _ironingLaundryBusiness;
        CommonMethods commonMethods;
        #endregion

        #region Constructor
        public IroningLaundryController(IUnitOfWork uow, IIroningLaundryBusiness ironingLaundryBusiness)
        {
            _ironingLaundryBusiness = ironingLaundryBusiness;
            _ironingLaundryBusiness.Uow = uow;
            commonMethods = new CommonMethods();
        }
        #endregion

        #region Get Methods

        [HttpGet]
        [ActionName("GetAllIroningLaundryOrders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllIroningLaundryOrderAsync()
        {
            return Ok(await _ironingLaundryBusiness.GetAllIroningLaundryOrderAsync());
        }

        [HttpGet]
        [ActionName("GetOrderById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetIroningLaundryOrderAsync(int orderId)
        {
            return Ok(await _ironingLaundryBusiness.GetIroningLaundryOrderAsync(orderId));
        }

        [HttpGet]
        [ActionName("GetIroningLaundryOrdersForAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetIroningLaundryOrdersForAdminAsync()
        {
            return Ok(await _ironingLaundryBusiness.GetIroningLaundryOrdersForAdminAsync());
        }

        [HttpGet]
        [ActionName("CheckPromoCodeValid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> IsPromoCodeValidAsync(string promoCode)
        {
            return Ok(await _ironingLaundryBusiness.IsPromoCodeValidAsync(promoCode));
        }
        #endregion

        #region Post Methods

        [HttpPost]
        [ActionName("AddIroningLaundryOrder")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddIroningLaundryOrderAsync(
           [FromBody, SwaggerParameter("Model containing the details of the new order to create", Required = true)] IroningLaundryOrder order)
        {
            return Ok(await _ironingLaundryBusiness.AddIroningLaundryOrderAsync(order));
        }

        #endregion
    }
}
