using System;
using System.Collections.Generic;
using System.Text;

namespace LaundryIroningCommon
{

    public class Constants
    {
        public const string defaultLanguageCode = "en-US";
    }
    public class MessageConstants
    {
        public const string UnauthorizedAccess = "UnauthorizedAccess";
        public const string UnauthorizedAccessForEdit = "UnauthorizedAccessForEdit";
        public const string InvalidAccessToken = "InvalidAccessToken";
        public const string InternalServerError = "InternalServerError";
        public const string ValidToken = "\"success\"";

        #region Global Messages
        public const string RecordAlreadyExist = "Record already exist.";
        public const string StringNullData = "null";
        public const string InvalidInputData = "Invalid input data";

        public const string RecordAddedSuccessfully = "Record added successfully";
        public const string RecordDeletedSuccessfully = "Record deleted successfully";
        public const string RecordUpdatedSuccessfully = "Record updated successfully";


        public const string NoRecordForUpdation = "No Record exist for updation";
        public const string RecordAddedorUpdatedSuccessfully = "Record added/updated successfully";
        public const string NoRecordForDeletion = "No Record exist for deletion";
        public const string MappedSuccesfully = "Mapped succesfully";
        public const string NoRecordForUpdate = "No Record exist for update";

        public const string Msg_ErrorDelete = "Record not deleted";
        public const string Msg_ErrorInsert = "Record not inserted";
        public const string Msg_ErrorUpdate = "record not updated";

        public const string BadRequest = "Bad request";

        public const string FileUploadedSuccessfully = "File uploaded sucessfully";
        public const string ErrorInFileUpload = "Error in file upload";
        public const string InvalidFileProvided = "Invalid file provided";
        #endregion
    }
    public class MethodType
    {
        public const string Get = "Get";
        public const string Add = "Add";
        public const string Update = "Update";
        public const string Delete = "Delete";
            
    }
   
    public class ProcedureConstants
    {
        public const string GetIroningOrderDetailsById = "GetIroningOrderDetailsById";

    }

    public class UrlConstants
    {
       

    }



}
