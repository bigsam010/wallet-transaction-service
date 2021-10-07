
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data.Models;
using static API.Data.Models.BaseResponse;

namespace API.Providers.CryptoQuery
{
    public interface ICryptoQuery
    {
        public Task<ApiResponse<List<CryptoWalletTransaction>>> GetWalletTransactions(UpdateTransactionMessage requestPayload);
    }

    public class CrypoApi : ICryptoQuery
    {
        public Task<ApiResponse<List<CryptoWalletTransaction>>> GetWalletTransactions(UpdateTransactionMessage requestPayload)
        {
            return Task.FromResult(new ApiResponse<List<CryptoWalletTransaction>>
            {
                IsSuccessful = true,
                StatusCode = 200,
                Message = "Wallet transactions retrieved",
                Data = new List<CryptoWalletTransaction> { }
            });
        }
    }
}
