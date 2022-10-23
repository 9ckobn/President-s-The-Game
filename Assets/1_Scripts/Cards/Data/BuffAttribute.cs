using Cards.Type;

namespace Cards.Data
{
    public class BuffAttribute
    {
        private int value;
        private TypeStateAttribute state;

        public int GetValue { get => value; }
        public TypeStateAttribute GetState { get => state; }

        public BuffAttribute(int value)
        {
            this.value = value;

            ChangeState();
        }

        public void AddValue(int value)
        {
            this.value += value;

            ChangeState();
        }

        public void SubstrateValue(int value)
        {
            this.value -= value;

            ChangeState();
        }

        private void ChangeState()
        {
            if (value == 0)
            {
                state = TypeStateAttribute.Default;
            }
            else if (value > 0)
            {
                state = TypeStateAttribute.Up;
            }
            else if (value < 0)
            {
                state = TypeStateAttribute.Down;
            }
        }
    }
}