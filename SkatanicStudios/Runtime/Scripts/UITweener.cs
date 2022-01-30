using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.Serialization;

namespace LivingTheDeal.UI
{

    public enum UIAnimationTypes
    {
        Move,
        Scale,
        ScaleX,
        ScaleY,
        Fade,
        Rect
    }

    public enum TweenDirection
    {
        Forwards,
        Backwards
    }

    public class UITweener : MonoBehaviour
    {
        public GameObject objectToAnimate;

        public UIAnimationTypes animationType;
        private TweenDirection _direction;
        public LeanTweenType easeType;
        public float duration;
        public float delay;
        [Tooltip("Multiplies the delay based on the child index so that UI elements can stack delay automatically")]
        public bool multiplyDelayByChildIndex;
        public bool hideDuringDelay;


        public bool loop;
        public bool pingpong;

        public bool startPositionOffset;
        public bool useStartingPosition;
        public Vector3 from;
        public Vector3 to;

        private LTDescr _tweenObject;

        [FormerlySerializedAs("showOnEnable")]
        public bool animateOnEnable;
        [FormerlySerializedAs("workOnDisable")]
        public bool animateOnDisable;
        public bool resetOffsetOnDisable = true;

        public bool useSoundBehaviours;
        public UISoundBehaviour soundBehaviour;

        private Vector2 startingPosition;
        private RectTransform rectTransform;

        public LTDescr Tween => _tweenObject;
        public Action onComplete;

        private void Awake()
        {
            if (objectToAnimate == null)
            {
                objectToAnimate = gameObject;
            }

            rectTransform = objectToAnimate.GetComponent<RectTransform>();
            
            ResetStartingPosition();
        }
        public void OnEnable()
        {
            //StartCoroutine(OnEnableRoutine());
            if (animateOnEnable)
            {
                Enable();
            }
        }
        /*
        private IEnumerator OnEnableRoutine()
        {
            if (_tweenObject != null)
            {
                Cancel();

                yield return new WaitForEndOfFrame();
            }

            if (animateOnEnable)
            {
                Enable();
            }
            yield return new WaitForEndOfFrame();
        }*/
        public void ResetStartingPosition()
        {
            startingPosition = rectTransform.anchoredPosition; 
        }

        public void Enable()
        {
           
            /*
            if (_tweenObject != null)
            {
                Debug.Log("Cancel");
                Cancel();
            }*/
            
            SwapDirection(TweenDirection.Forwards);

            if (useSoundBehaviours)
            {
                LeanTween.delayedCall(delay, () =>
                {
                    soundBehaviour.enableSound?.Play();
                });
            }

            HandleTween();
        }

        private void HandleTween()
        {
            _tweenObject = null;
            switch (animationType)
            {
                case UIAnimationTypes.Fade:
                    Fade();
                    break;
                case UIAnimationTypes.Move:
                    MoveAbsolute();
                    break;
                case UIAnimationTypes.Scale:
                    Scale();
                    break;
                case UIAnimationTypes.ScaleX:
                    from = new Vector3(from.x, transform.localScale.y, transform.localScale.z);
                    to = new Vector3(to.x, transform.localScale.y, transform.localScale.z);
                    Scale();
                    break;
                case UIAnimationTypes.ScaleY:
                    from = new Vector3(transform.localScale.x, from.y, transform.localScale.z);
                    to = new Vector3(transform.localScale.x, to.y, transform.localScale.z);
                    Scale();
                    break;
                case UIAnimationTypes.Rect:
                    Rect();
                    break;

            }
            
            var delayResult = delay;
            if (multiplyDelayByChildIndex)
            {
                delayResult = delay * transform.GetSiblingIndex();
            }

            _tweenObject.setDelay(delayResult);

            if ((delayResult >= 0f) && hideDuringDelay)
            {
                CanvasGroup group = objectToAnimate.GetComponent<CanvasGroup>();
                if (group == null)
                {
                    group = objectToAnimate.AddComponent<CanvasGroup>();
                }

                group.alpha = 0;
                //Show the object when it starts
                _tweenObject.setOnStart(() =>
                {
                    group.alpha = 1;
                });

            }

            _tweenObject.setEase(easeType);

            if (loop)
            {
                _tweenObject.loopCount = int.MaxValue;
            }
            if (pingpong)
            {
                _tweenObject.setLoopPingPong();
            }
            if (onComplete != null)
            {
                _tweenObject.setOnComplete(onComplete);
            }
            
        }

        public void Fade()
        {

            if (gameObject.GetComponent<CanvasGroup>() == null)
            {
                gameObject.AddComponent<CanvasGroup>();
            }

            if (startPositionOffset)
            {
                objectToAnimate.GetComponent<CanvasGroup>().alpha = from.x;
            }
            _tweenObject = LeanTween.alphaCanvas(objectToAnimate.GetComponent<CanvasGroup>(), to.x, duration);
        }

        public void MoveAbsolute()
        {

            rectTransform.anchoredPosition = (useStartingPosition) ? startingPosition  + new Vector2(from.x, from.y) : new Vector2(from.x, from.y);

            Vector2 destination = (useStartingPosition) ? startingPosition + new Vector2(to.x, to.y) : new Vector2(to.x, to.y);

            _tweenObject = LeanTween.move(rectTransform, destination, duration);
        }

        public void Scale()
        {
            if (startPositionOffset)
            {
                objectToAnimate.GetComponent<RectTransform>().localScale = from;
            }
            _tweenObject = LeanTween.scale(objectToAnimate, to, duration);
        }

        private void Rect()
        {
            if (startPositionOffset)
            {
                rectTransform.sizeDelta = from;
            }
            _tweenObject = LeanTween.value(objectToAnimate, RectSize, from, to, duration);
        }

        private void RectSize(Vector3 size)
        {
            rectTransform.sizeDelta = size;
        }

        void SwapDirection(TweenDirection direction)
        {
            if (_direction != direction)
            {
                Cancel();
                _direction = direction;
                var temp = from;
                from = to;
                to = temp;
            }

        }

        public void Reverse()
        {
            SwapDirection(TweenDirection.Backwards);

            HandleTween();

            _tweenObject.setOnComplete(() =>
            {

                SwapDirection(TweenDirection.Forwards);
            });
        }
            

        public void Disable()
        {
            if (!animateOnDisable)
            {
                gameObject.SetActive(false);
            }
            else
            {
                if (useSoundBehaviours)
                {
                    LeanTween.delayedCall(delay, () =>
                    {
                        soundBehaviour.disableSound?.Play();
                    });
                }

                SwapDirection(TweenDirection.Backwards);

                HandleTween();

                _tweenObject.setOnComplete(() =>
                {

                    SwapDirection(TweenDirection.Forwards);

                    gameObject.SetActive(false);
                });
            }
            
        }
        public void Disable(Action onCompleteAction)
        {
            if (!animateOnDisable)
            {
                gameObject.SetActive(false);
                onCompleteAction();
            }
            else
            {
                SwapDirection(TweenDirection.Backwards);

                HandleTween();

                _tweenObject.setOnComplete(() =>
                {

                    SwapDirection(TweenDirection.Forwards);

                    onCompleteAction();
                });
            }
            
        }

        public void Cancel()
        {
            if (_tweenObject != null)
            {
                LeanTween.cancel(gameObject, _tweenObject.id);
            }

            if (objectToAnimate != null && startPositionOffset && animationType == UIAnimationTypes.Scale)
            {
                objectToAnimate.GetComponent<RectTransform>().localScale = to;
            }
        }

        private void OnDisable()
        {
            if (useStartingPosition && resetOffsetOnDisable)
            {
                rectTransform.anchoredPosition = startingPosition;
            }
        }
    }
}
