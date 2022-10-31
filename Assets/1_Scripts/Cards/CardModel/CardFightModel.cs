using Cards.Data;
using Cards.View;
using Core;
using DG.Tweening;
using Gameplay;
using UnityEngine;

namespace Cards
{
    [RequireComponent(typeof(CardFightView))]
    public class CardFightModel : CardModel
    {
        private const float SCALE_ANIM_VALUE = 1.5f, POS_Z = 0.1f;

        private Vector3 blockPosition = new Vector3(-20, 0, 0), unblockPosition = new Vector3(-20, 0, 0);

        public CardFightData SetCardData
        {
            set
            {
                data = value;

                (view as CardFightView).SetData(data as CardFightData);
            }
        }
        public CardFightData GetFightData { get => data as CardFightData; }

        #region INTERACTION

        protected override void MouseEnter()
        {
            BoxController.GetController<CardsController>().SelectFightCard(this);

            float scale = transform.localScale.x;
            Vector3 position = transform.position;

            transform.localScale = new Vector3(scale * SCALE_ANIM_VALUE, scale * SCALE_ANIM_VALUE, scale * SCALE_ANIM_VALUE);
            transform.position = new Vector3(position.x, position.y + POS_Z, position.z + POS_Z);
        }

        protected override void MouseExit()
        {
            BoxController.GetController<CardsController>().DeselectFightCard(this);

            float scale = transform.localScale.x;
            Vector3 position = transform.position;

            transform.localScale = new Vector3(scale / SCALE_ANIM_VALUE, scale / SCALE_ANIM_VALUE, scale / SCALE_ANIM_VALUE);
            transform.position = new Vector3(position.x, position.y - POS_Z, position.z - POS_Z);
        }

        protected override void MouseDown()
        {
            BoxController.GetController<CardsController>().ClickFightCard(this);
        }

        #endregion

        public void EndUseCard()
        {
            MouseExit();

            GetFightData.UpdateReloading();
        }

        public bool CheckCanUseCard()
        {
            return GetFightData.CurrentReloading == 0;
        }

        public void BlockCard()
        {
            isCanInteraction = false;

            Vector3 rotation = transform.rotation.eulerAngles;
            Tween tween = transform.DORotate(blockPosition, 0.7f);
            tween.Play();
        }

        public void UnlockCard()
        {
            isCanInteraction = true;

            Vector3 rotation = transform.rotation.eulerAngles;
            Tween tween = transform.DORotate(unblockPosition, 0.7f);
            tween.Play();
        }
    }
}