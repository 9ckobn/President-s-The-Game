using Core;
using EffectSystem;
using Tutorial;
using UI;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "FightSceneController", menuName = "Controllers/Gameplay/FightSceneController")]
    public class FightSceneController : BaseController
    {
        private const int MAX_USE_CARDS = 1;        

        private int countUseCards = 0;

        public bool IsTutorNow { get; private set; }

        public override void OnInitialize()
        {
            IsTutorNow = AppManager.Instance.IsTutorNow;
        }

        public void StartGame()
        {
            bool isPlayer = BoxController.GetController<CharactersDataController>().GetIsPlayerNow;

            BoxController.GetController<CardsController>().CreateCards();
            BoxController.GetController<CharactersDataController>().CreateCharactersData();
            //BoxController.GetController<CardsController>().ShowCardsCharacter(isPlayer);
            BoxController.GetController<CardsController>().DecreaseReloadingCharacterCards(!isPlayer);

            if (IsTutorNow)
            {
                BoxController.GetController<TutorialController>().StartTutorial();
            }
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
            BoxController.GetController<CardsController>().HighlightPlayerPresidentCards(isPlayer);
            BoxController.GetController<CardsController>().DecreaseReloadingCharacterCards(!isPlayer);
        }
    }
}