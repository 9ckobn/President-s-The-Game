using Data;
using UI.Components;
using UnityEngine;

namespace UI
{
    public class AttributesCharactersWindow : Window
    {
        [SerializeField] AttributesPanel playerAttributes, enemyAttributes;

        public void RedrawPlayerData(AttributeTextData[] data)
        {
            playerAttributes.RedrawData(data);
        }

        public void RedrawEnemyData(AttributeTextData[] data)
        {
            enemyAttributes.RedrawData(data);
        }
    }
}