using Core;
using EffectSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class AttributeData
    {
        public TypeAttribute TypeAttribute { get; private set; }
        public int StartValue { get; private set; }
        public int Value { get; private set; }

        public bool IsHaveDefend { get; private set; }
        public bool IsGodDefend { get; private set; }
        public int ValueDefend { get; private set; }

        private int[] valueStates;

        private List<int> idMyEffect = new List<int>();

        private bool isActive = true;
        public bool GetIsActive { get => isActive; }

        public AttributeData(TypeAttribute type, int value, int[] valueStates = null)
        {
            TypeAttribute = type;
            Value = value;
            StartValue = value;
            IsHaveDefend = false;
            IsGodDefend = false;
            ValueDefend = 0;
            this.valueStates = valueStates;
        }

        public int GetAttributeState()
        {
            for (int i = 0; i < valueStates.Length; i++)
            {
                if (Value >= valueStates[i])
                {
                    return i;
                }
            }

            return valueStates[valueStates.Length - 1];
        }

        public void AddIdEffect(int id)
        {
            if (idMyEffect.Contains(id))
            {
                LogManager.LogError($"Double add effect");
            }
            else
            {
                idMyEffect.Add(id);
            }
        }

        public void RemoveEffect(int id)
        {
            if (idMyEffect.Contains(id))
            {
                idMyEffect.Remove(id);
            }
        }

        public bool IsHaveEffect(int id)
        {
            return idMyEffect.Contains(id);
        }

        public void AddValue(int value)
        {
            Value += value;
        }

        public void DecreaseValue(int value)
        {
            Value -= value;

            if(Value <= 0)
            {
                Value = 0;
                isActive = false;
            }
        }

        public void GetDefend(int valueDefend)
        {
            IsHaveDefend = true;
            ValueDefend = valueDefend;
        }

        public void GetGodDefend()
        {
            IsHaveDefend = true;
            IsGodDefend = true;
        }

        public void DecreaseDefend(int value)
        {
            ValueDefend -= value;

            if(ValueDefend < 0)
            {
                ValueDefend = 0;
            }
        }

        public void LoseGodDefend()
        {
            IsHaveDefend = false;
            IsGodDefend = false;
        }
    }
}