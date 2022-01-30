using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;

namespace SkatanicStudios.Localisation
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UITextLocaliser : MonoBehaviour
    {

        [SerializeField]
        public string key;

        private TextMeshProUGUI _textBox;

        public bool preview = true;

        // Use this for initialization
        void Start()
        {
            Set();
        }

        void OnEnable()
        {
            Set();
        }

        void Set()
        {
            _textBox = GetComponent<TextMeshProUGUI>();

            string val = TextLocalisation.GetLocalisedValue(key);

            _textBox.text = TextLocalisation.GetLocalisedValue(key);

            if (gameObject.GetComponent<UITextAnimator>())
            {
                gameObject.GetComponent<UITextAnimator>().Set(val);
            }
        }

        public void Refresh()
        {
            Set();
        }
    }
}
