using Core;
using Data;
using EffectSystem;
using UI;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "FightSceneController", menuName = "Controllers/Gameplay/FightSceneController")]
    public class FightSceneController : BaseController
    {
        private const int MAX_USE_CARDS = 1;        

        private int countUseCards = 0;        

        public void StartGame()
        {
            bool isPlayer = BoxController.GetController<CharactersDataController>().GetIsPlayerNow;
            BoxController.GetController<CardsController>().ShowCardsCharacter(isPlayer);
            BoxController.GetController<CardsController>().DecreaseReloadingCharacterCards(!isPlayer);
        }

        public void AddCountUseCards()
        {
            countUseCards++;

            if(countUseCards >= MAX_USE_CARDS)
            {
                EndRound();
            }
        }

        private void EndRound()
        {
            BoxController.GetController<EffectsController>().EndRound();
            BoxController.GetController<CharactersDataController>().ChangeCurrentCharacter();

            countUseCards = 0;

            bool isPlayer = BoxController.GetController<CharactersDataController>().GetIsPlayerNow;
            BoxController.GetController<CardsController>().ShowCardsCharacter(isPlayer);
            BoxController.GetController<CardsController>().DecreaseReloadingCharacterCards(!isPlayer);
        }
    }
}