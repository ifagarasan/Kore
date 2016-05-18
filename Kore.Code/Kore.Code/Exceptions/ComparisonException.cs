using System;

namespace Kore.Code.Exceptions
{
    public class ComparisonException : Exception
    {
        public ComparisonException()
        {
        }

        public ComparisonException(string input1, string input2, string relation) :
            base("expected '" + input1 + "' to be '" + relation + "' '" + input2 + "'")
        {
        }
    }
}