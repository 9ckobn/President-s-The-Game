using Buildings;
using Core;
using EffectSystem;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Data
{
    public class CharacterData
    {
        private bool isPlayer;

        private Dictionary<TypeAttribute, int> attributes = new Dictionary<TypeAttribute, int>();
        private Building[] myBuildings;

        private List<Effect> temporaryEffect = new List<Effect>();

        public CharacterData(Dictionary<TypeAttribute, int> attributes, Building[] buildings, bool isPlayer)
        {
            this.attributes = attributes;
            myBuildings = buildings;
            this.isPlayer = isPlayer;

            RedrawData();
        }

        #region ATTRIBUTES

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

            CountMorality();
            RedrawData();
        }

        public void DownAttribute(TypeAttribute type, int value)
        {
            attributes[type] -= value;

            CountMorality();
            RedrawData();
        }

        private void CountMorality()
        {
            if(attributes[TypeAttribute.Morality] <= 0)
            {
                BoxController.GetController<LogController>().LogError($"Character DEATH. Need logic death!");
            }
        }

        private void RedrawData()
        {
            AttributeTextData[] data = new AttributeTextData[5];

            data[0] = new AttributeTextData($"{TypeAttribute.Attack} - {attributes[TypeAttribute.Attack]}", Color.green);
            data[1] = new AttributeTextData($"{TypeAttribute.Defend} - {attributes[TypeAttribute.Defend]}", Color.green);
            data[2] = new AttributeTextData($"{TypeAttribute.Luck} - {attributes[TypeAttribute.Luck]}", Color.green);
            data[3] = new AttributeTextData($"{TypeAttribute.Diplomacy} - {attributes[TypeAttribute.Diplomacy]}", Color.green);
            data[4] = new AttributeTextData($"{TypeAttribute.Morality} - {attributes[TypeAttribute.Morality]}", Color.green);

            if (isPlayer)
            {
                UIManager.Instance.GetWindow<AttributesCharactersWindow>().RedrawPlayerData(data);
            }
            else
            {
                UIManager.Instance.GetWindow<AttributesCharactersWindow>().RedrawEnemyData(data);
            }
        }

        #endregion

        #region CERD_EFFECTS

        public void AddTemporaryEffect(Effect effect)
        {
            // TODO: check if effect alredy add are update duration this effect

            temporaryEffect.Add(effect);
        }

        public List<Effect> GetDefendEffects()
        {
            List<Effect> defendEffects = new List<Effect>();

            foreach (var effect in temporaryEffect)
            {
                if(effect is DefendEffect || effect is RandomDefendEffect)
                {
                    defendEffects.Add(effect);
                }
            }

            return defendEffects;
        }

        #endregion

        #region VISUAL_EFFECTS

        public void ShowDamage(TypeAttribute typeBuilding)
        {
            foreach (var building in myBuildings)
            {
                if (building.GetTypeBuilding == typeBuilding)
                {
                    building.ShowGetDamage();
                }
            }
        }

        public void ShowHealth(TypeAttribute typeBuilding)
        {
            foreach (var building in myBuildings)
            {
                if (building.GetTypeBuilding == typeBuilding)
                {
                    building.ShowGetHealth();
                }
            }
        }

        public void ShowDefend(TypeAttribute typeBuilding)
        {
            foreach (var building in myBuildings)
            {
                if (building.GetTypeBuilding == typeBuilding)
                {
                    building.ShowGetDefend();
                }
            }
        }

        #endregion
    }
}