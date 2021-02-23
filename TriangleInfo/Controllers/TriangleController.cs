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

        public string Area(double side1, double side2, double side3)
        {
            return $"{BufferArea(side1, side2, side3)}";
        }

        public string IsRightAngled(double side1, double side2, double side3)
        {
            return (side2 * side2 == (side1 * side1 + side3 * side3)).ToString();
        }
        public string IsIsosceles(double side1, double side2, double side3)
        {
            return (side1 == side3).ToString();
        }
        public string Perimeter(int side1, int side2, int side3)
        {
            return $"{BufferPerimeter(side1, side2, side3)}";
        }

        [NonAction]
        public double BufferArea(double side1, double side2, double side3)
        {
            double p = (side1 + side2 + side3) * 0.5;
            return Math.Round(Math.Sqrt(p * ((p - side1) * (p - side2) * (p - side3))), 4);
        }
        [NonAction]
        public int BufferPerimeter(int side1, int side2, int side3)
        {
            return side1 + side2 + side3;
        }

        public string Arecongruent(TriangleClass tr1, TriangleClass tr2)
        {
            return $"";

        }
        public string Info(int side1, int side2, int side3)
        {
            double perimeter = BufferPerimeter(side1, side2, side3);
            double areaTriangle = BufferArea(side1, side2, side3);
            return "------------------------------------------------------------" + "\n" +
                "Triangle:" + "\n" +
                $"({side1}, {side2}, {side3})" + "\n" +
                "Reduced:" + "\n" +
                $"({side1 / perimeter}, {side2 / perimeter}, {side3 / perimeter})" + "\n" +
                $"Area = {areaTriangle}" + "\n" +
                $"Perimeter = {perimeter}" + "\n" +
                "------------------------------------------------------------";
        }
        public string IsEquilateral(int side1, int side2, int side3)
        {
            return (side1 == side2 && side2 == side3).ToString();
        }
        [NonAction]
        public string IsValidTriangle(int side1, int side2, int side3)
        {
            return ((side1 + side2) > side3 && (side1 + side3) > side2 && (side2+side3)>side1).ToString();
            
        }
    }
}
