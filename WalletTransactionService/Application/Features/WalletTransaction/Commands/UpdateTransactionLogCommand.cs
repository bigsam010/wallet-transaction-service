
using System;
using System.Threading;
using System.Threading.Tasks;
using API.Data.Enums;
using API.Data.Models;
using API.Data.Persistence;
using API.Data.Persistence.Entities;
using MediatR;

namespace API.Application.Features.WalletTransaction.Commands
{
    public class UpdateTransactionLogCommand : IRequest<BaseResponse>
    {
        public Guid ClientId { set; get; }
        public string HashedRefrence { set; get; }
        public string WalletAddress { set; get; }
        public CryptoCurrency Currency { set; get; }
        public bool IsSuccessful { set; get; }
    }

    public class UpdateTransactionLogCommandHandler : IRequestHandler<UpdateTransactionLogCommand, BaseResponse>
    {
        private readonly WalletServiceContext _walletServiceContext;
        public UpdateTransactionLogCommandHandler(WalletServiceContext walletServiceContext)
        {
            _walletServiceContext = walletServiceContext;
        }
        public Task<BaseResponse> Handle(UpdateTransactionLogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _walletServiceContext.UpdateTransactionLogs.Add(new UpdateTransactionLog
                {
                    Id = Guid.NewGuid(),
                    ClientId = request.ClientId,
                    Currency = request.Currency,
                    HashedRefrence = request.HashedRefrence,
                    IsSuccessful = request.IsSuccessful,
                    RequestDate = DateTime.UtcNow,
                    WalletAddress = request.WalletAddress

                });
                _walletServiceContext.SaveChanges();
                return Task.FromResult(new BaseResponse(true, "Update transaction successfully logged to the Db"));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new BaseResponse(false, $"An error occured while logging transaction update. Error message-{ex.Message}"));
            }
        }
    }
}
