using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental;
using UnityEngine;

namespace SkatanicStudios.Utilities
{
    [CustomPropertyDrawer(typeof(IDAttribute))]
    public class IDAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(property, label);
            if (GUILayout.Button((Texture)EditorGUIUtility.Load("arrow-down-bold.png"), GUILayout.MaxHeight(EditorGUIUtility.singleLineHeight), GUILayout.MaxWidth(35)))
            {
                property.stringValue = property.serializedObject.targetObject.name;
                property.serializedObject.ApplyModifiedProperties();
            }
            EditorGUILayout.EndHorizontal();
            //base.OnGUI(position, property, label);
        }
    }
}
