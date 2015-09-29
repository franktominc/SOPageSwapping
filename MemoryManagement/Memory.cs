using System.Collections.Generic;
using System.Linq;

namespace MemoryManagement {
    internal class Memory {
        private int Size { get; set; }
        public int[] Frames { get; set; }
        private readonly SortedSet<int> _emptyPositions;

        public Memory(int size) {
            Size = size;
            Frames = new int[Size];
            _emptyPositions = new SortedSet<int>();
            for (var i = 0; i < Size; i++) {
                _emptyPositions.Add(i);
            }
            for (int i = 0; i < Frames.Length; i++) {
                Frames[i] = -1;
            }
        }

        public bool IsInMemory(int frame) {
            for (var i = 0; i < Size; i++) {
                if (Frames[i] == frame)
                    return true;
            }
            return false;
        }

        public int WhereIs(int frame) {
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

        public int LoadFrame(int frame) {
            int where = _emptyPositions.First();
            Frames[where] =  frame;
            _emptyPositions.Remove(where);
            return where;
        }

        public void SwapFrame(int frameA, int frameB) {
            Frames[WhereIs(frameA)] = frameB;
        }

        public bool IsFull() {
            return Frames.Where(x=> x!=-1).Count() == Size;
        }

        public int FrameAt(int position) {
            return Frames[position];
        }
    }
}
