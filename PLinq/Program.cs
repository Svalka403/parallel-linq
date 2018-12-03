using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            ListGenerator.ShowList(ListGenerator.ReadCsv());
            Console.ReadKey();
        }
    }
}
