using Core;
using EffectSystem;
using System.Collections.Generic;

namespace Data
{
    public class AttributeData
    {
        public TypeAttribute TypeAttribute { get; private set; }
        public int Value { get; private set; }
        public bool IsHaveDefend { get; private set; }
        public bool IsRandomDefend { get; private set; }
        public int ValueDefend { get; private set; }
        public int ValueRandomDefend { get; private set; }

        private List<int> idMyEffect = new List<int>();        

        public AttributeData(TypeAttribute type, int value)
        {
            TypeAttribute = type;
            Value = value;
            IsHaveDefend = false;
            IsRandomDefend = false;
            ValueDefend = 0;
            ValueRandomDefend = 0;
        }

        public void AddIdEffect(int id)
        {
            if (idMyEffect.Contains(id))
            {
                BoxController.GetController<LogController>().LogError($"Double add effect");
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
        }

        public void SetDefend(bool randomDefend, int valueDefend, int valueRandomDefend = 0)
        {
            IsHaveDefend = true;
            IsRandomDefend = randomDefend;
            ValueDefend = valueDefend;
            ValueRandomDefend = valueRandomDefend;
        }
    }
}