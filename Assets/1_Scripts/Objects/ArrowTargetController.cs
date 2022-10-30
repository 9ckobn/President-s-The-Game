using UnityEngine;

namespace SceneObjects
{
    public class ArrowTargetController : MonoBehaviour
    {
        private GameObject beginPos, targetPos;
        private Camera mainCamera;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            Vector3 Mouse = Input.mousePosition;
            Mouse.z = Vector3.Distance(mainCamera.transform.position, targetPos.transform.position);
            Vector3 target = mainCamera.ScreenToWorldPoint(Mouse);

            gameObject.transform.LookAt(target);
            gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.eulerAngles.x, gameObject.transform.rotation.eulerAngles.y, 0);

            float _scaleArrowZ = Vector3.Distance(target, gameObject.transform.position);
            gameObject.transform.localScale = new Vector3(_scaleArrowZ, _scaleArrowZ, 2.5f * _scaleArrowZ);
        }

        public void ResetAnimationArrow()
        {
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = Vector3.one;
            gameObject.SetActive(false);
        }

        public void SetPositions(GameObject beginPos, GameObject targetPos)
        {
            transform.position = beginPos.transform.position;
            this.targetPos = targetPos;
        }
    }
}