using Core;
using EffectSystem;
using EnemyAI;
using Tutorial;
using UI;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "FightSceneController", menuName = "Controllers/Gameplay/FightSceneController")]
    public class FightSceneController : BaseController
    {
        private const int MAX_USE_CARDS = 3;

        private int countUseCards = 0;

        public bool IsTutorNow { get; private set; }

        public override void OnInitialize()
        {
            IsTutorNow = AppManager.Instance.IsTutorNow;
        }

        public void StartGame()
        {
            Debug.Log($"DEBUG Start fight scene controller");

            bool isPlayer = BoxController.GetController<CharactersDataController>().GetIsPlayerNow;

            BoxController.GetController<CardsController>().CreateCards();
            BoxController.GetController<CharactersDataController>().CreateCharactersData();

            if (IsTutorNow)
            {
                BoxController.GetController<TutorialController>().StartTutorial();
            }
            else
            {
                //UIManager.GetWindow<SelectBuffAttributeWindow>().OnClickStartGame.AddListener(SlickStartGame);
                //UIManager.ShowWindow<SelectBuffAttributeWindow>();

                // DELETE!!!!!!!!!!!!!!!!!!!!
                UIManager.HideWindow<BlackoutWindow>();
                BoxController.GetController<CardsController>().SetCanUseCard = true;
                UIManager.ShowWindow<AttributesCharactersWindow>();
            }
        }

        private void SlickStartGame()
        {
            UIManager.GetWindow<SelectBuffAttributeWindow>().OnClickStartGame.RemoveListener(SlickStartGame);
            UIManager.HideWindow<BlackoutWindow>();
            BoxController.GetController<CardsController>().SetCanUseCard = true;
        }

        public void AddCountUseCards()
        {
            countUseCards++;

            if (countUseCards >= MAX_USE_CARDS)
            {
                BoxController.GetController<EffectsController>().EndRound();
                BoxController.GetController<CharactersDataController>().ChangeCurrentCharacter();

                countUseCards = 0;

                bool isPlayer = BoxController.GetController<CharactersDataController>().GetIsPlayerNow;
                //BoxController.GetController<CardsController>().HighlightPlayerPresidentCards(isPlayer);
                BoxController.GetController<CardsController>().DecreaseReloadingCharacterCards(!isPlayer);

                NewRound();
            }
            else if (!BoxController.GetController<CharactersDataController>().GetIsPlayerNow)
            {
                BoxController.GetController<EnemyAiController>().NextCard();
            }
        }

        public void EnemyAiSkipRound()
        {
            LogManager.Log($"Not have selected card. Ai skip round!");

            countUseCards = 3;
            AddCountUseCards();
        }

        private void NewRound()
        {
            bool isPlayer = BoxController.GetController<CharactersDataController>().GetIsPlayerNow;

            if (isPlayer)
            {
                BoxController.GetController<CardsController>().SetCanUseCard = true;
            }
            else
            {
                BoxController.GetController<CardsController>().SetCanUseCard = false;
                BoxController.GetController<EnemyAiController>().StartRound();
            }
        }

        public void PlayerLoseGame()
        {
            UIManager.GetWindow<EndGameWindow>().SetData("Your lose!");
        }

        public void EnemyLoseGame()
        {
            UIManager.GetWindow<EndGameWindow>().SetData("Your win!");
        }

        public void RestartGame()
        {
            AppManager.Instance.IsTutorNow = false;

            LoadSceneManager.Instance.LoadFightScene();
        }
    }
}