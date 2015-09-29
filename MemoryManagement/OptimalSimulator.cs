using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MemoryManagement {
    class OptimalSimulator {
        private readonly List<int> _framesList;
        private int PageFaults { get; set; }
        private int PageHits { get; set; }
        private Memory Memory { get; set; }
        public OptimalSimulator(string path, int memSize) {
            Memory = new Memory(memSize);
            _framesList = new List<int>();
            var frames = File.ReadAllLines(path);
            foreach (var frame in frames) {
                var frameToConvert = frame.Split(null)[0].Substring(0,5);
                var frameAsInt = Convert.ToInt32(frameToConvert, 16);
               
                _framesList.Add(frameAsInt);
            }
            PageFaults = PageHits = 0;
        }

        public void Simulate() {
            while (_framesList.Count != 0) {
                if (!Memory.IsInMemory(_framesList.First())) {
                    if (!Memory.IsFull()) {
                        Console.WriteLine("Allocated Frame: {0}", _framesList.First());
                        Memory.LoadFrame(_framesList.First());
                        PageFaults++;
                    }
                    else {
                        Console.WriteLine("Victim Page: {0}", FindVictim());
                        Console.WriteLine("Victim Page was swapped for: {0}", _framesList.First());
                        Memory.SwapFrame(FindVictim(), _framesList.First());
                        PageFaults++;
                    }
                }
                else {
                    Console.WriteLine("Hit Page {0}", _framesList.First());
                    PageHits++;
                }
                _framesList.Remove(_framesList.First());
            }
            Console.WriteLine("Page Faults: {0}", PageFaults);
            Console.WriteLine("Page Hits: {0}", PageHits);

        }

        private int FindVictim() {
            var maxIndex = int.MinValue;
            var frameToSwap = 0;
            foreach (var frame in Memory.Frames) {
                var k = _framesList.IndexOf(frame);
                if (k == -1) k = int.MaxValue;
                if (k <= maxIndex) continue;
                maxIndex = k;
                frameToSwap = frame;
            }
            return frameToSwap;
        }

    }
}
