using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEditor;

namespace SkatanicStudios.UI {
    [CustomEditor(typeof(ExtendedButton))]
    public class ExtendedButtonEditor : ButtonEditor
    {
        ExtendedButton instance;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            instance = (ExtendedButton)target;

            EditorGUILayout.LabelField("Extended Button", EditorStyles.boldLabel);
                
            EditorGUILayout.PropertyField(serializedObject.FindProperty("tweenOnMouseover"));

            if (instance.tweenOnMouseover)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("useTweener"));
                if (!instance.useTweener)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("time"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("scaleTo"));
                }
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("toggleGraphic"));
            if (instance.toggleGraphic)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_graphicToggleTarget"), true);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("graphicsToToggle"), true);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("toggleColor"), true);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
