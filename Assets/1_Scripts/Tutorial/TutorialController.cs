using Core;
using UnityEngine;

namespace Tutorial
{
    [CreateAssetMenu(fileName = "TutorialController", menuName = "Controllers/Gameplay/TutorialController")]  
    public class TutorialController : BaseController
    {
        [SerializeField] private SCRO_TutorialData tutorialData;

        public SCRO_TutorialData GetTutorialData { get => tutorialData; }
    }
}