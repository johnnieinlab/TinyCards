using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyCards.Core.Service;
using TinyCards.Core.Model;
using TinyCards.Core.Interface;
using TinyCards.Api.Types;
using TinyCards.Core.Constant;

namespace TinyCards.Api.Controllers
{
    [Route("card")]
    [ApiController]
    public class CardControler : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly ILogger<CardControler> _logger;

        public CardControler(ICardService cardService, ILogger<CardControler> logger)
        {
            _logger = logger;
            _cardService = cardService;

        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register (
            [FromBody] RegisterCardRequest request)
        {
            var result = await _cardService.RegisterAsync(request.CardNumber, request.initialAmount);
            if (result.Code != ResultCode.Success)
            {
                return StatusCode((int)result.Code, result.ErrorMessage);
            }
            return Ok(result);
        }


        [HttpPost]
        [Route("Authorize")]
        public async Task<IActionResult> Authorize(
            [FromBody] AuthorizeCardRequest request)
        {
            var result = await _cardService.AuthorizeAsync(request.CardNumber, request.amount, request.type, request.description);
            if (result.Code != ResultCode.Success)
            {
                return StatusCode((int)result.Code, result.ErrorMessage);
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("Charge")]
        public async Task<IActionResult> Charge(
        [FromBody] AuthorizeCardRequest request)
        {
            var result = await _cardService.ChargeAsync(request.CardNumber, request.amount);
            if (result.Code != ResultCode.Success)
            {
                return StatusCode((int)result.Code, result.ErrorMessage);
            }
            return Ok(result);
        }

    }
}
