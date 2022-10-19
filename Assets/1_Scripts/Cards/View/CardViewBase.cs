using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    public class CardViewBase : MonoBehaviour
    {
        [BoxGroup("Image")]
        [SerializeField] protected Image image;
    }
}