using Buildings;
using Data;
using UnityEngine;

namespace EffectSystem
{
    public abstract class ApplyRandomEffect : ApplyEffect
    {
        protected const int MAX_VALUE = 100, COUNT_SHAKE = 5;

        protected CharacterData characterData;

        protected int maxValue, chanceValue;
        protected string text;        

        public override void SelectTargetBuilding(Building building)
        {
        }

        protected void CountRandom()
        {
            bool[] randomsShake = new bool[COUNT_SHAKE];

            for (int i = 0; i < COUNT_SHAKE; i++)
            {
                int random = Random.Range(0, maxValue);
                randomsShake[i] = random <= chanceValue;
            }

            bool lucky = randomsShake[Random.Range(0, randomsShake.Length)];

            text += $"<color=green>Шанс :{chanceValue}</color>\n";
            text += $"<color=red>против :{maxValue}</color>\n";
            text += lucky ? "удача" : "Не удача" + "\n";

            if (lucky)
            {
                WinRandom();
            }
            else
            {
                LoseRandom();
            }

            EndApply();
        }

        protected abstract void WinRandom();
        protected abstract void LoseRandom();
    }
}