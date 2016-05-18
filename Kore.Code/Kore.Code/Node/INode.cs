using System.Collections.Generic;

namespace Kore.Code.Node
{
    public interface INode<T> : IEnumerable<T>
    {
        //TODO: Key becomes generic
        int Key { get; set; }
        string Label { get; }
    }
}