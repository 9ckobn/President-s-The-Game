using Buildings;
using Core;
using Data;
using Gameplay;
using UnityEngine;

namespace EffectSystem
{
    public class ApplyRandomEffect : ApplyEffect
    {
        private const int MAX_VALUE = 100, COUNT_SHAKE = 5;

        private RandomUpAttributeEffect effect;
        private CharacterData characterData;
        public override void Apply(Effect currentEffect)
        {
            effect = currentEffect as RandomUpAttributeEffect;

            for (int i = 0; i < 10; i++)
            {
                CountRandom();
            }
        }

        public override void SelectTargetBuilding(Building building)
        {
        }

        private void CountRandom()
        {
            characterData = BoxController.GetController<FightSceneController>().GetCurrentCharacter;

            int maxValue = MAX_VALUE;
            int chanceValue = effect.Value;

            if (effect.IsNeedAttribute)
            {
                chanceValue += characterData.GetValueAttribute(effect.TypeAttribute);
            }

            bool[] randomsShake = new bool[COUNT_SHAKE];

            for (int i = 0; i < COUNT_SHAKE; i++)
            {
                int random = Random.Range(0, maxValue);
                randomsShake[i] = random <= chanceValue;
            }

            bool lucky = randomsShake[Random.Range(0, randomsShake.Length)];

            if (lucky)
            {
                WinRandom();
            }
            else
            {
                LoseRandom();
            }
        }

        private void WinRandom()
        {

        }

        private void LoseRandom()
        {

        }
    }
}