using EffectSystem;
using UnityEngine;
using DG.Tweening;

namespace Buildings
{
    [RequireComponent(typeof(BoxCollider))]
    public class Building : MonoBehaviour
    {
        [SerializeField] private GameObject model;
        [SerializeField] private GameObject logo;
        [SerializeField] private GameObject effectExplosion;
        [SerializeField] private TypeAttribute typeBuilding;

        public TypeAttribute GetTypeBuilding { get => typeBuilding; }

        private bool isTarget;

        private void Start()
        {
        }

        private void OnMouseUp()
        {

        }

        private void OnMouseDown()
        {

        }

        public void EnableStateTarget()
        {
            isTarget = true;

            Vector3 rotate = new Vector3(logo.transform.rotation.x, 360, logo.transform.rotation.z);

            logo.transform.DORotate(rotate, 2f, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart);
        }
    }
}
