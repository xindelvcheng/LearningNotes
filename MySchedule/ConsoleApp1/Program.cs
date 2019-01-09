using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            Console.WriteLine(list.Count);
            list.RemoveAt(1);
            Console.WriteLine(list.Count);
            Console.ReadLine();
        }
    }
}
