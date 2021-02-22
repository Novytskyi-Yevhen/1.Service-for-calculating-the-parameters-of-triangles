using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TriangleInfo.Models;
using System;

namespace TriangleInfo.Controllers
{
    public class TriangleController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public TriangleController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public string Info(int side1, int side2, int side3)
        {
            double perimeter = side1 + side2 + side3;
            double semiPerimeter = perimeter / 2;
            double areaTriangle = Math.Sqrt(semiPerimeter * (semiPerimeter - side1) * (semiPerimeter - side2) * (semiPerimeter - side3));
            return "------------------------------------------------------------" + "\n" +
                "Triangle:" + "\n" +
                $"({side1}, {side2}, {side3})" + "\n" +
                "Reduced:" + "\n" +
                $"({side1 / perimeter}, {side2 / perimeter}, {side3 / perimeter})" + "\n" +
                $"Area = {areaTriangle}" + "\n" +
                $"Perimeter = {perimeter}" + "\n" +
                "------------------------------------------------------------";
        }
       
    }
}
