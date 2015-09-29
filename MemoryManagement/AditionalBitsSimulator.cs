using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryManagement {
    class AditionalBitsSimulator {
        private Memory Memory;
        private char[][] bitArray;
        public int PageFaults { get; set; }
        public int PageHits { get; set; }

        private List<int> _framesList;

        public AditionalBitsSimulator(string path, int size) {
            Memory = new Memory(size);
            bitArray = new char[size][];
            for (int i = 0; i < size; i++) {
                bitArray[i] = new char[8];
                for (int j = 0; j <bitArray[i].Length; j++) {
                    bitArray[i][j] = '0';
                }
            }
            _framesList = new List<int>();
            var frames = File.ReadAllLines(path);
            foreach (var frame in frames) {
                var frameToConvert = frame.Split(null)[0].Substring(0, 5);
                var frameAsInt = Convert.ToInt32(frameToConvert, 16);

                _framesList.Add(frameAsInt);
            }
            PageFaults = PageHits = 0;
        }

        public void Simulate() {
            int numberOfAccess = 0;
            while (_framesList.Count != 0) {
                int k = _framesList.First();
                if (!Memory.IsInMemory(k)) {
                    if (!Memory.IsFull()) {
                        int x = Memory.LoadFrame(k);
                        bitArray[x][0] = '1';
                    } else {
                        int victim = findVictim();
                        Memory.SwapFrame(Memory.FrameAt(victim), k);
                        for (int i = 1; i < bitArray[victim].Length; i++) {
                            bitArray[victim][i] = '0';
                        }
                        bitArray[victim][0] = '1';
                    }
                    PageFaults++;
                }
                else {
                    bitArray[Memory.WhereIs(k)][0] = '1';
                    PageHits++;
                    numberOfAccess++;
                }
                if (numberOfAccess%100 == 0) {
                    ShiftAll();
                }
                _framesList.Remove(k);
            }
            Console.WriteLine("Page Hits: {0}", PageHits);
            Console.WriteLine("page Faults: {0}", PageFaults);
        }

        private void ShiftAll() {
            for (int i = 0; i < bitArray.Length; i++) {
                for (int j = 1; j < bitArray[i].Length; j++) {
                    bitArray[i][j] = bitArray[i][j - 1];
                }
                bitArray[i][0] = '0';
            }
        }

        private int findVictim() {
            int k = int.MaxValue;
            int frameToswap = -1;
            for (int i = 0; i < bitArray.GetLength(0); i++) {
                int n = Convert.ToInt32(new string(bitArray[i]), 10);
                if (n < k) {
                    frameToswap = i;
                    k = n;
                }
            }
            return frameToswap;
            
        }
    }
}
