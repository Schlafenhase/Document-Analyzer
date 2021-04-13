namespace DocumentAnalyzerAPI.A.Interfaces
{
    public interface IAzureBlobAuthService
    {
        /// <summary>
        /// Method that generates the SAS token for Azure Blob Storage authentication
        /// </summary>
        /// <param name="key">
        /// Azure account key
        /// </param>
        /// <param name="accountName">
        /// Azure account name
        /// </param>
        /// <returns>
        /// SAS key
        /// </returns>
        string GenerateSAS(string key, string accountName);
    }
}
