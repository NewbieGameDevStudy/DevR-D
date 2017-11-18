using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Annotations;

namespace ConsoleApp1
{
    public class Test
    {
        int Method(int a, int b)
        {
            return a* b;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<int> dd = new List<int>();
            dd.Add(10);

            List<int> ddd = dd;
            dd.Clear();
            dd = null;

            Console.WriteLine(ddd?.Count);
            int a = 0;

        }
    }
}
