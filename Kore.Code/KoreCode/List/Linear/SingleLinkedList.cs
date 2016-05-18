using System;

namespace KoreCode.List.Linear
{
    public class SingleLinkedList<T> : LinearLinkedList<T> where T : IComparable
    {
        protected override void AddListItem(ListItem<T> listItem)
        {
            tail.Next = listItem;
        }

        protected override void InsertListItem(ListItem<T> listItem, int index)
        {
            var targetListItem = GetListItemByIndex(index - 1);
            listItem.Next = targetListItem.Next;
            targetListItem.Next = listItem;
        }

        protected override ListItem<T> CreateListItem(T value)
        {
            return new SingleLinkedListItem<T>(value);
        }

        protected override void RemoveListItemAt(int index)
        {
            if (index == 0)
                head = head.Next;
            else
            {
                var targetListItem = GetListItemByIndex(index - 1);
                targetListItem.Next = targetListItem.Next.Next;
            }
        }
    }
}