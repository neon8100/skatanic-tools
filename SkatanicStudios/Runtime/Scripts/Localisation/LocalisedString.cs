using UnityEngine;
using System.Collections;

namespace SkatanicStudios.Localisation
{
    [System.Serializable]
    public struct LocalisedString 
    {
        public static string GetValue(string key)
        {
            return TextLocalisation.GetLocalisedValue(key);
        }

        public string key;

        public LocalisedString(string key)
        {
            this.key = key;
        }

        public string value
        {
            get
            {
                if (string.IsNullOrEmpty(key))
                {
                    return "";
                }

                return TextLocalisation.GetLocalisedValue(key);
            }
        }

        public static implicit operator LocalisedString(string value)
        {
            return new LocalisedString(value);
        }

        public static bool operator ==(LocalisedString localisedString, string key)
        {
            if(localisedString.key == key)
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(LocalisedString localisedString, string key)
        {
            if (localisedString.key != key)
            {
                return true;
            }
            return false;
        }

        public static bool operator ==(string key, LocalisedString localisedString)
        {
            if (localisedString.key == key)
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(string key, LocalisedString localisedString)
        {
            if (localisedString.key != key)
            {
                return true;
            }
            return false;
        }
    }
    
}
