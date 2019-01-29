using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers.CallBack.Models
{
    public class CallBackViewModel
    {
        public string UserName { get; set; }
        public string UserPhone { get; set; }
        public string ProgramType { get; set; }
        public string ProgramTitle { get; set; }
        public string Url { get; set; }
    }
}
