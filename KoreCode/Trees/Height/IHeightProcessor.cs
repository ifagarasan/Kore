using KoreCode.Trees.Binary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoreCode.Trees.Height
{
    public interface IHeightProcessor
    {
        int GetHeight(IBinaryNode node);
        void Clear();
    }
}
