using System;

namespace MemoryManagement {
    class Program {
        public static void Main() {
            var frame = "07b243a0";
            var frameToConvert = frame.Split(null)[0].Substring(0, 5);
            var frameAsInt = Convert.ToInt32(frameToConvert,16);
            Console.WriteLine(frameAsInt);
            
            Console.ReadKey();
        }

        
    }
}
