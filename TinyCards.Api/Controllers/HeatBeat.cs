using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyCards.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HeartBeat : ControllerBase
    {

        private readonly ILogger<HeartBeat> _logger;

        public HeartBeat(ILogger<HeartBeat> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(DateTime.Now);
        }
    }
}
