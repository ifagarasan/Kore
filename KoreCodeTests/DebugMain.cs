using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KoreCodeTests.List;

namespace KoreCodeTests
{
    public static class DebugMain
    {
        public static void Main(string[] args)
        {
            CircularLinkedListFunctional circular = new CircularLinkedListFunctional();

            circular.SetUp();
            circular.AddAppendsItemAtEnd();
        }
    }
}
