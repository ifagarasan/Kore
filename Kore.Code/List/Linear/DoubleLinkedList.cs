﻿using System;

namespace Kore.Code.List.Linear
{
    public class DoubleLinkedList<T> : LinearLinkedList<T> where T : IComparable
    {
        protected override void AddListItem(ListItem<T> listItem)
        {
            listItem.Prev = tail;
            tail.Next = listItem;
            tail = listItem;
        }

        protected override ListItem<T> CreateListItem(T value)
        {
            return new DoubleLinkedListItem<T>(value);
        }

        protected override void InsertListItem(ListItem<T> listItem, int index)
        {
            var targetListItem = GetListItemByIndex(index);
            listItem.Prev = targetListItem.Prev;
            targetListItem.Prev = listItem;
            targetListItem.Prev.Next = listItem;
        }

        protected override void RemoveListItemAt(int index)
        {
            if (index == 0)
            {
                head = head.Next;

                if (head != null)
                    head.Prev = null;
            }
            else
            {
                var targetListItem = GetListItemByIndex(index);
                targetListItem.Prev.Next = targetListItem.Next;

                if (targetListItem.Next != null)
                    targetListItem.Next.Prev = targetListItem.Prev;
            }
        }
    }
}