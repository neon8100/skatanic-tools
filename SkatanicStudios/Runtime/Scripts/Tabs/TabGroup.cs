using System;
using System.Collections;
using System.Collections.Generic;
using SubjectNerd.Utilities;
using UnityEngine;

namespace SkatanicStudios.UI
{
    public class TabGroup : MonoBehaviour
    {
        protected List<Tab> _tabs = new List<Tab>();

        public bool tabSwapsActiveGameObject;

        public GameObject[] gameObjects;

        public MultiPagePanel pagePanel;

        [SerializeField]
        protected Tab activeTab;

        public Action onTabSelectedCallback;

        private void Start()
        {
            StartActiveTab();
        }

        public void StartActiveTab()
        {
            if (activeTab != null)
            {
                SetActive(activeTab);
            }
        }

        public void Subscribe(Tab tab)
        {
            _tabs.Add(tab);

            StartActiveTab();

        }

        public void Unsubscribe(Tab tab)
        {
            _tabs.Remove(tab);
            if (activeTab = tab)
            {
                activeTab = null;
            }
        }

        public void SetActive(Tab tab)
        {

            foreach (Tab t in _tabs)
            {
                t.Deactivate();

            }

            activeTab = tab;
            activeTab.Activate();

            if (onTabSelectedCallback != null)
            {
                onTabSelectedCallback();
            }

            if (tabSwapsActiveGameObject)
            {
                SwapGameObject();
            }

            if (pagePanel != null)
            {
                pagePanel.SetPageIndex(tab.transform.GetSiblingIndex());

            }

        }

        public void SetActive(int siblingIndex)
        {

            foreach (Tab t in _tabs)
            {
                if (t.transform.GetSiblingIndex() == siblingIndex)
                {
                    SetActive(t);
                    return;
                }
            }

        }

        public int GetActiveTabIndex()
        {
            return activeTab.transform.GetSiblingIndex();
        }

        void SwapGameObject()
        {

            for (int i = 0; i < gameObjects.Length; i++)
            {
                if (activeTab.transform.GetSiblingIndex() != i)
                {
                    gameObjects[i].SetActive(false);
                }
            }
            if (activeTab.transform.GetSiblingIndex() < gameObjects.Length)
            {
                gameObjects[activeTab.transform.GetSiblingIndex()].SetActive(true);
            }
        }

        public void DisableTab(int index)
        {
            if (_tabs.Count > index)
            {
                _tabs[index].Disable();
            }
        }

        public void EnableTab(int index)
        {
            if (_tabs.Count > index)
            {

                _tabs[index].Enable();
            }
        }

        public void SetInteractableState(bool value)
        {
            foreach (Tab t in _tabs)
            {
                t.button.interactable = value;
            }
        }

    }
}
