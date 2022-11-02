using DG.Tweening;
using TMPro;
using UnityEngine;

namespace SceneObjects
{
    public class VisualEffectDefend : MonoBehaviour
    {
        private GameObject model;
        private TextMeshPro textMeshPro;

        private void Awake()
        {
            model = gameObject.transform.GetChild(0).gameObject;
            textMeshPro = GetComponentInChildren<TextMeshPro>();
        }

        public void ShowGetDefend()
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

            sequence.OnComplete(() => { model.SetActive(false); });
        }

        public void ShowDefend(int value)
        {
            model.transform.localPosition = new Vector3(0, 0, 0);
            model.transform.localScale = new Vector3(100, 100, 100);
            model.SetActive(true);

            if (value < 100)
            {
                textMeshPro.text = value.ToString();
                textMeshPro.gameObject.SetActive(true);
            }
        }

        public void HideDefend()
        {

        }
    }
}