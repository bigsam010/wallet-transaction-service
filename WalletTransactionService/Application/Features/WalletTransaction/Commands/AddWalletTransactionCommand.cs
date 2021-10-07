using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Data.Enums;
using API.Data.Models;
using API.Data.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace API.Application.Features.WalletTransaction.Commands
{
    public class AddWalletTransactionCommand : IRequest<BaseResponse<List<CryptoWalletTransaction>>>
    {
        public List<CryptoWalletTransaction> Transactions;
    }
    public class AddWalletTransactionCommandHandler : IRequestHandler<AddWalletTransactionCommand, BaseResponse<List<CryptoWalletTransaction>>>
    {
        private readonly WalletServiceContext _walletServiceContext;
        private readonly ILogger<AddWalletTransactionCommandHandler> _logger;
        private readonly IMapper _mapper;

        public AddWalletTransactionCommandHandler(WalletServiceContext walletServiceContext, ILogger<AddWalletTransactionCommandHandler> logger, IMapper mapper)
        {
            _walletServiceContext = walletServiceContext;
            _logger = logger;
            _mapper = mapper;
        }
        public Task<BaseResponse<List<CryptoWalletTransaction>>> Handle(AddWalletTransactionCommand request, CancellationToken cancellationToken)
        {

            var duplicateFound = request.Transactions.RemoveAll(x => _walletServiceContext.WalletTransactions.Any(y => y.ClientId == x.ClientId && y.Ref == x.Ref));
            _walletServiceContext.WalletTransactions.AddRange(_mapper.Map<Data.Persistence.Entities.WalletTransaction>(request.Transactions));
            _walletServiceContext.SaveChanges();
            if (duplicateFound > 0)
            {
                _logger.LogInformation($"Ignored {duplicateFound} duplicate wallet transaction(s)  for wallet owned by client-{request.Transactions.FirstOrDefault()?.ClientId}");
            }
            return Task.FromResult(new BaseResponse<List<CryptoWalletTransaction>>(true, "Wallet transaction saved", request.Transactions));
        }
    }
}
