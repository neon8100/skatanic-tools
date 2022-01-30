using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkatanicStudios.Localisation
{
    public class TextLocalisationManager : MonoBehaviour
    {

        public static TextLocalisationManager instance;
        public static string Replace(string value)
        {
            return instance.OnGetLocalisedValue(value);
        }

        public void Awake()
        {
            instance = this;
        }

        public virtual string OnGetLocalisedValue(string value) { return value; }
    }
}
