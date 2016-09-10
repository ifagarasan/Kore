namespace Kore.Input.Keys
{
    public class Key
    {
        public Key(char symbol)
        {
            Symbol = symbol.ToString();
        }

        public string Symbol { get; }

        public override bool Equals(object obj) => obj is Key && Equals((Key)obj);

        protected bool Equals(Key other) => string.Equals(Symbol, other.Symbol);

        public override int GetHashCode() => Symbol?.GetHashCode() ?? 0;
    }
}