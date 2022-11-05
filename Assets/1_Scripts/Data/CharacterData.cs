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

        private List<AttributeData> attributes = new List<AttributeData>();
        private Building[] myBuildings;

        private List<Effect> temporaryEffect = new List<Effect>();

        public CharacterData(List<AttributeData> attributes, Building[] buildings, bool isPlayer)
        {
            this.attributes = attributes;
            myBuildings = buildings;
            this.isPlayer = isPlayer;

            RedrawData();
        }

        #region ATTRIBUTES

        public AttributeData GetAttribute(TypeAttribute type)
        {
            foreach (var attribute in attributes)
            {
                if (attribute.TypeAttribute == type)
                {
                    return attribute;
                }
            }

            BoxController.GetController<LogController>().LogError($"Not have attribute {type}");
            return null;
        }

        public int GetValueAttribute(TypeAttribute type)
        {
            return GetAttribute(type).Value;
        }

        public void UpAttribute(TypeAttribute type, int value)
        {
            GetAttribute(type).AddValue(value);

            CountMorality();
            RedrawData();
        }

        public void DownAttribute(TypeAttribute type, int value)
        {
            GetAttribute(type).DecreaseValue(value);

            CountMorality();
            RedrawData();
        }

        private void CountMorality()
        {
            if (GetAttribute(TypeAttribute.Morality).Value <= 0)
            {
                BoxController.GetController<LogController>().LogError($"Character DEATH. Need logic death!");
            }
        }

        private void RedrawData()
        {
            AttributeTextData[] data = new AttributeTextData[5];

            data[0] = new AttributeTextData($"{TypeAttribute.Attack} - {GetAttribute(TypeAttribute.Attack).Value}", Color.green);
            data[1] = new AttributeTextData($"{TypeAttribute.Defend} - {GetAttribute(TypeAttribute.Defend).Value}", Color.green);
            data[2] = new AttributeTextData($"{TypeAttribute.Luck} - {GetAttribute(TypeAttribute.Luck).Value}", Color.green);
            data[3] = new AttributeTextData($"{TypeAttribute.Diplomacy} - {GetAttribute(TypeAttribute.Diplomacy).Value}", Color.green);
            data[4] = new AttributeTextData($"{TypeAttribute.Morality} - {GetAttribute(TypeAttribute.Morality).Value}", Color.green);

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

        #region CARD_EFFECTS

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
                if (effect is DefendEffect)
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

        public void EndRound()
        {
            if (temporaryEffect.Count > 0)
            {
                temporaryEffect = BoxController.GetController<EffectsController>().CheckEffectsAfterEndRound(temporaryEffect);
            }
        }
    }
}