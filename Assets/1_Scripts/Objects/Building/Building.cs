using EffectSystem;
using UnityEngine;
using DG.Tweening;
using Core;
using NaughtyAttributes;
using System.Collections;
using SceneObjects;
using UI.Components;

namespace Buildings
{
    [RequireComponent(typeof(BoxCollider))]
    public class Building : MonoBehaviour
    {
        [BoxGroup("Model parts")]
        [SerializeField] private GameObject logo;
        [BoxGroup("Model states")]
        [SerializeField] private GameObject[] modelStates;
        [BoxGroup("Effects")]
        [SerializeField] private VisualEffectDefend effectDefend;
        [BoxGroup("Effects")]
        [SerializeField] private GameObject effectDamage, effectHealh;
        [BoxGroup("Type building")]
        [SerializeField] private TypeAttribute typeBuilding;
        [BoxGroup("Backlight effect")]
        [SerializeField] private MeshRenderer[] meshsLight;
        [BoxGroup("Backlight effect")]
        [SerializeField] private Material defaultMaterial, lightMaterial;
        [BoxGroup("UI")]
        [SerializeField] private BuildingCanvas canvas;

        private bool isTarget;
        private Tween tween;

        public TypeAttribute GetTypeBuilding { get => typeBuilding; }

        private void Awake()
        {
            effectDefend.SetBuilding = this;
        }

        public void OnMouseOver()
        {
            if (isTarget)
            {
                foreach (var mesh in meshsLight)
                {
                    mesh.material = lightMaterial;
                }
            }
        }

        public void OnMouseExit()
        {
            if (isTarget)
            {
                foreach (var mesh in meshsLight)
                {
                    mesh.material = defaultMaterial;
                }
            }
        }

        public void OnMouseDown()
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

            foreach (var mesh in meshsLight)
            {
                mesh.material = defaultMaterial;
            }
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

        public void ShowValueAttribute(int value)
        {
            canvas.ShowValueAttribute(value);
        }

        #region VISUAL_EFFECTS

        public void ShowDamage()
        {
            StartCoroutine(CoShowEffect(effectDamage));
        }

        public void ShowHealth()
        {
            StartCoroutine(CoShowEffect(effectHealh));
        }

        public void ShowGetDefend(int valueDefend)
        {
            effectDefend.ShowDefend();
            canvas.ShowValueDefend(valueDefend);
        }

        public void ShowGodDefend()
        {
            effectDefend.ShowGodDefend();
        }

        public void ChangeValueDefend(int value)
        {
            canvas.ShowValueDefend(value);
        }

        public void LoseDefend()
        {
            Debug.Log("LOSE DEFEND");
            effectDefend.LoseDefend();
            canvas.HideValueDefend();
        }

        public void LoseGodDefend()
        {
            effectDefend.LoseGodDefend();
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
