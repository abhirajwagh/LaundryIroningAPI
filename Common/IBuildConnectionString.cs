namespace LaundryIroningCommon
{
    /// <summary>
    /// Implement the insterface for build connection string
    /// </summary>
    public interface IBuildConnectionString
    {
        /// <summary>
        /// Prepared Connection stirng 
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        string PreparedConnection();
    }
}
