using System.Collections.Generic;

namespace Cards.Data
{
    public class DeckData
    {
        private const int MAX_PRESIDENTS = 3;
        private const int MAX_FIGHTS = 9;

        public List<string> PresidentsId { get; private set; }
        public List<string> FightsId { get; private set; }

        public int Id { get; private set; }
        public string Name { get; private set; }

        public DeckData(int id, string name, List<string> presidents, List<string> fights)
        {
            Id = id;
            Name = name;

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
        }

        public bool CanAddPresidentData()
        {
            return PresidentsId.Count < MAX_PRESIDENTS;
        }

        public bool CanAddFightData()
        {
            return FightsId.Count < MAX_FIGHTS;
        }

        public void AddPresidentCard(string idCards)
        {
            PresidentsId.Add(idCards);
        }

        public void AddFightCard(string idCards)
        {
            FightsId.Add(idCards);
        }

        public void RemovePresidentCard(string idCards)
        {
            PresidentsId.Remove(idCards);
        }

        public void RemoveFightCard(string idCards)
        {
            FightsId.Remove(idCards);
        }
    }
}