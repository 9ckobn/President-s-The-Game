using Cards;
using Core;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "DataGameController", menuName = "Controllers/DataGameController")]
    public class DataGameController : BaseController
    {
        private TypeClimate typeClimate;

        public TypeClimate GetTypeClimate { get => typeClimate; }
    }
}