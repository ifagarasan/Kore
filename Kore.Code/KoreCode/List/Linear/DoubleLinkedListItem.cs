using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoreCode.List
{
    public class DoubleLinkedListItem<T>: ListItem<T> 
    {
        public DoubleLinkedListItem() { }

        public DoubleLinkedListItem(T value): base(value) { }

        public override ListItem<T> Next { get; set; }
        public override ListItem<T> Prev { get; set; }
    }
}
