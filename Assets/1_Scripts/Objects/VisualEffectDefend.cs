using Buildings;
using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

namespace SceneObjects
{
    public class VisualEffectDefend : MonoBehaviour
    {
        [SerializeField] private GameObject model, brokenModel;
        [SerializeField] private GodDefend godDefend;

        public Building SetBuilding { set => godDefend.SetBuilding = value; }

        public void ShowGetDefend(int randomDefend)
        {
            model.SetActive(true);

            Sequence sequence = DOTween.Sequence();

            model.transform.localScale = new Vector3(20f, 20f, 20f);

            sequence.AppendCallback(() =>
            {
                model.transform.DOScale(new Vector3(100f, 100f, 100f), 0.5f);
                model.transform.DOLocalMoveY(0.225f, 0.5f);
            });

            sequence.AppendInterval(0.5f);
            sequence.Append(model.transform.DOLocalRotate(new Vector3(-90, 180, 0), 0.5f));

            sequence.AppendCallback(() =>
            {
                model.transform.DOScale(new Vector3(20, 20, 20), 0.5f);
                model.transform.DOLocalMoveY(0f, 0.5f);

            });
            sequence.AppendInterval(0.5f);

            sequence.OnComplete(() =>
            {
                model.SetActive(false);
            });
        }

        public void ShowDefend()
        {
            model.transform.localPosition = new Vector3(0, 0, 0);
            model.transform.localScale = new Vector3(100, 100, 100);
            model.SetActive(true);
        }

        public void ShowGodDefend()
        {
            godDefend.gameObject.SetActive(true);
        }

        public void LoseDefend()
        {
            StartCoroutine(CoLoseDefend());
        }

        public void LoseGodDefend()
        {
            godDefend.gameObject.SetActive(false);
        }

        private IEnumerator CoLoseDefend()
        {
            brokenModel.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.9f);
            brokenModel.gameObject.SetActive(false);
        }

        public void HideDefend()
        {
            model.SetActive(false);
        }
    }
}