using UnityEngine;

namespace UI.Components
{
    public class MoveScrollCards : MoveObject
    {
        private const float VALUE_MOVE = 990f;

        private float startXPos = 0f;

        private void Awake()
        {
            startXPos = transform.localPosition.x;
        }

        public void MoveRight()
        {
            Vector3 nextPos = GetNextPos;
            nextPos.x -= VALUE_MOVE;
            SetNextPosositionForMove(nextPos);
        }

        public void MoveLeft()
        {
            Vector3 nextPos = GetNextPos;
            nextPos.x += VALUE_MOVE;
            SetNextPosositionForMove(nextPos);
        }

        public void ResetStartPosition()
        {
            Vector2 startPosition = transform.localPosition;
            startPosition.x = startXPos;
            transform.localPosition = startPosition;
            SetNextPosition = startPosition;
        }
    }
}