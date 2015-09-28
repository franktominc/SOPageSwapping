using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace MemoryManagement {
    class OptimalSimulator {
        private List<int> framesList;
        public int pageFaults { get; set; }
        public Memory memory { get; private set; }
        public OptimalSimulator(string Path) {
            framesList = new List<int>();
            var frames = File.ReadAllLines(Path);
            foreach (var frame in frames) {
                var frameToConvert = frame.Split(null)[0].Substring(0,5);
                var frameAsInt = Convert.ToInt32(frameToConvert, 16);
               
                framesList.Add(frameAsInt);
            }
            pageFaults = 0;
        }

        public void Simulate() {
            while (framesList.Count != 0) {
                if (!memory.IsFull()) {
                    memory.LoadFrame(framesList.First());
                }
                else {
                    memory.SwapFrame(FindVictim(), framesList.First());
                    pageFaults++;
                }
                framesList.Remove(framesList.First());
            }
        }

        private int FindVictim() {
            var maxIndex = int.MinValue;
            var frameToSwap = 0;
            foreach (var frame in memory.Frames) {
                var k = framesList.IndexOf(frame);
                if (k <= maxIndex) continue;
                maxIndex = k;
                frameToSwap = frame;
            }
            return frameToSwap;
        }

    }
}
