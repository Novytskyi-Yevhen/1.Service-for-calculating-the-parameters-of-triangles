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
            return this.GetHashCode() == obj.GetHashCode();    
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(side1,side2,side3);  
        }
    }
}
