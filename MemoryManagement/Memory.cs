using System.Collections.Generic;
using System.Linq;

namespace MemoryManagement {
    internal class Memory {
        private int Size { get; set; }
        private int[] Frames { get; set; }
        private readonly SortedSet<long> _emptyPositions;

        public Memory(int size) {
            Size = size;
            Frames = new int[Size];
            _emptyPositions = new SortedSet<long>();
            for (var i = 0; i < Size; i++) {
                _emptyPositions.Add(i);
            }
        }

        public bool IsInMemory(int frame) {
            for (var i = 0; i < Size; i++) {
                if (Frames[i] == frame)
                    return true;
            }
            return false;
        }

        public long WhereIs(int frame) {
            for (var i = 0; i < Size; i++) {
                if (Frames[i] == frame)
                    return i;
            }
            return -1;
        }

        public void RemovePage(int frame) {
            for (var i = 0; i < Size; i++) {
                if (Frames[i] != frame) continue;
                Frames[i] = -1;
                _emptyPositions.Add(i);
                break;
            }
        }

        public void LoadFrame(int frame) {
            Frames[_emptyPositions.First()] =  frame;
            _emptyPositions.Remove(_emptyPositions.First());
        }

        public void SwapFrame(int frameA, int frameB) {
            Frames[WhereIs(frameA)] = frameB;
        }

        public bool IsFull() {
            return Frames.Where(x=> x!=-1).Count() == Size;
        }
    }
}
