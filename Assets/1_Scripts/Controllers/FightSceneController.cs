using Core;
using Data;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "FightSceneController", menuName = "Controllers/Gameplay/FightSceneController")]
    public class FightSceneController : BaseController
    {
        private bool isPlayerNow = true;
        private CharacterData currentCharacter;

        public bool GetIsPlayerNow { get => isPlayerNow; }
        public CharacterData GetCurrentCharacter { get => currentCharacter; }

        public void StartGame()
        {
            currentCharacter = BoxController.GetController<CharactersDataController>().GetPlayerData;
        }
    }
}