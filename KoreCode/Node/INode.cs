using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoreCode.Node
{
    public interface INode
    {
        //TODO: Key becomes generic
        //TODO: implements IEnumerable, to traversals can be reused - not only for trees, but also for graphs
        int Key { get; set; }
    }
}
