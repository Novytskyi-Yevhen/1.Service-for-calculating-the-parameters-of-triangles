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
        [NonAction]
        public bool IsValidTriangle(int side1, int side2, int side3)
        {
            return (side1 + side2) > side3 && (side1 + side3) > side2 && (side2 + side3) > side1;
        }
        [NonAction]
        public double BufferArea(int side1, int side2, int side3)
        {
            double p = (side1 + side2 + side3) * 0.5;
            return Math.Round(Math.Sqrt(p * ((p - side1) * (p - side2) * (p - side3))), 4);
        }
        [NonAction]
        public int BufferPerimeter(int side1, int side2, int side3)
        {
            return side1 + side2 + side3;
        }
        public ActionResult<string> Info(int side1, int side2, int side3)
        {
            if (IsValidTriangle(side1, side2, side3))
            {
                double perimeter = BufferPerimeter(side1, side2, side3);
                double areaTriangle = BufferArea(side1, side2, side3);
                return
                "------------------------------------------------------------" + "\n" +
                    "Triangle:" + "\n" +
                    $"({side1}, {side2}, {side3})" + "\n" +
                    "Reduced:" + "\n" +
                    $"({side1 / perimeter}, {side2 / perimeter}, {side3 / perimeter})" + "\n" +
                    $"Area = {areaTriangle}" + "\n" +
                    $"Perimeter = {perimeter}" + "\n" +
                    "------------------------------------------------------------";
            }
            return new RedirectToActionResult("Error", "Home", "");
        }
        public IActionResult Area(int side1, int side2, int side3)
        {
            if (IsValidTriangle(side1, side2, side3))
                return Ok($"{BufferArea(side1, side2, side3)}");
            return new RedirectToActionResult("Error", "Home", "");
        }
        public IActionResult Perimeter(int side1, int side2, int side3)
        {
            if (IsValidTriangle(side1, side2, side3))
                return Ok($"{BufferPerimeter(side1, side2, side3)}");
            return new RedirectToActionResult("Error", "Home", "");
        }
        public IActionResult IsRightAngled(int side1, int side2, int side3)
        {
            if (IsValidTriangle(side1, side2, side3))
                return Ok(side2 * side2 == (side1 * side1 + side3 * side3));
            return new RedirectToActionResult("Error", "Home", "");
        }
        public IActionResult IsEquilateral(int side1, int side2, int side3)
        {
            if (IsValidTriangle(side1, side2, side3))
                return Ok(side1 == side2 && side2 == side3);
            return new RedirectToActionResult("Error", "Home", "");
        }
        public IActionResult IsIsosceles(int side1, int side2, int side3)
        {
            if (IsValidTriangle(side1, side2, side3))
                return Ok(side1 == side3);
            return new RedirectToActionResult("Error", "Home", "");
        }
        public IActionResult Arecongruent(TriangleClass tr1, TriangleClass tr2)
        {
            if (IsValidTriangle(tr1.side1, tr1.side2, tr1.side3) && IsValidTriangle(tr2.side1, tr2.side2, tr2.side3))
            {
                double[] array1 = new double[] { tr1.side1, tr1.side2, tr1.side3 }.OrderBy(x => x).ToArray();
                double[] array2 = new double[] { tr2.side1, tr2.side2, tr2.side3 }.OrderBy(x => x).ToArray();
                return Ok($"{array1.SequenceEqual(array2)}");
            }
            return new RedirectToActionResult("Error", "Home", "");
        }
        public IActionResult AreSimilar(TriangleClass tr1, TriangleClass tr2)
        {
            if (IsValidTriangle(tr1.side1, tr1.side2, tr1.side3) && IsValidTriangle(tr2.side1, tr2.side2, tr2.side3))
            {
                var array1 = new double[] { tr1.side1, tr1.side2, tr1.side3 }.OrderBy(x => x).ToArray();
                var array2 = new double[] { tr2.side1, tr2.side2, tr2.side3 }.OrderBy(x => x).ToArray();
                double sideOne = array1[0] / array2[0];
                double sideTwo = array1[1] / array2[1];
                double sideThree = array1[2] / array2[2];
                return Ok(sideOne == sideTwo && sideTwo == sideThree);
            }
            return new RedirectToActionResult("Error", "Home", "");
        }
        public ActionResult<string> GreatesByPerimeter(TriangleClass[] tr)
        {
            foreach (TriangleClass triangle in tr)
            {
                if (!IsValidTriangle(triangle.side1, triangle.side2, triangle.side3))
                    return new RedirectToActionResult("Error", "Home", "");
            }
            List<int> perimeterTriangles = tr.Select(item => BufferPerimeter(item.side1, item.side2, item.side3)).ToList();
            int index = perimeterTriangles.IndexOf(perimeterTriangles.Max());
            return Info(tr[index].side1, tr[index].side2, tr[index].side3);
        }
        public ActionResult<string> GreatestByArea(TriangleClass[] tr)
        {
            foreach (TriangleClass triangle in tr)
            {
                if (!IsValidTriangle(triangle.side1, triangle.side2, triangle.side3))
                    return new RedirectToActionResult("Error", "Home", "");
            }
            List<double> areaTriangles = tr.Select(item => BufferArea(item.side1, item.side2, item.side3)).ToList();
            int index = areaTriangles.IndexOf(areaTriangles.Max());
            return Info(tr[index].side1, tr[index].side2, tr[index].side3);
        }
        public ActionResult<string> Infos(TriangleClass tr)
        {
            if (IsValidTriangle(tr.side1, tr.side2, tr.side3))
                return Info(tr.side1, tr.side2, tr.side3);
            return new RedirectToActionResult("Error", "Home", "");
        }
        public ActionResult<string> PairwiseNonSimilar(TriangleClass[] tr)
        {
            foreach (TriangleClass triangle in tr)
            {
                if (!IsValidTriangle(triangle.side1, triangle.side2, triangle.side3))
                    return new RedirectToActionResult("Error", "Home", "");
            }
            List<TriangleClass> listPairwiseNonSimilarTriangle = new List<TriangleClass>();
            string result = "";
            listPairwiseNonSimilarTriangle = tr.GroupBy(s => s).Where(g => g.Count() == 1).Select(g => g.Key).ToList();
            foreach (var item in listPairwiseNonSimilarTriangle)
            {
                result += Infos(item).Value;
                result += "\n" + "****************" + "\n";
            }
            return result;
        }
    }
}
