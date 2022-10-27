using UnityEngine;

namespace Cards.Data
{
    public class CardDataBase
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public Sprite Sprite { get; private set; }

        public CardDataBase(string id, Sprite sprite, string name)
        {
            Id = id;
            Sprite = sprite;
            Name = name;
        }
    }
}