using System;

namespace MemoryManagement {
    class Program {
        public static void Main() {
            var os = new AditionalBitsSimulator(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)+@"\swim.trace", 4);
            os.Simulate();
            
            
            Console.ReadKey();
        }

        
    }
}
