namespace LaundryIroningHelper.Enum
{
    public enum StatusCode
    {
        ConflictStatusCode = 409,
        SuccessfulStatusCode = 200,
        Accepted = 202,
        NoContent = 204,
        NotAllowed = 403,
        ExpectationFailed = 417,
        PartialContent = 206,
        NotFound = 404,
        InternalServerError=500,
        RecordFound=302,
        NotAcceptable = 406
    };

}
