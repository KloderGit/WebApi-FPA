using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAmoCRM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebApiBusinessLogic;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Models")]
    public class ModelsController : Controller
    {
        BusinessLogic logic;
        ILogger logger;

        public ModelsController(ILogger logger, BusinessLogic logic)
        {
            this.logger = logger;
            this.logic = logic;
        }

        [Route("status/{status?}")]
        public async Task<IEnumerable<string>> Get(int status)
        {
            // 17769208

            var leads = await logic.GetLeadsByStatus(status);

            return leads.ToList().Where(i => i.IsDeleted != true).Select(i => i.Name);
        }

        public string Get()
        {
            return "value";
        }

    }
}
