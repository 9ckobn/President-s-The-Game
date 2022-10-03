using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;

namespace UI
{
    public abstract class Window : MonoBehaviour, IInitialize
    {
        public bool IsActive { get; protected set; }

        #region EVENTS

        [HideInInspector]
        public UnityEvent<Window> EndShow, EndChange, EndHide;

        #endregion

        protected float timeShow;
        protected float timeHide;
        protected float timeChange;

        private GameObject background;
        private Animator animator;
        private bool haveAnimation;

        private CloseButton buttonClose;

        #region INITIALIZE

        public void OnInitialize()
        {
            SetAnimator();

            if (haveAnimation)
            {
                SetAnimationTime();
            }

            AfterInitialization();
        }

        protected virtual void AfterInitialization() { }

        public void OnStart()
        {
            background = gameObject.transform.GetChild(0).gameObject;

            if (background.activeSelf)
            {
                IsActive = true;
            }

            buttonClose = GetComponentInChildren<CloseButton>();
        }

        #endregion INITIALIZE

        protected void OnClickButtonClose()
        {
            BeforeClickButtonClose();
            Hide();
            AfterClickButtonClose();
        }

        #region ANIMATION

        private void SetAnimator()
        {
            if (TryGetComponent(out Animator animator))
            {
                this.animator = animator;
                haveAnimation = true;
            }
            else
            {
                haveAnimation = false;
            }
        }

        private void SetAnimationTime()
        {
            AnimationClip[] animations = gameObject.GetComponent<Animator>().runtimeAnimatorController.animationClips;

            foreach (var anim in animations)
            {
                if (anim.name == TypeAnimation.Show.ToString())
                {
                    timeShow = anim.length;
                }
                if (anim.name == TypeAnimation.Hide.ToString())
                {
                    timeHide = anim.length;
                }
                if (anim.name == TypeAnimation.Change.ToString())
                {
                    timeChange = anim.length;
                }
            }
        }

        private IEnumerator CoShowAnimation(TypeAnimation typeAnimation, Action callBack = null)
        {
            animator.SetTrigger(typeAnimation.ToString());
            float timeWait = 0;

            if (typeAnimation == TypeAnimation.Show)
            {
                timeWait = timeShow;
            }
            else if (typeAnimation == TypeAnimation.Hide)
            {
                timeWait = timeHide;
            }
            else if (typeAnimation == TypeAnimation.Change)
            {
                timeWait = timeChange;
            }

            yield return new WaitForSeconds(timeWait);
            callBack?.Invoke();
        }

        #endregion

        #region SHOW/HIDE

        public void Show()
        {
            if (IsActive)
            {
                return;
            }
            else
            {
                background.SetActive(true);
                IsActive = true;
                BeforeShow();

                if (haveAnimation)
                {
                    StartCoroutine(CoShowAnimation(TypeAnimation.Show, () => { AfterAnimationShow(); }));
                }
                else
                {
                    AfterAnimationShow();
                }
            }
        }

        public void Change() { }

        public void Hide()
        {
            if (!IsActive)
            {
                return;
            }
            else
            {
                background.SetActive(false);

                IsActive = false;
                BeforeHide();

                if (haveAnimation)
                {
                    StartCoroutine(CoShowAnimation(TypeAnimation.Hide, () => { AfterAnimationHide(); }));
                }
                else
                {
                    AfterAnimationHide();
                }
            }
        }

        private void AfterAnimationShow()
        {
            AfterShow();
            EndShow?.Invoke(this);
        }

        private void AfterAnimationHide()
        {
            AfterHide();
            EndHide?.Invoke(this);
        }

        protected virtual void BeforeClickButtonClose() { }
        protected virtual void AfterClickButtonClose() { }
        protected virtual void BeforeShow() { }
        protected virtual void AfterShow() { }
        protected virtual void BeforeHide() { }
        protected virtual void AfterHide() { }

        #endregion
    }
}
