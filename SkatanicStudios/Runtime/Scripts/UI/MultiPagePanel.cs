using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SkatanicStudios;
using System;

namespace SkatanicStudios.UI
{
    public class MultiPagePanel : ExtendedMonoBehaviour
    {
        [Header("Page Panel Data")]
        [SerializeField]
        protected Button _nextButton;

        [SerializeField]
        protected Button _backButton;
        
        [SerializeField]
        protected GameObject[] _panelPages;

        [SerializeField]
        protected TabGroup _headerTabs;

        [SerializeField]
        protected int _currentPage = 0;

        [SerializeField]
        protected bool hideNextButtonAtEnd = true;

        public Action onEndOfPanelNextButtonClickedCallback;

        public void Awake()
        {
            Init();

            _nextButton.onClick.AddListener(NextPage);
            _backButton.onClick.AddListener(PreviousPage);

        }

        void Init()
        {
            ShowCurrentPanel();
        }

        

        void ShowCurrentPanel()
        {
            //Loop through the pages
            for (int i = 0; i < _panelPages.Length; i++)
            {
                //Activate the current page and hide the others;
                if (i == _currentPage)
                {
                    _panelPages[i].gameObject.SetActive(true);
                }
                else
                {
                    _panelPages[i].gameObject.SetActive(false);
                }
            }

            if (_currentPage > 0)
            {
                _backButton.gameObject.SetActive(true);
            }
            else
            {
                _backButton.gameObject.SetActive(false);
            }

            if (_currentPage >= (_panelPages.Length-1))
            {
                if (hideNextButtonAtEnd)
                {
                    _nextButton.gameObject.SetActive(false);
                }

                OnLastPage();
            }
            else
            {
                _nextButton.gameObject.SetActive(true);
            }
            
        }

        void SetTabIndex()
        {
            if (_headerTabs != null)
            {
                _headerTabs.SetActive(_currentPage);
            }
        }

        public void NextPage()
        {
            if(_currentPage>= _panelPages.Length-1)
            {
                
                
                if (onEndOfPanelNextButtonClickedCallback != null)
                {
                    onEndOfPanelNextButtonClickedCallback();
                }

                return;
            }
            _currentPage++;

            ShowCurrentPanel();
            SetTabIndex();
        }

        public void PreviousPage()
        {
            if (_currentPage == 0)
            {
                return;
            }

            _currentPage--;

            ShowCurrentPanel();
            SetTabIndex();
        }


        public void SetPageIndex(int index)
        {
            if (_panelPages.Length <= index) { return; }
            if (index < 0) { return; }

            _currentPage = index;

            ShowCurrentPanel();
        }

        /// <summary>
        /// Called once the panel reaches the last page.
        /// </summary>
        public virtual void OnLastPage()
        {

        }





    }
}
