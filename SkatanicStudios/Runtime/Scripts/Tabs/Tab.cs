using System;
using UnityEngine;
using UnityEngine.UI;

namespace SkatanicStudios.UI
{
    [RequireComponent(typeof(TabButton))]
    public class Tab : MonoBehaviour
    {
        public GameObject tabGroup;

        private TabGroup _tabGroup;
        private MultiTabGroup _multiTabGroup;

        private TabButton _button;
        public TabButton button
        {
            get
            {
                if (_button == null)
                {
                    _button = GetComponent<TabButton>();
                }

                return _button;
            }
        }

        public Action onTabSelectedCallback;

        public void Start()
        {
            button.onClick.AddListener(OnSelectTab);

            if (tabGroup == null)
            {
                if (GetComponentInParent<TabGroup>() != null)
                {
                    tabGroup = transform.parent.gameObject;
                }
                else
                {

                    Debug.Log("No Tab Group assigned on " + gameObject.name); return;
                }
            }
            if (tabGroup.GetComponent<MultiTabGroup>() != null)
            {
                _multiTabGroup = tabGroup.GetComponent<MultiTabGroup>();
                _multiTabGroup.Subscribe(this);
            }
            else
            {
                _tabGroup = tabGroup.GetComponent<TabGroup>();
                _tabGroup.Subscribe(this);
            }
        }


        [Header("Colors")]
        public Graphic[] graphics;
        public Color[] activeColors;
        public Color[] inactiveColors;
        [Header("Game Objects")]
        public GameObject[] showOnSelected;
        public GameObject[] hideOnDeselected;

        bool isActive;

        public void OnSelectTab()
        {

            Debug.Log("Tab Button Clicked");

            if (onTabSelectedCallback != null)
            {
                onTabSelectedCallback();
            }

            if (_multiTabGroup == null)
            {
                _tabGroup.SetActive(this);
                return;
            }

            if (!isActive)
            {
                _multiTabGroup.SetActive(this);
            }
            else
            {
                _multiTabGroup.SetInactive(this);
            }

        }


        public void Activate()
        {
            if (graphics == null) { return; }
            for (int i = 0; i < graphics.Length; i++)
            {
                graphics[i].color = activeColors[i];
            }

            isActive = true;
            button.selected = true;

            foreach (GameObject obj in showOnSelected)
            {
                obj.SetActive(true);
            }

        }

        public void Deactivate()
        {
            if (graphics == null) { return; }
            for (int i = 0; i < graphics.Length; i++)
            {
                graphics[i].color = inactiveColors[i];
            }

            isActive = false;

            button.selected = false;
            button.OnResetColor();

            foreach (GameObject obj in hideOnDeselected)
            {
                obj.SetActive(false);
            }
        }

        public void Disable()
        {
            button.interactable = false;
        }

        public void Enable()
        {
            button.interactable = true;
        }


        private void OnDestroy()
        {
            if (_tabGroup != null)
            {
                _tabGroup.Unsubscribe(this);
            }
        }


    }
}
