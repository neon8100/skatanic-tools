using UnityEngine;
using System;

namespace SkatanicStudios
{
    /// <summary>
    /// A key agnostic ID for a keyboard event that can be remapped;
    /// </summary>
    [Serializable]
    public class KeyMap
    {
        [SerializeField]
        private KeyCode _defaultKey;

        [SerializeField]
        private KeyCode _currentKey;

        [SerializeField]
        private KeyCode _alternateKey;

        [SerializeField]
        private string _keyId;

        public string id { get { return _keyId; } }

        public KeyCode GetKey() { return _currentKey; }

        public static KeyMap Create(string keyId, KeyCode defaultKey)
        {
            return Create(keyId, defaultKey, defaultKey);

        }
        public static KeyMap Create(string keyId, KeyCode defaultKey, KeyCode alternateKey)
        {
            KeyMap k = new KeyMap();
            k._keyId = keyId;
            k._defaultKey = defaultKey;
            k._alternateKey = alternateKey;
            k._currentKey = defaultKey;

            return k;
        }


    }
}