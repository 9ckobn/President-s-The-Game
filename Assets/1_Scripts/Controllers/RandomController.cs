using Core;
using UI;
using UnityEngine;

namespace EffectSystem
{
    [CreateAssetMenu(fileName = "RandomController", menuName = "Controllers/Gameplay/RandomController")]
    public class RandomController : BaseController
    {
        private const int MAX_VALUE = 100, COUNT_SHAKE = 5;
        private string messageText;

        public bool CountRandom(int chanceValue)
        {
            messageText = "";
            bool[] randomsShake = new bool[COUNT_SHAKE];

            for (int i = 0; i < COUNT_SHAKE; i++)
            {
                int random = Random.Range(0, MAX_VALUE);
                randomsShake[i] = random <= chanceValue;
            }

            bool lucky = randomsShake[Random.Range(0, randomsShake.Length)];

            messageText += $"<color=green>���� :{chanceValue}</color>\n";
            messageText += $"<color=red>������ :{MAX_VALUE}</color>\n";
            messageText += lucky ? "�����" + "\n" : "�� �����" + "\n";

            if (lucky)
            {
                messageText += $"<color=green>������ ��������</color>\n";
            }
            else
            {
                messageText += $"<color=red>������ �� ��������</color>\n";
            }

            //UIManager.GetWindow<InfoWindow>().ShowText(messageText);

            return lucky;
        }
    }
}