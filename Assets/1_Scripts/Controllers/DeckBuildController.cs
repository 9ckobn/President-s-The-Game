using Cards;
using Core;
using UI;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "DeckBuildController", menuName = "Controllers/Gameplay/DeckBuildController")]
    public class DeckBuildController : BaseController
    {
        private TypeClimate typeClimate;

        public TypeClimate GetTypeClimate { get => typeClimate; }

        public override void OnStart()
        {
            UIManager.Instance.ShowWindow<DeckBuildWindow>();
        }
    }
}