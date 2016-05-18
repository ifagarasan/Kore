using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoreCode.Node
{
    public interface INode<T> : IEnumerable<T>
    {
        //TODO: Key becomes generic
        int Key { get; set; }
        string Label { get; }
    }
}
