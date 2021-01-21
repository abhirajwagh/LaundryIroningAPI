using LaundryIroningCommon;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LaundryIroningAPI.CommonMethod
{
    public class CommonMethods : Controller
    {
        public IActionResult GetResultMessages(int result, string Method)
        {
            if (Method == MethodType.Get)
            {
                switch (result)
                {
                    case (int)LaundryIroningHelper.Enum.StatusCode.InternalServerError:
                        return BadRequest(HttpStatusCode.InternalServerError);
                    default:
                        return BadRequest(EntityResourceInformation.GetResValue("Error_GetFailed"));
                }
            }
            if (Method == MethodType.Add)
            {
                switch (result)
                {
                    case (int)LaundryIroningHelper.Enum.StatusCode.SuccessfulStatusCode:
                        return Ok(EntityResourceInformation.GetResValue("Success_RecordAddedSuccessfully"));
                    case (int)LaundryIroningHelper.Enum.StatusCode.ConflictStatusCode:
                        return BadRequest(EntityResourceInformation.GetResValue("Error_RecordAlreadyExists"));
                    case (int)LaundryIroningHelper.Enum.StatusCode.InternalServerError:
                        return BadRequest(HttpStatusCode.InternalServerError);
                    case (int)LaundryIroningHelper.Enum.StatusCode.ExpectationFailed:
                        return Ok(EntityResourceInformation.GetResValue("Error_ModelNull"));
                    default:
                        return BadRequest(EntityResourceInformation.GetResValue("Error_AddFailed"));
                }
            }
            if (Method == MethodType.Update)
            {
                switch (result)
                {
                    case (int)LaundryIroningHelper.Enum.StatusCode.SuccessfulStatusCode:
                        return Ok(EntityResourceInformation.GetResValue("Success_RecordsUpdatedSuccessfully"));
                    case (int)LaundryIroningHelper.Enum.StatusCode.ExpectationFailed:
                        return Ok(EntityResourceInformation.GetResValue("Error_ModelValidationFailed"));
                    case (int)LaundryIroningHelper.Enum.StatusCode.NotAllowed:
                        return Ok(EntityResourceInformation.GetResValue("Error_UpdateNotAllowed"));
                    case (int)LaundryIroningHelper.Enum.StatusCode.NotAcceptable:
                        return BadRequest(EntityResourceInformation.GetResValue("Error_DuplicateDataFound"));
                    case (int)LaundryIroningHelper.Enum.StatusCode.NotFound:
                        return BadRequest(EntityResourceInformation.GetResValue("Success_NoRecordsToUpdate"));
                    case (int)LaundryIroningHelper.Enum.StatusCode.InternalServerError:
                        return BadRequest(HttpStatusCode.InternalServerError);
                    default:
                        return BadRequest(EntityResourceInformation.GetResValue("Error_UpdatesFailed"));
                }
            }
            if (Method == MethodType.Delete)
            {
                switch (result)
                {
                    case (int)LaundryIroningHelper.Enum.StatusCode.NotAllowed:
                        return BadRequest(EntityResourceInformation.GetResValue("Error_DeleteNotAllowedForThisRecord"));
                    case (int)LaundryIroningHelper.Enum.StatusCode.SuccessfulStatusCode:
                        return Ok(EntityResourceInformation.GetResValue("Success_RecoedDeletedSuccessfully"));
                    case (int)LaundryIroningHelper.Enum.StatusCode.NotFound:
                        return BadRequest(EntityResourceInformation.GetResValue("Success_NoRecordToDelete"));
                    case (int)LaundryIroningHelper.Enum.StatusCode.InternalServerError:
                        return BadRequest(HttpStatusCode.InternalServerError);
                    default:
                        return BadRequest(EntityResourceInformation.GetResValue("Error_DeleteFailed"));
                }
            }

            return Ok("");
        }
    }
}
