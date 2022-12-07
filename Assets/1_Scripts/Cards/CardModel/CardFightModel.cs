using Cards.Data;
using Cards.View;
using Core;
using DG.Tweening;
using EffectSystem;
using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Cards
{
    [RequireComponent(typeof(CardFightView))]
    public class CardFightModel : CardModel
    {
        private const float SCALE_ANIM_VALUE = 1.5f, POS_Z = 0.1f;

        private Vector3 blockPosition = new Vector3(-20, 0, 0), unblockPosition = new Vector3(20, 180, 0);
        private Vector3 startPosition;

        private Sequence mySequence;

        private bool canUseCard = true;
        private bool isPlayerCard;

        public bool SetIsPlayerCard { set => isPlayerCard = value; }
        public Vector3 SetBlockPosition { set => blockPosition = value; }
        public Vector3 SetUnblockPosition { set => unblockPosition = value; }
        public bool GetCanUseCard { get => canUseCard; }
        public bool SetCanUseCard 
        {
            set
            {
                canUseCard = value;

                if (!canUseCard)
                {
                    (view as CardFightView).ShowBlackHighlight();
                }
            }
        }

        public CardFightData SetCardData
        {
            set
            {
                data = value;

                (view as CardFightView).SetData(data as CardFightData);
            }
        }

        private CardFightData getFightData { get => data as CardFightData; }

        #region GET_DATA

        public string GetId { get => getFightData.Id; }
        public List<Effect> GetEffects { get => getFightData.GetEffects; }
        public TypeAttribute[] GetTypesCost { get => getFightData.TypeCost; }
        public int GetValueCost { get => getFightData.Cost; }

        #endregion

        private void Start()
        {
            startPosition = transform.localPosition;
        }

        #region INTERACTION

        protected override bool CheckMouseEnter()
        {
            bool playerCheck = false;

            if ((isPlayerCard && !isBlocked) || (!isPlayerCard && isBlocked))
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

            IncreaseAnimation();
        }

        protected override void MouseExit()
        {
            BoxController.GetController<CardsController>().DeselectFightCard(this);

            DecreaseAnimation();
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

        public override void ChangeHighlight(bool isActive)
        {
            if (isActive)
            {
                (view as CardFightView).ShowWhiteHighlight();
            }
            else
            {
                (view as CardFightView).HideHighlight();
            }
        }

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
            return canUseCard && !isBlocked && getFightData.CurrentReloading == 0;
        }

        public void DecreaseReloading()
        {
            getFightData.DecreaseReloading();

            if (getFightData.CurrentReloading == 0)
            {
                UnBlockCard();
            }
        }

        public void BlockCard(bool needRotate = false)
        {
            isBlocked = true;
            isUse = false;
            isSelected = false;

            if (needRotate)
            {
                Tween tween = transform.DORotate(blockPosition, 0.7f);
                tween.Play();
            }
        }

        public void UnBlockCard()
        {
            isBlocked = false;

            Tween tween = transform.DORotate(unblockPosition, 0.7f);
            tween.Play();
        }

        public void ShowHighlightRandom(bool luck)
        {
            (view as CardFightView).ShowHighlightRandom(luck);
        }

        public void HideHighlightRandom()
        {
            (view as CardFightView).HideHighlight();
        }

        private void IncreaseAnimation()
        {
            mySequence = DOTween.Sequence();

            mySequence.AppendCallback(() =>
            {
                transform.DOScale(new Vector3(startScale * SCALE_ANIM_VALUE, startScale * SCALE_ANIM_VALUE, startScale * SCALE_ANIM_VALUE), 0.15f);
                transform.DOLocalMove(new Vector3(startPosition.x, startPosition.y + POS_Z, startPosition.z + POS_Z), 0.3f);
            });
        }

        private void DecreaseAnimation()
        {
            mySequence = DOTween.Sequence();

            mySequence.AppendCallback(() =>
            {
                transform.DOScale(new Vector3(startScale, startScale, startScale), 0.15f);
                transform.DOLocalMove(new Vector3(startPosition.x, startPosition.y, startPosition.z), 0.3f);
            });
        }

        #endregion

        #region ENEMY_AI

        public void AiUseCard()
        {
            ChangeHighlight(true);
            IncreaseAnimation();

            BoxController.GetController<EffectsController>().ClickFightCard(this);
        }

        public void AiSkipCard()
        {
            ChangeHighlight(false);
            DecreaseAnimation();

            BoxController.GetController<EffectsController>().StopUseFightCard();
        }

        #endregion
    }
}