using UnityEngine;

namespace UI.Components
{
    public class MoveObject : MonoBehaviour
    {
        public delegate void Move();
        public event Move AfterMove;

        [SerializeField] protected float speed = 10;

        private Vector3 nextPos;
        private bool moveNow = false;

        public Vector3 GetNextPos { get => nextPos; }
        public Vector3 SetNextPosition { set => nextPos = value; }

        private void Start()
        {
            nextPos = transform.localPosition;
        }

        private void Update()
        {
            if (moveNow)
            {
                transform.localPosition = Vector2.MoveTowards(transform.localPosition, nextPos, speed * Time.deltaTime);

                if (transform.localPosition == nextPos)
                {
                    moveNow = false;

                    if (AfterMove != null)
                    {
                        AfterMove.Invoke();
                    }
                }
            }
        }

        public void SetNextPosositionForMove(Vector2 nextPos)
        {
            this.nextPos = nextPos;

            moveNow = true;
        }
    }
}