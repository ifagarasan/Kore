using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KoreCode.Exceptions;

namespace KoreCode.List
{
    public abstract class LinkedList<T>: IList<T>, IEnumerable where T : IComparable
    {
        int count;
        public int Count
        {
            get
            {
                return count;
            }
            protected set
            {
                if (value < 0)
                    throw new IndexOutOfRangeException("Count cannot be negative");

                count = value;
            }
        }

        public virtual T this[int index]
        {
            get
            {
                return GetListItemByIndex(index).Value;
            }
            set
            {
                GetListItemByIndex(index).Value = value;
            }
        }

        public bool Contains(T value)
        {
            foreach (ListItem<T> listItem in this)
                if (listItem.Value.CompareTo(value) == 0)
                    return true;

            return false;
        }

        protected ListItem<T> GetListItemByIndex(int index)
        {
            if (Count == 0)
                throw new CollectionEmptyException();

            if (index >= Count || index < 0)
                throw new IndexOutOfRangeException("invalid value '" + index + "' for index");

            return RetrieveListItem(index);
        }

        public abstract IEnumerator GetEnumerator();
        protected abstract ListItem<T> RetrieveListItem(int index);
        protected abstract void Add(ListItem<T> value);

        public void Add(T value)
        {
            Add(CreateListItem(value));

            Count++;
        }

        protected abstract void Insert(ListItem<T> listItem, int index);

        public void Insert(T value, int index)
        {
            ListItem<T> listItem = CreateListItem(value);

            if (Count == 0)
            {
                if (index != 0)
                    throw new IndexOutOfRangeException("invalid value '" + index + "' for index");
                else
                    Add(listItem);
            }
            else
                Insert(listItem, index);

            Count++;
        }

        public void RemoveAt(int index)
        {
            if (Count == 0)
                throw new CollectionEmptyException();

            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException("invalid value '" + index + "' for index");

            RemoveListItemAt(index);

            Count--;
        }

        protected abstract void RemoveListItemAt(int index);
        protected abstract ListItem<T> CreateListItem(T value);
    }
}
