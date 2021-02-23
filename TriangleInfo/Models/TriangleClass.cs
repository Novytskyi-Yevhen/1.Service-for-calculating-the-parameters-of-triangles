using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TriangleInfo.Controllers
{
    public class TriangleClass
    {
        public int side1 { get; set; }
        public int side2 { get; set; }
        public int side3 { get; set; }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is TriangleClass second)
                return this.side1 == second.side1 && this.side2 == second.side2 && this.side3==second.side3;

            return this.GetHashCode() == obj.GetHashCode();    
        }
        public static bool operator ==(TriangleClass a, TriangleClass b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TriangleClass a, TriangleClass b)
        {
            return !(a == b);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(side1,side2,side3);  
        }
    }
}
