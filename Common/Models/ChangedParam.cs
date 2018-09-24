﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Common.Models.AmoCrm.Listener
{
    public class ChangedParam
    {
        public string @Event { get; set; }
        public string CurrentValue { get; set; }
        public string OldValue { get; set; }
    }
}
