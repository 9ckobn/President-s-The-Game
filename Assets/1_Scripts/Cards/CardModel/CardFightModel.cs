using Cards.Data;
using Cards.View;
using Core;
using DG.Tweening;
using EffectSystem;
using Gameplay;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    [RequireComponent(typeof(CardFightView))]
    public class CardFightModel : CardModel
    {
        private const float SCALE_ANIM_VALUE = 1.5f, POS_Z = 0.1f;

        private Vector3 blockPosition = new Vector3(-20, 0, 0), unblockPosition = new Vector3(20, 180, 0);
        private Vector3 startPosition;

        private Sequence mySequence;

        private bool isPlayerCard;
        public bool SetIsPlayerCard { set => isPlayerCard = value; }

        public Vector3 SetBlockPosition { set => blockPosition = value; }
        public Vector3 SetUnblockPosition { set => unblockPosition = value; }

        public CardFightData SetCardData
        {
            set
            {
                data = value;

                (view as CardFightView).SetData(data as CardFightData);
            }
        }
        private CardFightData getFightData { get => data as CardFightData; }

        private void Start()
        {
            startPosition = transform.localPosition;
        }

        #region GET_DATA

        public List<Effect> GetEffects { get => getFightData.GetEffects; }
        public TypeAttribute[] GetTypeCost { get => getFightData.TypeCost; }
        public int GetValueCost { get => getFightData.Cost; }

        #endregion

        #region INTERACTION

        protected override bool CheckMouseEnter()
        {
            bool playerCheck = false;

            if((isPlayerCard && !isBlocked) ||(!isPlayerCard && isBlocked))
            {
                playerCheck = true;
            }

            return !isSelected && cardController.CanSelectedCard && playerCheck;
        }

        protected override bool CheckMouseDown()
        {
            return isSelected && isPlayerCard && !isBlocked;
        }

        protected override void MouseEnter()
        {
            BoxController.GetController<CardsController>().SelectFightCard(this);

            float scale = startScale;
            Vector3 position = startPosition;

            mySequence = DOTween.Sequence();

            mySequence.AppendCallback(() =>
            {
                transform.DOScale(new Vector3(scale * SCALE_ANIM_VALUE, scale * SCALE_ANIM_VALUE, scale * SCALE_ANIM_VALUE), 0.15f);
                transform.DOLocalMove(new Vector3(position.x, position.y + POS_Z, position.z + POS_Z), 0.3f);
            });
        }

        protected override void MouseExit()
        {
            BoxController.GetController<CardsController>().DeselectFightCard(this);

            float scale = startScale;
            Vector3 position = startPosition;

            mySequence = DOTween.Sequence();

            mySequence.AppendCallback(() =>
            {
                transform.DOScale(new Vector3(scale, scale, scale), 0.15f);
                transform.DOLocalMove(new Vector3(position.x, position.y, position.z), 0.3f);
            });
        }

        protected override void UseCard()
        {
            BoxController.GetController<CardsController>().UseFightCard(this);
        }

        protected override void StopUseCard()
        {
            BoxController.GetController<CardsController>().StopUseFightCard(this);
        }

        #endregion

        #region CHANGE_STATE

        public void EndUseCard()
        {
            MouseExit();

            getFightData.UpdateReloading();
            ChangeHighlight(false);

            if (!CheckCanUseCard())
            {
                BlockCard(true);
            }
        }

        public bool CheckCanUseCard()
        {
            return !isBlocked && getFightData.CurrentReloading == 0;
        }

        public void DecreaseReloading()
        {
            getFightData.DecreaseReloading();

            if (getFightData.CurrentReloading == 0)
            {
                UnlockCard();
            }
        }

        private void BlockCard(bool needRotate = false)
        {
            isBlocked = true;

            if (needRotate)
            {
                Tween tween = transform.DORotate(blockPosition, 0.7f);
                tween.Play();
            }
        }

        private void UnlockCard()
        {
            isBlocked = false;

            Tween tween = transform.DORotate(unblockPosition, 0.7f);
            tween.Play();
        }

        #endregion
    }
}