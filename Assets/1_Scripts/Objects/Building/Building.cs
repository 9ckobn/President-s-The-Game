using EffectSystem;
using UnityEngine;
using DG.Tweening;
using Core;
using NaughtyAttributes;
using System.Collections;
using SceneObjects;
using TMPro;

namespace Buildings
{
    [RequireComponent(typeof(BoxCollider))]
    public class Building : MonoBehaviour
    {
        [BoxGroup("Model parts")]
        [SerializeField] private GameObject model, logo;
        [BoxGroup("Model states")]
        [SerializeField] private GameObject[] modelStates;
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
        [BoxGroup("UI")]
        [SerializeField] private TextMeshProUGUI valueText;

        private bool isTarget;
        private Tween tween;

        public TypeAttribute GetTypeBuilding { get => typeBuilding; }

        public int SetValue { set => valueText.text = value.ToString(); }

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
                BoxController.GetController<EffectsController>().SelectTargetBuilding(typeBuilding);
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
            isTarget = false;

            meshLight.material = defaultMaterial;
            effectDefend.HideDefend();
        }

        public void ChangeStateBuilding(int numberState)
        {
            if (numberState >= modelStates.Length)
            {
                BoxController.GetController<LogController>().LogError($"Number state building > count models!");
            }
            else
            {
                for (int i = 0; i < modelStates.Length; i++)
                {
                    modelStates[i].SetActive(i == numberState);
                }
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

        public void ShowGetDefend(int randomDefend)
        {
            effectDefend.ShowGetDefend(randomDefend);
        }

        public void ShowDefend(int randomDefend)
        {
            effectDefend.ShowDefend(randomDefend);
        }

        public void HideDefend()
        {
            effectDefend.HideDefend();
        }

        public void ShowLoseDefend()
        {
            effectDefend.LoseDefend();
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
