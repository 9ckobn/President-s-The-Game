using EffectSystem;
using UnityEngine;

namespace Buildings
{
    [RequireComponent(typeof(BoxCollider))]
    public class Building : MonoBehaviour
    {
        [SerializeField]private GameObject model;
        [SerializeField] private GameObject logo;
        [SerializeField] private GameObject effectExplosion;
        [SerializeField] private TypeAttribute typeBuilding;

        private void OnMouseUp()
        {
            
        }

        private void OnMouseDown()
        {
            
        }
    }
}
