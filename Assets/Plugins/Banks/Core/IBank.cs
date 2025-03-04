﻿using UniRx;

namespace Plugins.Banks.Core
{
    public interface IBank<T>
    {
        public IReadOnlyReactiveProperty<T> Amount { get; }

        public IReadOnlyReactiveProperty<bool> IsEmpty { get; }

        public void Add(T value);

        public bool Spend(T value);

        public void SetValue(T value);

        public void Clear();

        public bool HasEnough(T value);
    }
}