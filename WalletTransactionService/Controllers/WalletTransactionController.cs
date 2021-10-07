using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using API.Application.Features.WalletTransaction.Queries;
using API.Data.Models;
using API.Data.Persistence.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WalletTransactionService.Controllers
{
    [ApiController]
    [Route("[controller]/v1")]
    public class WalletTransactionController : ControllerBase
    {
        private readonly ISender _mediatrSender;
        private readonly ILogger<WalletTransactionController> _logger;

        public WalletTransactionController(ILogger<WalletTransactionController> logger, ISender mediatrSender)
        {
            _logger = logger;
            _mediatrSender = mediatrSender;
        }

        [ProducesResponseType(typeof(BaseResponse<IEnumerable<WalletTransaction>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.BadRequest)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediatrSender.Send(new GetWalletTransactionQuery());
            return result.Status ? Ok(result) : BadRequest(result);
        }
    }
}
