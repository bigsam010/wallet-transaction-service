using System;
using System.Diagnostics;
using System.Threading.Tasks;
using API.Application.Features.WalletTransaction.Commands;
using API.Application.Features.WalletTransaction.Events;
using API.Data.Models;
using API.Providers.CryptoQuery;
using MediatR;
using Microsoft.Extensions.Logging;

namespace API.Application.Features.WalletTransaction.Services
{
    public class UpdateTransactionConsumer : IUpdateTransactionConsumer<UpdateTransactionMessage>
    {
        private readonly ILogger<UpdateTransactionConsumer> _logger;
        private readonly ICryptoQuery _cryptoQuery;
        private IPublisher _mediatrPublisher;
        private readonly ISender _mediatrSender;


        public UpdateTransactionConsumer(ILogger<UpdateTransactionConsumer> logger, ICryptoQuery cryptoQuery, IPublisher mediatrPublisher, ISender mediatrSender)
        {
            _logger = logger;
            _cryptoQuery = cryptoQuery;
            _mediatrPublisher = mediatrPublisher;
            _mediatrSender = mediatrSender;
        }
        public async Task Consume(UpdateTransactionMessage message)
        {
            _logger.LogInformation("New update transaction message received");
            var cryptoQueryResponse = await _cryptoQuery.GetWalletTransactions(message);
            _ = await _mediatrSender.Send(new UpdateTransactionLogCommand
            {
                ClientId = message.ClientId,
                Currency = message.Currency,
                HashedRefrence = message.HashedRefrence,
                WalletAddress = message.WalletAddress,
                IsSuccessful = cryptoQueryResponse.IsSuccessful

            });

            if (cryptoQueryResponse.IsSuccessful)
            {
                _logger.LogInformation("CryptoQuery API call sucessful. Initiating db save operation for recieved transactions");
                var newWalletTransactions = await _mediatrSender.Send(new AddWalletTransactionCommand { Transactions = cryptoQueryResponse.Data });
                if (newWalletTransactions.Data.Count > 0)
                {
                    _logger.LogInformation("Initiating new transaction event publishing for new transactions");
                }

                foreach (var walletTransaction in newWalletTransactions.Data)
                {
                    await _mediatrPublisher.Publish(new NewWalletTransactionReceivedEvent()
                    {
                        Amount = walletTransaction.Amount,
                        ClientId = walletTransaction.ClientId,
                        Ref = walletTransaction.Ref,
                        Status = walletTransaction.Status,
                        TransactionDate = walletTransaction.TransactionDate,
                        Type = walletTransaction.Type,
                        WalletAddress = walletTransaction.WalletAddress
                    });
                }
            }
            else
            {
                _logger.LogError($"CryptoQuery API call failed. Request Details: ClietId-{message.ClientId}, WalletAddress-{message.WalletAddress}, HashedRef-{message.HashedRefrence}. Reason-{cryptoQueryResponse.Message}");
            }

        }
    }

    public interface IUpdateTransactionConsumer<T>
    {
        public Task Consume(T message);
    }
}