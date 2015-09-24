using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MemoryManagement {
    internal class Memory {
        public int Size { get; private set; }
        public Dictionary<long, long> Frames { get; private set; }
        private SortedSet<long> EmptyPositions;

        public Memory(int size) {
            Size = size;
            Frames = new Dictionary<long, long>();
            for (int i = 0; i < Size; i++) {
                EmptyPositions.Add(i);
            }
        }

        public bool IsInMemory(long Frame) {
            return Frames.ContainsKey(Frame);
        }

        public void RemovePage(long Frame) {
            EmptyPositions.Add(Frames[Frame]);
            Frames.Remove(Frame);
        }

        public void LoadFrame(long Frame) {
            Frames.Add(Frame, EmptyPositions.First());
            EmptyPositions.Remove(EmptyPositions.First());
        }

        public void SwapFrame(long FrameA, long FrameB) {
            long value = Frames[FrameA];
            Frames.Remove(FrameA);
            Frames.Add(FrameB, value);
        }

        public bool IsFull() {
            return Frames.Count == Size;
        }
    }
}
