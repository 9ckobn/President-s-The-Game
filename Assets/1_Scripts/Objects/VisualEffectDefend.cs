using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

namespace SceneObjects
{
    public class VisualEffectDefend : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshPro;
        [SerializeField] private GameObject model, brokenModel;

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
                textMeshPro.gameObject.SetActive(false);
            });

            ShowRandomDefend(randomDefend);
        }

        public void ShowDefend(int randomDefend)
        {
            model.transform.localPosition = new Vector3(0, 0, 0);
            model.transform.localScale = new Vector3(100, 100, 100);
            model.SetActive(true);

            ShowRandomDefend(randomDefend);
        }

        private void ShowRandomDefend(int value)
        {
            if (value > 0)
            {
                textMeshPro.text = value.ToString();
                textMeshPro.gameObject.SetActive(true);
            }
        }

        public void LoseDefend()
        {
            StartCoroutine(CoLoseDefend());
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
            textMeshPro.gameObject.SetActive(false);
        }
    }
}