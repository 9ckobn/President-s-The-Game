using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class MyButton : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClickButton);

            AfterAwake();
        }

        protected virtual void AfterAwake() { }

        protected virtual void OnClickButton(){ }

        private void OnDestroy()
        {
            GetComponent<Button>().onClick.RemoveListener(OnClickButton);
        }
    }
}