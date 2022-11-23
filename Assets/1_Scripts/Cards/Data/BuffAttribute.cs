using Cards.Type;
using EffectSystem;

namespace Cards.Data
{
    public class BuffAttribute
    {
        public TypeAttribute TypeAttribute { get; private set; }
        public int Value { get; private set; }
        public TypeStateAttribute State { get; private set; }

        public BuffAttribute(TypeAttribute typeAttribute,int value)
        {
            TypeAttribute = typeAttribute;
            Value = value;

            ChangeState();
        }

        public void AddValue(int value)
        {
            Value += value;

            ChangeState();
        }

        public void SubstrateValue(int value)
        {
            Value -= value;

            ChangeState();
        }

        private void ChangeState()
        {
            if (Value == 0)
            {
                State = TypeStateAttribute.Default;
            }
            else if (Value > 0)
            {
                State = TypeStateAttribute.Up;
            }
            else if (Value < 0)
            {
                State = TypeStateAttribute.Down;
            }
        }
    }
}