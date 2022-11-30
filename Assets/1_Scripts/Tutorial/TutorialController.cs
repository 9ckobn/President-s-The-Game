using Buildings;
using Core;
using EffectSystem;
using Gameplay;
using SceneObjects;
using UI;
using UnityEngine;

namespace Tutorial
{
    [CreateAssetMenu(fileName = "TutorialController", menuName = "Controllers/Gameplay/TutorialController")]
    public class TutorialController : BaseController
    {
        [SerializeField] private SCRO_TutorialData tutorialData;
        [SerializeField] private SCRO_StepTutorial[] stepsTutorial;

        public SCRO_TutorialData GetTutorialData { get => tutorialData; }

        private int counterSteps = 0;
        private SCRO_StepTutorial currentStep;

        private string[] textsTutorial;
        private int counterText = 0;

        public void StartTutorial()
        {
            UIManager.GetWindow<SelectBuffAttributeWindow>().HideLabelWindow();

            NextStep();
        }

        private void NextStep()
        {
            currentStep = stepsTutorial[counterSteps];

            if (currentStep is SCRO_TextTutorial)
            {
                StartTextTutor();
            }
            else if (currentStep is SCRO_ActionTutorial)
            {
                StartAction();
            }
        }

        private void EndStep()
        {
            if(counterSteps + 1 < stepsTutorial.Length)
            {
                counterSteps++;
                NextStep();
            }
            else
            {
                EndTutorial();
            }
        }

        private void EndTutorial()
        {
            Debug.Log("END TUTOR !!!!!!");
        }

        #region TEXT_TUTOR

        private void StartTextTutor()
        {
            UIManager.ShowWindow<TutorialWindow>();

            textsTutorial = (currentStep as SCRO_TextTutorial).Texts;
            counterText = 0;

            NextTextTutor();
        }

        private void NextTextTutor()
        {
            UIManager.GetWindow<TutorialWindow>().OnClick.AddListener(ClickTutorialWindow);
            UIManager.GetWindow<TutorialWindow>().ShowTutorText(textsTutorial[counterText]);
        }

        private void ClickTutorialWindow()
        {
            UIManager.GetWindow<TutorialWindow>().OnClick.RemoveListener(ClickTutorialWindow);

            if (currentStep is SCRO_TextTutorial)
            {
                if (counterText + 1 < textsTutorial.Length)
                {
                    counterText++;

                    NextTextTutor();
                }
                else
                {
                    EndTextTutor();
                }
            }
        }

        private void EndTextTutor()
        {
            EndStep();
        }

        #endregion

        private void StartAction()
        {
            TypeActionTutor action = (currentStep as SCRO_ActionTutorial).TypeAction;

            if (action == TypeActionTutor.ShowBuffAttributeWindow)
            {
                UIManager.ShowWindow<SelectBuffAttributeWindow>();
                EndStep();
            }
            else if (action == TypeActionTutor.WaitClickStartGame)
            {
                UIManager.GetWindow<SelectBuffAttributeWindow>().OnClickStartGame.AddListener(ClickStartGame);
            }
            else if (action == TypeActionTutor.HideBlackout)
            {
                UIManager.HideWindow<BlackoutWindow>();
                EndStep();
            }
            else if (action == TypeActionTutor.SelectBuilding)
            {
                Building[] buildings = ObjectsOnScene.Instance.GetBuildingsStorage.GetPlayerBuildings;
                foreach (var building in buildings)
                {
                    building.EnableHighlight();
                }

                EndStep();
            }
            else if (action == TypeActionTutor.DeselectBuilding)
            {
                Building[] buildings = ObjectsOnScene.Instance.GetBuildingsStorage.GetPlayerBuildings;
                foreach (var building in buildings)
                {
                    building.DisableStateTarget();
                }

                EndStep();
            }
            else if (action == TypeActionTutor.EnableAttributes)
            {
                UIManager.ShowWindow<AttributesCharactersWindow>();
                EndStep();
            }
            else if (action == TypeActionTutor.HidePopup)
            {
                UIManager.GetWindow<TutorialWindow>().HidePopup();
                EndStep();
            }
            else if (action == TypeActionTutor.HighlightFightCards)
            {
                BoxController.GetController<CardsController>().HighlightPlayerFightCards(true);
                EndStep();
            }
            else if (action == TypeActionTutor.UnhighlightFightCards)
            {
                BoxController.GetController<CardsController>().HighlightPlayerFightCards(false);
                EndStep();
            }
            else if (action == TypeActionTutor.ShowPopupCardFightDescription)
            {
                UIManager.GetWindow<TutorialWindow>().ShowDescriptionCard();
                EndStep();
            }
            else if (action == TypeActionTutor.HidePopupCardFightDescription)
            {
                UIManager.GetWindow<TutorialWindow>().HideDescriptionCard();
                EndStep();
            }
            else if (action == TypeActionTutor.ShowBlackout)
            {
                UIManager.ShowWindow<BlackoutWindow>();
                EndStep();
            }
            else if (action == TypeActionTutor.BlockFightCards)
            {
                BoxController.GetController<CardsController>().SetCanUseCard = true;
                BoxController.GetController<CardsController>().BlockAllCardsExceptOne();

                BoxController.GetController<CharactersDataController>().GetPlayerData.ChangeCanSelectBuilding(TypeAttribute.Economic, false);
                BoxController.GetController<CharactersDataController>().GetPlayerData.ChangeCanSelectBuilding(TypeAttribute.Food, false);
                BoxController.GetController<CharactersDataController>().GetPlayerData.ChangeCanSelectBuilding(TypeAttribute.RawMaterials, false);

                EndStep();
            }
            else if (action == TypeActionTutor.UnblockFightCards)
            {
                BoxController.GetController<CardsController>().UnblockAllCardsExceptOne();

                BoxController.GetController<CharactersDataController>().GetPlayerData.ChangeCanSelectBuilding(TypeAttribute.Economic, true);
                BoxController.GetController<CharactersDataController>().GetPlayerData.ChangeCanSelectBuilding(TypeAttribute.Food, true);
                BoxController.GetController<CharactersDataController>().GetPlayerData.ChangeCanSelectBuilding(TypeAttribute.RawMaterials, true);

                EndStep();
            }
        }

        private void ClickStartGame()
        {
            UIManager.GetWindow<SelectBuffAttributeWindow>().OnClickStartGame.RemoveListener(ClickStartGame);
            EndStep();
        }
    }
}