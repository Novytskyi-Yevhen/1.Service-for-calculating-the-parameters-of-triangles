using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TriangleInfo.Controllers
{
    public class TriangleController : Controller
    {
        [NonAction]
        public bool IsValidTriangle(int side1, int side2, int side3)
        {
            return (side1 + side2) > side3 && (side1 + side3) > side2 && (side2 + side3) > side1;
        }
        [NonAction]
        public double GetArea(int side1, int side2, int side3)
        {
            double p = (side1 + side2 + side3) * 0.5;
            return Math.Round(Math.Sqrt(p * ((p - side1) * (p - side2) * (p - side3))), 4);
        }
        [NonAction]
        public int GetPerimeter(int side1, int side2, int side3)
        {
            return side1 + side2 + side3;
        }
        [NonAction]
        public ActionResult<string> Infos(TriangleClass tr)
        {
            if (IsValidTriangle(tr.side1, tr.side2, tr.side3))
                return Info(tr.side1, tr.side2, tr.side3);
            return new RedirectToActionResult("Error", "Home", "");
        }
        [NonAction]
        public bool GetAreSimilar(TriangleClass tr1, TriangleClass tr2)
        {
            var array1 = new double[] { tr1.side1, tr1.side2, tr1.side3 }.OrderBy(x => x).ToArray();
            var array2 = new double[] { tr2.side1, tr2.side2, tr2.side3 }.OrderBy(x => x).ToArray();
            double sideOne = array1[0] / array2[0];
            double sideTwo = array1[1] / array2[1];
            double sideThree = array1[2] / array2[2];
            return sideOne == sideTwo && sideTwo == sideThree;
        }


        public ActionResult<string> Info(int side1, int side2, int side3)
        {
            if (IsValidTriangle(side1, side2, side3))
            {
                double perimeter = GetPerimeter(side1, side2, side3);
                double areaTriangle = GetArea(side1, side2, side3);
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
                return Ok($"{GetArea(side1, side2, side3)}");
            return new RedirectToActionResult("Error", "Home", "");
        }
        public IActionResult Perimeter(int side1, int side2, int side3)
        {
            if (IsValidTriangle(side1, side2, side3))
                return Ok($"{GetPerimeter(side1, side2, side3)}");
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
                return Ok(GetAreSimilar(tr1, tr2));
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
            List<int> perimeterTriangles = tr.Select(item => GetPerimeter(item.side1, item.side2, item.side3)).ToList();
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
            List<double> areaTriangles = tr.Select(item => GetArea(item.side1, item.side2, item.side3)).ToList();
            int index = areaTriangles.IndexOf(areaTriangles.Max());
            return Info(tr[index].side1, tr[index].side2, tr[index].side3);
        }


        public ActionResult<string> PairwiseNonSimilar(TriangleClass[] tr)
        {
            foreach (TriangleClass triangle in tr)
            {
                if (!IsValidTriangle(triangle.side1, triangle.side2, triangle.side3))
                    return new RedirectToActionResult("Error", "Home", "");
            }
            string result = "";
            List<TriangleClass> listPairwiseNonSimilarTriangle = GetListPairwiseNonSimilar(tr);
            foreach (var item in listPairwiseNonSimilarTriangle)
            {
                if (item == listPairwiseNonSimilarTriangle.First())
                    result += "*********************** New triangle ***********************" + "\n";
                else
                    result += "\n" + "*********************** New triangle ***********************" + "\n";
                result += Infos(item).Value;
            }
            return result;
        }
        [NonAction]
        public List<TriangleClass> GetListPairwiseNonSimilar(TriangleClass[] triangles)
        {
            List<TriangleClass> listPairwiseNonSimilarTriangle = new List<TriangleClass>();
            foreach (var item in triangles)
            {
                bool isUnique = true;
                for (int i = 0; i < triangles.Length; i++)
                {
                    if (i == Array.IndexOf(triangles, item))
                        continue;
                    if (GetAreSimilar(item, triangles[i]))
                    {
                        isUnique = false;
                        break;
                    }
                }
                if (isUnique)
                    listPairwiseNonSimilarTriangle.Add(item);
            }
            return listPairwiseNonSimilarTriangle;
        }

    }
}
