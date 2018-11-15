using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Interfaces;
using LibraryAmoCRM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using WebApiBusinessLogic;
using WebApiBusinessLogic.Models.Crm;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Models")]
    public class ModelsController : Controller
    {
        BusinessLogic logic;
        ILoggerService logger;
        IMemoryCache cache;

        public ModelsController(ILoggerService logger, BusinessLogic logic, IMemoryCache memoryCache)
        {
            this.logger = logger;
            this.logic = logic;
            this.cache = memoryCache;
        }

        [ResponseCache( Location = ResponseCacheLocation.Client, Duration = 3600 )]
        [Route("programs")]
        public IEnumerable<ProgramList> GetPrograms()
        {
            var result = cache.GetOrCreate( "Programs", entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromHours( 20 );

                return logic.GetProgramsListForAmo();
            } );

            return result;
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
