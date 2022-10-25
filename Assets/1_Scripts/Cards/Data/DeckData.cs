using System.Collections.Generic;
using UnityEngine;

namespace Cards.Data
{
    public class DeckData
    {
        private const int MAX_PRESIDENTS = 3;
        private const int MAX_FIGHTS = 9;

        public List<CardPresidentData> PresidentsData { get; private set; }
        public List<CardFightData> FightsData { get; private set; }

        public int Id { get; private set; }
        public string Name { get; private set; }

        public DeckData(int id, List<CardPresidentData> presidents, List<CardFightData> fights)
        {
            Id = id;
            Name = id.ToString();

            if (presidents == null)
            {
                PresidentsData = new List<CardPresidentData>();
            }
            else
            {
                PresidentsData = presidents;
            }

            if (fights == null)
            {
                FightsData = new List<CardFightData>();
            }
            else
            {
                FightsData = fights;
            }
        }

        public bool CanAddPresidentData()
        {
            return PresidentsData.Count < MAX_PRESIDENTS;
        }

        public bool CanAddFightData()
        {
            return FightsData.Count < MAX_FIGHTS;
        }

        public void AddPresidentCard(CardPresidentData data)
        {
            PresidentsData.Add(data);
        }

        public void AddFightCard(CardFightData data)
        {
            FightsData.Add(data);
        }

        public void RemovePresidentCard(CardPresidentData data)
        {
            PresidentsData.Remove(data);
        }

        public void RemoveFightCard(CardFightData data)
        {
            FightsData.Remove(data);
        }
    }
}