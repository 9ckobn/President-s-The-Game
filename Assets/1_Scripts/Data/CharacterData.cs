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

        private List<Effect> temporaryEffects = new List<Effect>();

        public List<Effect> TemporaryEffects
        {
            get
            {
                return temporaryEffects;
            }

            set
            {
                temporaryEffects = value;
            }
        }

        public CharacterData(List<AttributeData> attributes, Building[] buildings, bool isPlayer)
        {
            this.attributes = attributes;
            myBuildings = buildings;
            this.isPlayer = isPlayer;

            RedrawData();
        }

        public Building GetBuilding(TypeAttribute typeBuilding)
        {
            foreach (var building in myBuildings)
            {
                if (building.GetTypeBuilding == typeBuilding)
                {
                    return building;
                }
            }

            LogManager.LogError($"Not have building {typeBuilding}!");
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

        // For tutorial
        public void ChangeCanSelectBuilding(TypeAttribute type, bool canSelect)
        {
            GetBuilding(type).SetCanSelectedForTarget = canSelect;
        }

        public void EnableArrowPointerOnBuilding(TypeAttribute typeBuilding)
        {
            GetBuilding(typeBuilding).EnableArrowPointer();
        }

        public void DisableArrowPointerOnBuilding(TypeAttribute typeBuilding)
        {
            GetBuilding(typeBuilding).DisableArrowPointer();
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

            LogManager.LogError($"Not have attribute {type}");
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

            if (CheckTypeBuilding(type))
            {
                GetAttribute(TypeAttribute.Morality).DecreaseValue(value);
            }

            CountMorality();
            RedrawData();
        }

        private void CountMorality()
        {
            if (GetAttribute(TypeAttribute.Morality).Value <= 0)
            {
                LogManager.LogError($"Character DEATH. Need logic death!");
            }
        }

        private void RedrawData()
        {
            AttributeTextData[] data = new AttributeTextData[5];

            data[0] = new AttributeTextData(TypeAttribute.Attack, GetAttribute(TypeAttribute.Attack).Value);
            data[1] = new AttributeTextData(TypeAttribute.Defend, GetAttribute(TypeAttribute.Defend).Value);
            data[2] = new AttributeTextData(TypeAttribute.Luck, GetAttribute(TypeAttribute.Luck).Value);
            data[3] = new AttributeTextData(TypeAttribute.Diplomacy, GetAttribute(TypeAttribute.Diplomacy).Value);
            data[4] = new AttributeTextData(TypeAttribute.Morality, GetAttribute(TypeAttribute.Morality).Value);

            if (isPlayer)
            {
                UIManager.GetWindow<AttributesCharactersWindow>().RedrawPlayerData(data);
            }
            else
            {
                UIManager.GetWindow<AttributesCharactersWindow>().RedrawEnemyData(data);
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

            temporaryEffects.Add(effect);
        }

        public List<BuffEffect> GetBuffEffects()
        {
            List<BuffEffect> buffEffects = new List<BuffEffect>();

            foreach (var effect in temporaryEffects)
            {
                if (effect is BuffEffect)
                {
                    buffEffects.Add(effect as BuffEffect);
                }
            }

            return buffEffects;
        }

        public List<OtherEffect> GetOtherEffects()
        {
            List<OtherEffect> buffEffects = new List<OtherEffect>();

            foreach (var effect in temporaryEffects)
            {
                if (effect is OtherEffect)
                {
                    buffEffects.Add(effect as OtherEffect);
                }
            }

            return buffEffects;
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
    }
}