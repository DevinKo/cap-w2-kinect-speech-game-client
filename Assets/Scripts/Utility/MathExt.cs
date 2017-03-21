using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utility
{
    static class MathExt
    {
        public static float CalculateStdDev(IEnumerable<float> values)
        {
            double ret = 0;
            if (values.Count() > 0)
            {
                //Compute the Average      
                float avg = values.Average();
                //Perform the Sum of (value-avg)_2_2      
                double sum = values.Sum(d => Math.Pow(d - avg, 2));
                //Put it all together      
                ret = Math.Sqrt((sum) / (values.Count() - 1));
            }
            return (float)ret;
        }
    }
}
