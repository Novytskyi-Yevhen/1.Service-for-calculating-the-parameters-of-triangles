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
        public string GreatestByArea(TriangleClass[] tr)
        {
            List<double> areaTriangles = tr.Select(item => BufferArea(item.side1, item.side2, item.side3)).ToList();
            int index = areaTriangles.IndexOf(areaTriangles.Max());
            return Info(tr[index].side1, tr[index].side2, tr[index].side3);
        }
        public string AreSimilar(TriangleClass tr1, TriangleClass tr2)
        {
            var array1 = new double[] { tr1.side1, tr1.side2, tr1.side3 }.OrderBy(x => x).ToArray();
            var array2 = new double[] { tr2.side1, tr2.side2, tr2.side3 }.OrderBy(x => x).ToArray();
            double sideOne = array1[0] / array2[0];
            double sideTwo = array1[1] / array2[1];
            double sideThree = array1[2] / array2[2];
            return (sideOne == sideTwo && sideTwo == sideThree).ToString();
        }
    }
}
