using System;

namespace Kore.Random
{
    public class Generator : IGenerator
    {
        private readonly System.Random _random;

        public Generator()
        {
            _random = new System.Random(Environment.TickCount);
        }

        public uint RetrieveNonNegative(uint max)
        {
            return (uint)_random.Next((int)max + 1);
        }
    }
}