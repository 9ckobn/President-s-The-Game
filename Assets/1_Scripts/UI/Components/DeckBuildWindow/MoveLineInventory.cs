using UnityEngine;

namespace UI.Components
{
    public class MoveLineInventory : MoveObject
    {
        private const float valueMove = 330f;

        private float startXPos = 0f;

        private void Awake()
        {
            startXPos = transform.localPosition.x;
        }

        /// <summary>
        /// Подвинуть панель вправо на один элемент
        /// </summary>
        public void MoveRight()
        {
            Vector3 nextPos = GetNextPos;
            nextPos.x -= valueMove;
            SetNextPosositionForMove(nextPos);
        }

        /// <summary>
        /// Подвинуть панель влево на один элемент
        /// </summary>
        public void MoveLeft()
        {
            Vector3 nextPos = GetNextPos;
            nextPos.x += valueMove;
            SetNextPosositionForMove(nextPos);
        }

        /// <summary>
        /// Вернуть стартовую позицию
        /// </summary>
        public void ResetStartPosition()
        {
            Vector2 startPosition = transform.localPosition;
            startPosition.x = startXPos;
            transform.localPosition = startPosition;
            SetNextPosition = startPosition;
        }
    }
}