using System;
using System.Collections.Generic;

namespace Kore.Code.Memory
{
    public class ArrayObjectAllocator<T> where T : new()
    {
        private readonly List<T> freeMemory;
        private readonly T[] memory;
        private readonly Dictionary<T, bool> memoryMap;

        public ArrayObjectAllocator(uint size)
        {
            if (size == 0)
                throw new Exception("size cannot be 0");

            memory = new T[size];
            freeMemory = new List<T>();
            memoryMap = new Dictionary<T, bool>();

            for (var i = 0; i < size; ++i)
            {
                memory[i] = new T();
                freeMemory.Add(memory[i]);
                memoryMap.Add(memory[i], false);
            }
        }

        public int FreeMemory
        {
            get { return freeMemory.Count; }
        }

        public T Allocate()
        {
            if (freeMemory.Count == 0)
                throw new OutOfMemoryException();

            var memoryObject = freeMemory[0];
            freeMemory.RemoveAt(0);
            memoryMap[memoryObject] = true;

            return memoryObject;
        }

        public void Release(T memoryObject)
        {
            if (!memoryMap.ContainsKey(memoryObject))
                throw new Exception("the allocator does not contain provided memoryObject");

            if (!memoryMap[memoryObject])
                throw new Exception("memoryObject already free");

            memoryMap[memoryObject] = false;
            freeMemory.Add(memoryObject);
        }
    }
}