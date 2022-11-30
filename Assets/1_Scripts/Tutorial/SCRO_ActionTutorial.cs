using UnityEngine;

namespace Tutorial
{
    [CreateAssetMenu(fileName = "ActionTutorial", menuName = "Data/Tutorial/ActionTutorial")]
    public class SCRO_ActionTutorial : SCRO_StepTutorial
    {
        public TypeActionTutor TypeAction;
    }

    public enum TypeActionTutor
    {
        ShowBuffAttributeWindow,
        WaitClickStartGame,
        HideBlackout,
        SelectBuilding,
        DeselectBuilding,
        EnableAttributes,
        HidePopup,
        HighlightFightCards,
        UnhighlightFightCards,
        ShowPopupCardFightDescription,
        HidePopupCardFightDescription,
        ShowBlackout,
        BlockFightCards,
        UnblockFightCards
    }
}