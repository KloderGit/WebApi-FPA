using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAmoCRM.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Controllers.CallBack.Models;
using WebApiBusinessLogic.Logics.CallBack;
using WebApiBusinessLogic.Logics.CallBack.Models;

namespace WebApi.Controllers.CallBack
{
    [Produces("application/json")]
    [Route("CallBack")]
    public class CallBackController : Controller
    {
        TypeAdapterConfig mapper;
        ILogger logger;
        IDataManager crm;

        CallBackLogic logic;

        public CallBackController(TypeAdapterConfig mapper, IDataManager crm, ILoggerFactory loggerFactory)
        {
            this.mapper = mapper;
            this.crm = crm;
            this.logger = loggerFactory.CreateLogger(this.ToString());

            this.logic = new CallBackLogic(mapper, crm, loggerFactory);
        }


        [HttpPost]
        [Route("request")]
        public async Task<IActionResult> GetModel([FromBody] CallBackViewModel model)
        {
            logger.LogError("Получена форма на запрос Перезвонить {@Model}", model);

            var res = await logic.CreateRecallTask(model.Adapt<CallBackDTO>());

            if (!res) return StatusCode(500);

            return Ok();
        }
    }
}