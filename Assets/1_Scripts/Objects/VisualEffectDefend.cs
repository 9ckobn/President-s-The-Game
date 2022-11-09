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

        Tween tweenDefend;

        public void ShowDefend()
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
            sequence.Append(model.transform.DOLocalRotate(new Vector3(-90, 180, 0), 1f));

            //sequence.AppendCallback(() =>
            //{
            //    model.transform.DOScale(new Vector3(20, 20, 20), 0.5f);
            //    model.transform.DOLocalMoveY(0f, 0.5f);

            //});
            //sequence.AppendInterval(0.5f);

            //sequence.OnComplete(() =>
            //{
            //    tweenDefend = model.transform.DOLocalRotate(new Vector3(-90, 180, 0), 1f).SetLoops(-1, LoopType.Restart);
            //});
        }

        public void ShowGodDefend()
        {
            godDefend.gameObject.SetActive(true);
        }

        public void LoseDefend()
        {
            model.SetActive(false);

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
    }
}