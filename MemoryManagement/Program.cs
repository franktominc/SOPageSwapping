using System;

namespace MemoryManagement {
    class Program {
        public static void Main() {
            var os = new OptimalSimulator(@"c:\Users\Frank\Desktop\Test1.txt", 5);
            os.Simulate();
            
            
            Console.ReadKey();
        }

        
    }
}
