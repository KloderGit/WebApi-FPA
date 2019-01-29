using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Controllers.CallBack.Models;

namespace WebApi.Controllers.CallBack
{
    [Produces("application/json")]
    [Route("CallBack")]
    public class CallBackController : Controller
    {
        ILogger logger;

        public CallBackController(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger(this.ToString());
        }


        [HttpPost]
        [Route("request")]
        public async Task<IActionResult> GetModel([FromBody] CallBackViewModel model)
        {
            logger.LogError("Получена форма на запрос Перезвонить {@Model}", model);

            return Ok();
        }
    }
}