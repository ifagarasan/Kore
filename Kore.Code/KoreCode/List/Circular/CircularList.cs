using System;
using System.Collections;
using KoreCode.List.Linear;

namespace KoreCode.List.Circular
{
    public class CircularList<T>: LinkedList<T> where T : IComparable
    {
        DoubleLinkedListItem<T> sentinel;

        public CircularList()
        {
            sentinel = new DoubleLinkedListItem<T>();
            sentinel.Prev = sentinel;
            sentinel.Next = sentinel;
        }

        protected override void Add(ListItem<T> listItem)
        {
            Interlace(listItem, sentinel);
        }

        protected override void Insert(ListItem<T> listItem, int index)
        {
            InsertListItem(listItem, index);
        }

        public override IEnumerator GetEnumerator()
        {
            ListItem<T> listItem = sentinel.Next;
            for (int i = 0; i < Count; ++i)
            {
                yield return listItem;
                listItem = listItem.Next;
            }
        }

        protected void InsertListItem(ListItem<T> listItem, int index)
        {
            Interlace(listItem, GetListItemByIndex(index));
        }

        private void Interlace(ListItem<T> listItem, ListItem<T> targetListItem)
        {
            listItem.Prev = targetListItem.Prev;
            targetListItem.Prev.Next = listItem;
            targetListItem.Prev = listItem;
            listItem.Next = targetListItem;
        }

        protected override ListItem<T> RetrieveListItem(int index)
        {
            ListItem<T> listItem = sentinel;

            for (int i = 0; i <= index; ++i)
                listItem = listItem.Next;

            return listItem;
        }

        protected override void RemoveListItemAt(int index)
        {
            ListItem<T> targetListItem = GetListItemByIndex(index);
            targetListItem.Prev.Next = targetListItem.Next;
            targetListItem.Next.Prev = targetListItem.Prev;
        }

        protected override ListItem<T> CreateListItem(T value)
        {
            return new DoubleLinkedListItem<T>(value);
        }
    }
}
