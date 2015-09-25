using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryManagement {
    class Program {
        static void Main(string[] args) {
            var a = new[] {-1, -1, -1, -1, -1, -1, -1, 1, 1, 1, 1, 1};
            Console.WriteLine(a.Where(x => x != -1));
            
            Console.ReadKey();
        }
    }
}
