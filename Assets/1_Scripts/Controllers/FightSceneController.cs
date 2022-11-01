using Core;
using Data;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "FightSceneController", menuName = "Controllers/Gameplay/FightSceneController")]
    public class FightSceneController : BaseController
    {
        private const int MAX_USE_CARDS = 3;

        private bool isPlayerNow = true;
        private CharacterData currentCharacter;

        private int countUseCards = 0;

        public bool GetIsPlayerNow { get => isPlayerNow; }
        public CharacterData GetCurrentCharacter { get => currentCharacter; }

        public void StartGame()
        {
            currentCharacter = BoxController.GetController<CharactersDataController>().GetPlayerData;
        }

        public void AddCountUseCards()
        {
            countUseCards++;

            if(countUseCards >= MAX_USE_CARDS)
            {
                ChangeCurrentPlayer();
            }
        }

        private void ChangeCurrentPlayer()
        {
            BoxController.GetController<CardsController>().BlockAllCards();

            if (isPlayerNow)
            {
                currentCharacter = BoxController.GetController<CharactersDataController>().GetEnemyData;
            }
            else
            {
                currentCharacter = BoxController.GetController<CharactersDataController>().GetPlayerData;
            }

            countUseCards = 0;
            isPlayerNow = !isPlayerNow;
        }
    }
}