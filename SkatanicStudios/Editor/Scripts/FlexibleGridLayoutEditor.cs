using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SkatanicStudios.UI
{
    [CustomEditor(typeof(FlexibleGridLayoutGroup))]
    public class FlexibleGridLayoutEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        }
    }
}
