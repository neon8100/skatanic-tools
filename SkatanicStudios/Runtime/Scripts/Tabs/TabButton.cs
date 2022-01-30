using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SkatanicStudios.UI
{
    public class TabButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private bool _interactable = true;

        public Graphic targetGraphic;
        public ColorBlock colors;

        public bool interactable
        {
            get
            {
                return _interactable;
            }
            set
            {
                _interactable = value;
                OnInteractableStateChanged();
            }
        }
        public bool selected = false;
        public UnityEvent onClick;

        void Reset()
        {
            if (GetComponent<Graphic>() != null && targetGraphic == null)
            {
                targetGraphic = GetComponent<Graphic>();
            }
        }

        private void Awake()
        {
            if (GetComponent<Graphic>() != null && targetGraphic == null)
            {
                targetGraphic = GetComponent<Graphic>();
            }

            targetGraphic.color = colors.normalColor;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!interactable) { return; }

            if (!selected)
            {
                targetGraphic.color = colors.pressedColor;
            }

            if (onClick != null)
            {
                onClick.Invoke();
            }

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!interactable) { return; }
            if (!selected)
            {
                targetGraphic.color = colors.highlightedColor;
            }

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!interactable) { return; }
            if (!selected)
            {
                OnResetColor();
            }

        }

        public void OnResetColor()
        {
            OnInteractableStateChanged();

        }

        private void OnInteractableStateChanged()
        {

            targetGraphic.color = (interactable) ? colors.normalColor : colors.disabledColor;
        }

    }
}
