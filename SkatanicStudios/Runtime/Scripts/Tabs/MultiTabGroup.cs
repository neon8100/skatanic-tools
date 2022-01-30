using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkatanicStudios.UI
{
    public class MultiTabGroup : TabGroup
    {
        [SerializeField]
        private int _maxSelection;

        [SerializeField]
        private List<Tab> _activeTabs;

        private void Awake()
        {
            if (_activeTabs == null)
            {
                _activeTabs = new List<Tab>();
            }
        }

        public new void SetActive(Tab tab)
        {
            if (_activeTabs.Count < _maxSelection)
            {
                _activeTabs.Add(tab);
            }

            foreach (Tab t in _tabs)
            {
                t.Deactivate();
            }

            foreach (Tab t in _activeTabs)
            {
                t.Activate();
            }

            if (onTabSelectedCallback != null)
            {
                onTabSelectedCallback();
            }

        }

        public void SetInactive(Tab tab)
        {
            if (_activeTabs.Contains(tab))
            {
                tab.Deactivate();
                _activeTabs.Remove(tab);
            }

            if (onTabSelectedCallback != null)
            {
                onTabSelectedCallback();
            }

        }


        public bool isMaxSelectionReached
        {
            get
            {
                return (_activeTabs.Count >= _maxSelection);
            }
        }

    }
}
