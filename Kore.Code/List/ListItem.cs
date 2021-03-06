﻿namespace Kore.Code.List
{
    public abstract class ListItem<T>
    {
        public ListItem()
        {
        }

        public ListItem(T value)
        {
            Value = value;
        }

        public T Value { get; set; }

        public abstract ListItem<T> Next { get; set; }
        public abstract ListItem<T> Prev { get; set; }
    }
}