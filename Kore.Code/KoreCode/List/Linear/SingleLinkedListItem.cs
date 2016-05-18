using System;

namespace KoreCode.List.Linear
{
    public class SingleLinkedListItem<T>: ListItem<T> 
    {
        public SingleLinkedListItem() { }

        public SingleLinkedListItem(T value): base(value) { }

        public override ListItem<T> Next { get; set; }

        public override ListItem<T> Prev
        {
            get
            {
                throw new NotSupportedException("Prev is not supported by SingleLinkedListItem");
            }
            set
            {
                throw new NotSupportedException("Prev is not supported by SingleLinkedListItem");
            }
        }
    }
}
