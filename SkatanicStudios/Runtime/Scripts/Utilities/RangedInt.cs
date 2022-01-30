using UnityEngine;
using System.Collections;
using System;

namespace SkatanicStudios
{
    /// <summary>
    /// Like a 2D Vector. A Ranged Int holds information about two values.
    /// </summary>
    [Serializable]
    public class RangedInt : MonoBehaviour
    {
        [SerializeField]
        public int min;
        [SerializeField]
        public int max;

        public RangedInt()
        {
            min = 0;
            max = 1;
        }

        public RangedInt(int minimumValue, int maximumValue)
        {
            min = minimumValue;
            max = maximumValue;
        }
    }

}