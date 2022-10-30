using Cards;
using Cards.Data;
using Core;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "FightSceneController", menuName = "Controllers/Gameplay/FightSceneController")]
    public class FightSceneController : BaseController
    {
        private bool isPlayerNow = true;

        public bool GetIsPlayerNow { get => isPlayerNow; }

        public override void OnStart()
        {

        }

        public void StartGame()
        {

        }
    }
}