using Core;
using Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EndGameWindow : Window
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Text characterWinText;

        protected override void AfterInitialization()
        {
            restartButton.onClick.AddListener(() => BoxController.GetController<FightSceneController>().RestartGame());
        }

        public void SetData(string winText)
        {
            characterWinText.text = winText;

            Show();
        }
    }
}