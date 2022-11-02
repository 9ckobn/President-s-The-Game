using EffectSystem;
using UnityEngine;
using DG.Tweening;
using Core;
using NaughtyAttributes;
using System.Collections;
using SceneObjects;

namespace Buildings
{
    [RequireComponent(typeof(BoxCollider))]
    public class Building : MonoBehaviour
    {
        [BoxGroup("Model parts")]
        [SerializeField] private GameObject model, logo;
        [BoxGroup("Effects")]
        [SerializeField] private VisualEffectDefend effectDefend;
        [BoxGroup("Effects")]
        [SerializeField] private GameObject effectDamage, effectHealh;
        [BoxGroup("Type building")]
        [SerializeField] private TypeAttribute typeBuilding;
        [BoxGroup("Backlight effect")]
        [SerializeField] private MeshRenderer meshLight;
        [BoxGroup("Backlight effect")]
        [SerializeField] private Material defaultMaterial, lightMaterial;

        private bool isTarget;
        private Sequence mySequence;
        private Tween tween;

        public TypeAttribute GetTypeBuilding { get => typeBuilding; }

        private void Start()
        {
            mySequence = DOTween.Sequence();
        }

        private void OnMouseOver()
        {
            if (isTarget)
            {
                meshLight.material = lightMaterial;
            }
        }

        private void OnMouseExit()
        {
            if (isTarget)
            {
                meshLight.material = defaultMaterial;
            }
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

                meshLight.material = defaultMaterial;
            }
        }


        #region VISUAL_EFFECTS

        public void ShowGetDamage()
        {
            StartCoroutine(CoShowEffect(effectDamage));
        }

        public void ShowGetHealth()
        {
            StartCoroutine(CoShowEffect(effectHealh));
        }

        public void ShowGetDefend()
        {
            if (effectDefend != null)
                effectDefend.ShowGetDefend();
        }

        public void ShowDefend(int value = 100)
        {
            effectDefend.ShowDefend(value);
        }

        private IEnumerator CoShowEffect(GameObject effect)
        {
            effect.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            effect.SetActive(false);
        }

        #endregion
    }
}
