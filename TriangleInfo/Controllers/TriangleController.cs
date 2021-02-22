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
        private readonly ILogger<TriangleController> _logger;

        public TriangleController(ILogger<TriangleController> logger)
        {
            _logger = logger;
        }

        public string Ret(int id)
        {
            return $"{id}";
        }
        public string Area(double side1, double side2, double side3)
        {
            double p = (side1 + side2 + side3) * 0.5;
            double s = Math.Round(Math.Sqrt(p * ((p - side1) * (p - side2) * (p - side3))), 4);
            return $"{s}";
        }
        public string IsRightAngled(double side1, double side2, double side3)
        {
            return (side2 * side2 == (side1 * side1 + side3 * side3)).ToString();
        }
    }
}
