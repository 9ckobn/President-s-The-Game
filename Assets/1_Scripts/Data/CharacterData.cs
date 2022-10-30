using Buildings;
using Core;
using EffectSystem;
using System.Collections.Generic;

namespace Data
{
    public class CharacterData
    {
        private Dictionary<TypeAttribute, int> attributes = new Dictionary<TypeAttribute, int>();
        private Building[] myBuildings;

        public CharacterData(Building[] buildings)
        {
            myBuildings = buildings;
        }

        public void AddAttribute(TypeAttribute attribute, int value)
        {
            attributes.Add(attribute, value);
        }

        public int GetValueAttribute(TypeAttribute attribute)
        {
            if (attributes.ContainsKey(attribute))
            {
                return attributes[attribute];
            }
            else
            {
                BoxController.GetController<LogController>().LogError($"Not have attribute {attribute}");
                return 0;
            }
        }

        public void UpAttribute(TypeAttribute type, int value)
        {
            attributes[type] += value;

            foreach (var building in myBuildings)
            {
                if(building.GetTypeBuilding == type)
                {
                    building.GetHealth();
                }
            }
        }

        public void DownAttribute(TypeAttribute type, int value)
        {
            attributes[type] -= value;

            foreach (var building in myBuildings)
            {
                if (building.GetTypeBuilding == type)
                {
                    building.GetDamage();
                }
            }
        }
    }
}