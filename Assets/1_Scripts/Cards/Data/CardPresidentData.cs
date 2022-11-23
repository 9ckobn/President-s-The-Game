using Cards.Type;
using EffectSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cards.Data
{
    public class CardPresidentData : CardDataBase
    {
        private int baseAttack;
        private int baseDefend;
        private int baseLuck;
        private int baseDiplomatic;
        
        public int Rarityrank { get; private set; }
        public TypeClimate Climate { get; private set; }

        public int Attack { get => baseAttack + GetValueBuff(TypeAttribute.Attack); }
        public int Defend { get => baseDefend + GetValueBuff(TypeAttribute.Defend); }
        public int Luck { get => baseLuck + GetValueBuff(TypeAttribute.Luck); }
        public int Diplomatic { get => baseDiplomatic + GetValueBuff(TypeAttribute.Diplomacy); }

        public List<TypeAttribute> PossiblePresidentBuff { get; private set; }

        private List<BuffAttribute> buffAttributes = new List<BuffAttribute>();

        public int GetValueBuff(TypeAttribute type)
        {
            return buffAttributes.First(buff => buff.TypeAttribute == type).Value;
        }

        public TypeStateAttribute GetBuffAttributeState(TypeAttribute type)
        {
            return buffAttributes.First(buff => buff.TypeAttribute == type).State;
        }

        public CardPresidentData(CardPresidentDataSerialize data, Sprite sprite) : base(data.id.ToString(), sprite, data.name, 1)
        {
            Rarityrank = data.rarityrank;
            baseAttack = data.attack;
            baseDefend = data.defend;
            baseLuck = data.luck;
            baseDiplomatic = data.diplomatic;

            DefineClimate(data.climate_zone);
            CreateBuffAttributes(data);
        }

        private void CreateBuffAttributes(CardPresidentDataSerialize data)
        {
            // TODO: Count this buffs after create Climate
            buffAttributes.Add(new BuffAttribute(TypeAttribute.Attack, 0));
            buffAttributes.Add(new BuffAttribute(TypeAttribute.Defend, 0));
            buffAttributes.Add(new BuffAttribute(TypeAttribute.Luck, 0));
            buffAttributes.Add(new BuffAttribute(TypeAttribute.Diplomacy, 0));

            if (data.economy)
            {
                PossiblePresidentBuff.Add(TypeAttribute.Economic);
            }
            else if (data.health_care)
            {
                PossiblePresidentBuff.Add(TypeAttribute.Medicine);
            }
            else if (data.raw_materials)
            {
                PossiblePresidentBuff.Add(TypeAttribute.RawMaterials);
            }
            else if (data.food)
            {
                PossiblePresidentBuff.Add(TypeAttribute.Food);
            }            
        }

        private void DefineClimate(string climate)
        {
            if (climate == "temperate")
            {
                Climate = TypeClimate.Temperate;
            }
            else if (climate == "equatorial")
            {
                Climate = TypeClimate.Equatorial;
            }
            else if (climate == "tropical")
            {
                Climate = TypeClimate.Tropical;
            }
        }
    }
}