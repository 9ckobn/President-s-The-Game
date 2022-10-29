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

        public TypeAttribute GetTypeBuilding{ get => typeBuilding; }

        private bool isTarget;

        private void OnMouseUp()
        {
            
        }

        private void OnMouseDown()
        {
            
        }

        public void EnableStateTarget()
        {
            isTarget = true;

            Debug.Log("state target");
        }
    }
}
