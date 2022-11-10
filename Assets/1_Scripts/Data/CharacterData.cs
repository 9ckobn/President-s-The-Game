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

        private Building GetBuilding(TypeAttribute typeBuilding)
        {
            foreach (var building in myBuildings)
            {
                if (building.GetTypeBuilding == typeBuilding)
                {
                    return building;
                }
            }

            BoxController.GetController<LogController>().LogError($"Not have building {typeBuilding}!");
            return null;
        }

        #region ATTRIBUTES

        public void ShowTargetAttribute(TypeAttribute type)
        {
            GetBuilding(type).EnableStateTarget();
        }

        public void HideTargetAttribute(TypeAttribute type)
        {
            GetBuilding(type).DisableStateTarget();
        }

        private AttributeData GetAttribute(TypeAttribute type)
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

        public int GetValueDefend(TypeAttribute type)
        {
            return GetAttribute(type).ValueDefend;
        }

        public bool AttributeHaveDefend(TypeAttribute type)
        {
            return GetAttribute(type).IsHaveDefend;
        }

        public bool AttributeHaveGodDefend(TypeAttribute type)
        {
            return GetAttribute(type).IsGodDefend;
        }

        public void UpAttribute(TypeAttribute type, int value)
        {
            GetAttribute(type).AddValue(value);

            CountMorality();
            RedrawData();
        }

        public void DownAttribute(TypeAttribute type, int value, bool showDamage = false)
        {
            GetAttribute(type).DecreaseValue(value);

            if (showDamage && CheckTypeBuilding(type))
            {
                GetBuilding(type).ShowDamage();
            }

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

            foreach (var attribute in attributes)
            {
                TypeAttribute type = attribute.TypeAttribute;

                if (type == TypeAttribute.Economic || type == TypeAttribute.Food || type == TypeAttribute.Medicine || type == TypeAttribute.RawMaterials)
                {
                    GetBuilding(type).ChangeStateBuilding(GetAttribute(type).GetAttributeState());
                    GetBuilding(type).ShowValueAttribute(GetAttribute(type).Value);
                }
            }
        }

        #endregion

        #region DEFEND

        public void AddDefend(TypeAttribute type, int valueDefend)
        {
            GetAttribute(type).GetDefend(valueDefend);
            GetBuilding(type).ShowGetDefend(valueDefend);
        }

        public void AddGodDefend(TypeAttribute type)
        {
            GetAttribute(type).GetGodDefend();
            GetBuilding(type).ShowGodDefend();
        }

        public void LoseGodDefend(TypeAttribute type)
        {
            GetAttribute(type).LoseGodDefend();
            GetBuilding(type).LoseGodDefend();
        }

        public void DecreaseDefend(TypeAttribute type, int value)
        {
            GetAttribute(type).DecreaseDefend(value);

            if (GetAttribute(type).ValueDefend > 0)
            {
                GetBuilding(type).ChangeValueDefend(value);
            }
            else
            {
                GetBuilding(type).LoseDefend();
            }
        }

        #endregion

        #region CARD_EFFECTS

        public void AddTemporaryEffect(Effect effect)
        {
            // TODO: check if effect alredy add are update duration this effect

            Debug.Log("Add TemporaryEffect");
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

        private bool CheckTypeBuilding(TypeAttribute type)
        {
            if (type == TypeAttribute.Economic ||
                type == TypeAttribute.Food ||
                type == TypeAttribute.Medicine ||
                type == TypeAttribute.RawMaterials)
            {
                return true;
            }

            return false;
        }

        private void ShowHealth(TypeAttribute typeBuilding)
        {
            GetBuilding(typeBuilding).ShowHealth();
        }

        #endregion

        public void EndRound()
        {
            if (temporaryEffect.Count > 0)
            {
                temporaryEffect = BoxController.GetController<EffectsController>().CheckCancelEffectsAfterEndRound(this, temporaryEffect);
            }
        }
    }
}