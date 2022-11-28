using Core;
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
            NextStep();
        }

        private void NextStep()
        {
            currentStep = stepsTutorial[counterSteps];

            if (currentStep is SCRO_TextTutorial)
            {
                StartTextTutor();
            }
        }

        private void StartTextTutor()
        {
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
                    EndTextTutor();
                }
                else
                {
                    NextTextTutor();
                }
            }
        }

        private void EndTextTutor()
        {

        }
    }
}