using EffectSystem;
using UnityEngine;
using DG.Tweening;
using Core;

namespace Buildings
{
    [RequireComponent(typeof(BoxCollider))]
    public class Building : MonoBehaviour
    {
        [SerializeField] private GameObject model;
        [SerializeField] private GameObject logo;
        [SerializeField] private GameObject effectExplosion;
        [SerializeField] private TypeAttribute typeBuilding;

        private bool isTarget;
        private Sequence mySequence;
        private Tween tween;

        public TypeAttribute GetTypeBuilding { get => typeBuilding; }

        private void Start()
        {
            mySequence = DOTween.Sequence();
        }

        private void OnMouseDown()
        {
            if (isTarget)
            {
                BoxController.GetController<EffectsController>().SelectTargetBuilding(this);
            }
        }

        public void EnableStateTarget()
        {
            if (!isTarget)
            {
                isTarget = true;

                Vector3 rotate = new Vector3(logo.transform.rotation.x, 360, logo.transform.rotation.z);

                tween = logo.transform.DORotate(rotate, 2f, RotateMode.FastBeyond360)
                    .SetLoops(-1, LoopType.Restart)
                    .OnStepComplete(
                    () =>
                    {
                        if (!isTarget)
                        {
                            tween.Complete();
                            tween.Kill();
                        }
                    });
            }
        }

        public void DisableStateTarget()
        {
            if (isTarget)
            {
                isTarget = false;
            }
        }

        public void GetDamage()
        {

        }

        public void GetHealth()
        {

        }
    }
}
