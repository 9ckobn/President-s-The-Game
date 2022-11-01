using Buildings;
using Core;
using Data;
using Gameplay;
using UI;
using UnityEngine;

namespace EffectSystem
{
    public class ApplyRandomEffect : ApplyEffect
    {
        private const int MAX_VALUE = 100, COUNT_SHAKE = 5;

        private RandomUpAttributeEffect effect;
        private CharacterData characterData;

        private string text;

        public override void Apply(Effect currentEffect)
        {
            text = "";
            effect = currentEffect as RandomUpAttributeEffect;

            CountRandom();
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

        private void WinRandom()
        {
            characterData.UpAttribute(effect.TypeWinAttribute, effect.WinProcent);

            text += $"<color=green>добавляем : {effect.TypeWinAttribute} {effect.WinProcent}</color>\n";
            UIManager.Instance.GetWindow<RandomWindow>().ShowText(text);
        }

        private void LoseRandom()
        {
            characterData.DownAttribute(effect.TypeLoseAttribute, effect.LoseProcent);

            text += $"<color=red>отнимаем : {effect.TypeLoseAttribute} {effect.LoseProcent}</color>\n";
            UIManager.Instance.GetWindow<RandomWindow>().ShowText(text);
        }
    }
}