using System;

namespace Player
{
    public class Score
    {
        public int Value { get; private set; }
        public int TargetValue { get; }

        public event Action<int> Changed;

        public Score(int target)
        {
            TargetValue = target;
        }

        public void Add(int value)
        {
            Value += value;
            Changed?.Invoke(Value);
        }

        public void Take(int value)
        {
            Value -= value;
            Changed?.Invoke(Value);
        }

        public bool HasMoreThan(int value) =>
            Value >= value;
    }
}