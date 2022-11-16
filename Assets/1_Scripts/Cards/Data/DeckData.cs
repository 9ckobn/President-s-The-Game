using Data;
using System.Collections.Generic;
using UnityEngine;

namespace Cards.Data
{
    public class DeckData
    {
        public List<string> PresidentsId { get; private set; }
        public List<string> FightsId { get; private set; }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public bool IsComplete { get; private set; }
        public bool IsSelected { get; set; }

        private int maxPresidents = 3;
        private int maxFights = 9;

        public DeckData(int id, string name, bool isComplete, bool isSelected, List<string> presidents, List<string> fights)
        {
            Id = id;
            Name = name;
            IsComplete = isComplete;
            IsSelected = isSelected;

            if (presidents == null)
            {
                PresidentsId = new List<string>();
            }
            else
            {
                PresidentsId = presidents;
            }

            if (fights == null)
            {
                FightsId = new List<string>();
            }
            else
            {
                FightsId = fights;
            }

            maxPresidents = MainData.MAX_PRESIDENT_CARDS;
            maxFights = MainData.MAX_FIGHT_CARDS;
        }

        public bool CanAddPresidentData()
        {
            return PresidentsId.Count < maxPresidents;
        }

        public bool CanAddFightData()
        {
            return FightsId.Count < maxFights;
        }

        public bool CanSelectedCard(CardPresidentData data)
        {
            foreach (var id in PresidentsId)
            {
                if (id == data.Id)
                {
                    return false;
                }
            }

            return true;
        }

        public bool CanSelectedCard(CardFightData data)
        {
            int countCard = 0;

            foreach (var id in FightsId)
            {
                if (id == data.Id)
                {
                    countCard++;
                }
            }

            return countCard < data.MaxCountInDeck;
        }

        public void AddPresidentCard(string idCards)
        {
            PresidentsId.Add(idCards);
            CheckIsComplete();
        }

        public void AddFightCard(string idCards)
        {
            FightsId.Add(idCards);
            CheckIsComplete();
        }

        public void RemovePresidentCard(string idCards)
        {
            PresidentsId.Remove(idCards);
            CheckIsComplete();
        }

        public void RemoveFightCard(string idCards)
        {
            FightsId.Remove(idCards);
            CheckIsComplete();
        }

        public void Rename(string name)
        {
            Name = name;
        }

        private void CheckIsComplete()
        {
            IsComplete = PresidentsId.Count == MainData.MAX_PRESIDENT_CARDS && FightsId.Count == MainData.MAX_FIGHT_CARDS;
        }
    }
}