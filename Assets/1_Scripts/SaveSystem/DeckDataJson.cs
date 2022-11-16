using System;
using System.Collections.Generic;

namespace SaveSystem
{
    [Serializable]
    public class DeckDataJson
    {
        public int Id;
        public string NameDeck;
        public bool IsComplete;
        public bool IsSelected;
        public List<string> IdPresidentCards;
        public List<string> IdFightCards;
    }
}