using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGame
{
    public class Deck<T>
    {
        public string Name { get; private set; }
        public int Size => Cards.Count;
        public string Description { get; private set; }
        public Stack<T> Cards { get; set; }

        public Deck(string name, string description, Stack<T> cards)
        {
            Name = name;
            Description = description;
            Cards = cards;
        }

        public T Draw()
        {
            if (Cards.Count == 0) throw new Exception("Deck is empty.");
            return Cards.Pop();
        }
    }
}
