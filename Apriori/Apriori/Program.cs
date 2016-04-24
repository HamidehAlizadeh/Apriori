using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apriori
{
    class Program
    {

         public static List<string> filecsv(string filepath)
            {

                var column2 = new List<string>();
                using (var rd = new StreamReader(filepath))
                {
                    while (!rd.EndOfStream)
                    {
                        var splits = rd.ReadLine().Split(',');
                        column2.Add(splits[1]);
                    }
                }
                List<string>source = column2.ToList<string>();
                return source;
            }
        
        static void Main(string[] args)
        {

            Console.WriteLine("address ra vared konid:");
            string path = Console.ReadLine();
            List<string> listt = new List<string>();
            listt = filecsv(path);
            Console.WriteLine("minsuport ra vared konid:");
            int minsup = Int32.Parse(Console.ReadLine());
            List<string> frequent = new List<string>();
            Itemset ii = new Itemset();
            frequent=ii.patern(listt, minsup,listt);
            foreach (string s in frequent)
                Console.WriteLine(s);

            Console.ReadKey();

        }
    }
}
