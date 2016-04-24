using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apriori
{
    public abstract class Lists
    {
        public static List<Itemset> sets;

        static Lists()
        {
            sets = new List<Itemset>();
        }
    }
}
