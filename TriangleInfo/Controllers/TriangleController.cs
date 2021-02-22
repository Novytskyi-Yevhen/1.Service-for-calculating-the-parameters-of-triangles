using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TriangleInfo.Models;

namespace TriangleInfo.Controllers
{
    public class TriangleController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public TriangleController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public string Ret(int id)
        {
            return $"{id}";
        }
       
    }
}
