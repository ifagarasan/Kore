using System;
using System.Collections;

namespace KoreCode.List.Linear
{
    public abstract class LinearLinkedList<T> : LinkedList<T>, IEnumerable where T : IComparable
    {
        protected ListItem<T> head, tail;

        public override IEnumerator GetEnumerator()
        {
            var listItem = head;
            for (var i = 0; i < Count; ++i)
            {
                yield return listItem;
                listItem = listItem.Next;
            }
        }

        protected override void Add(ListItem<T> listItem)
        {
            if (head == null)
                head = listItem;
            else
                AddListItem(listItem);

            tail = listItem;
        }

        protected override void Insert(ListItem<T> listItem, int index)
        {
            if (Count == 0)
                head = listItem;
            else if (index == 0)
            {
                listItem.Next = head;
                head = listItem;
            }
            else
                InsertListItem(listItem, index);
        }

        protected override ListItem<T> RetrieveListItem(int index)
        {
            if (head == null)
                throw new NullReferenceException("head is null when Count is " + Count);

            var listItem = head;
            for (var i = 0; i < index; ++i)
                listItem = listItem.Next;

            return listItem;
        }

        protected abstract void AddListItem(ListItem<T> listItem);
        protected abstract void InsertListItem(ListItem<T> listItem, int index);
    }
}