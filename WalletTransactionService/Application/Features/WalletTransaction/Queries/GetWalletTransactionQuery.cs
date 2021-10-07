using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.Data.Models;
using API.Data.Persistence;
using MediatR;

namespace API.Application.Features.WalletTransaction.Queries
{
    public class GetWalletTransactionQuery : IRequest<BaseResponse<IEnumerable<Data.Persistence.Entities.WalletTransaction>>>
    {

    }
    public class GetWalletTransactionQueryHandler : IRequestHandler<GetWalletTransactionQuery, BaseResponse<IEnumerable<Data.Persistence.Entities.WalletTransaction>>>
    {
        private readonly WalletServiceContext _walletServiceContext;
        public GetWalletTransactionQueryHandler(WalletServiceContext walletServiceContext)
        {
            _walletServiceContext = walletServiceContext;
        }
        public Task<BaseResponse<IEnumerable<Data.Persistence.Entities.WalletTransaction>>> Handle(GetWalletTransactionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return Task.FromResult(new BaseResponse<IEnumerable<Data.Persistence.Entities.WalletTransaction>>(true, "Transaction retrieved", _walletServiceContext.WalletTransactions));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new BaseResponse<IEnumerable<Data.Persistence.Entities.WalletTransaction>>(false, "Transaction not retrieved"));
            }
        }
    }
}
