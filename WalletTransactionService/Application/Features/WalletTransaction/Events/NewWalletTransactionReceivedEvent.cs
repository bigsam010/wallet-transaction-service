using System.Threading;
using System.Threading.Tasks;
using API.Data.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace API.Application.Features.WalletTransaction.Events
{
    public class NewWalletTransactionReceivedEvent : CryptoWalletTransaction, INotification
    {
        public NewWalletTransactionReceivedEvent()
        {
        }
    }
    public class ProcessWalletTransactionReceivedEvent : INotificationHandler<NewWalletTransactionReceivedEvent>
    {
        private readonly ILogger<ProcessWalletTransactionReceivedEvent> _logger;
        public ProcessWalletTransactionReceivedEvent(ILogger<ProcessWalletTransactionReceivedEvent> logger)
        {
            _logger = logger;
        }
        public async Task Handle(NewWalletTransactionReceivedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("New wallet transaction received");
        }
    }
}
