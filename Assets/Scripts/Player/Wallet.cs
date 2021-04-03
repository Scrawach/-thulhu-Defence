using System;
using UnityEngine;

namespace Player
{
    public class Wallet
    {
        public int Money { get; private set; }

        public event Action<int> Changed;

        public void Add(int value)
        {
            Money += value;
            Changed?.Invoke(Money);
        }

        public void Take(int value)
        {
            Money -= value;
            Changed?.Invoke(Money);
        }

        public bool HasMoreThan(int value) =>
            Money >= value;
    }
}
