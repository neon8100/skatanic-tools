using LivingTheDeal.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SkatanicStudios.UI
{
    [AddComponentMenu("UI/Extended Button", 30)]
    public class ExtendedButton : Button
    {
        public bool tweenOnMouseover;
        public bool useTweener;

        public bool toggleGraphic;

        private UITweener _tweener;

        [SerializeField]
        private Image _graphicToggleTarget;
        [SerializeField]
        private Sprite[] graphicsToToggle;
        [SerializeField]
        private Color[] toggleColor;

        private bool isToggle;

        public float time = 0.1f;

        public Vector2 scaleTo = new Vector2(1.05f, 1.05f);

        private Vector3 _defaultScale;

        private LTDescr currentTween;


        protected override void Awake()
        {
            base.Awake();

            _defaultScale = transform.localScale;

            _tweener = GetComponent<UITweener>();
            
            if (_graphicToggleTarget == null)
            {
                _graphicToggleTarget = GetComponent<Image>();
            }
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            if (interactable)
            {
                base.DoStateTransition(SelectionState.Highlighted, true);
            }

            if (tweenOnMouseover && interactable)
            {
                if (useTweener)
                {
                    _tweener.Enable();
                }
                else
                {

                    if (currentTween != null)
                    {
                        LeanTween.cancel(currentTween.uniqueId);
                    }

                    currentTween = LeanTween.scale(transform.GetComponent<RectTransform>(), scaleTo, time);
                }
            }
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);


            if (interactable)
            {
                base.DoStateTransition(SelectionState.Selected, true);
            }

            if (tweenOnMouseover && interactable)
            {
                if (useTweener)
                {

                    _tweener.Reverse();
                }
                else
                {
                    if (currentTween != null)
                    {
                        LeanTween.cancel(currentTween.uniqueId);
                    }

                    currentTween = LeanTween.scale(transform.GetComponent<RectTransform>(), _defaultScale, time);
                }
            }
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            
            if (interactable)
            {
                base.DoStateTransition(SelectionState.Normal, true);
            }

            if (tweenOnMouseover && interactable)
            {
                if (useTweener)
                {
                    _tweener.Reverse();

                }
                else
                {
                    if (currentTween != null)
                    {
                        LeanTween.cancel(currentTween.uniqueId);
                    }

                    currentTween = LeanTween.scale(transform.GetComponent<RectTransform>(), _defaultScale, time);
                }
            }
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            if (interactable)
            {
                base.DoStateTransition(SelectionState.Pressed, true);
            }

            if (toggleGraphic && interactable)
            {
                isToggle = !isToggle;

                _graphicToggleTarget.sprite = (isToggle) ? graphicsToToggle[1] : graphicsToToggle[0];
                _graphicToggleTarget.color = (isToggle) ? toggleColor[1] : toggleColor[0];
            }
        }
    }
}
