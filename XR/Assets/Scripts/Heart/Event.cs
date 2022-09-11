using System;
using UnityEngine;

namespace Api
{
    public class Event
    {
        public string Name { get; }

        public DateTime Timestamp { get; }

        public int Value { get; }

        public Event(string name, int value)
        {
            Name = name;
            Timestamp = DateTime.Now;
            Value = value;
        }
    }
}

