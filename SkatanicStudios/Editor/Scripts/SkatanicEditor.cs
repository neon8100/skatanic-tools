using System;
using UnityEditor;
using UnityEngine;

public class SkatanicEditor:Editor
{
    /// <summary>
    /// Sets a property to find relative
    /// </summary>
    protected SerializedProperty property;

    protected void DrawField(string propName)
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty(propName), true);
    }

    protected void DrawFieldRelative(string propName)
    {
        if (property != null)
        {
            EditorGUILayout.PropertyField(property.FindPropertyRelative(propName), true);
        }
    }

    protected void DrawProperty(SerializedProperty prop)
    {
        foreach (SerializedProperty p in prop)
        {
            if (p.isArray && p.propertyType == SerializedPropertyType.Generic)
            {
                EditorGUILayout.BeginHorizontal();
                p.isExpanded = EditorGUILayout.Foldout(p.isExpanded, p.displayName);
                EditorGUILayout.EndHorizontal();

                if (p.isExpanded)
                {
                    EditorGUI.indentLevel++;
                    DrawProperty(p);
                    EditorGUI.indentLevel--;
                }
            }
            else
            {
                EditorGUILayout.PropertyField(p, true);
            }

        }
    }

    public bool GetBool(string propName, bool relative)
    {
        if (relative && property != null)
        {
            return property.FindPropertyRelative(propName).boolValue;
        }
        else if (serializedObject != null)
        {
            if (serializedObject.FindProperty(propName) != null)
            {
                return serializedObject.FindProperty(propName).boolValue;
            }
        }
        
        return false;
        
    }


    public int GetEnum(string propName, bool relative)
    {
        if (relative && property != null)
        {
            return property.FindPropertyRelative(propName).enumValueIndex;
        }
        else if(serializedObject!=null)
        {
            if (serializedObject.FindProperty(propName) != null)
            {
                return serializedObject.FindProperty(propName).enumValueIndex;
            }
        }
        
        return 0;
        
    }


    /// <summary>
    /// Apply the modified propeties;
    /// </summary>
    protected void Apply()
    {
        serializedObject.ApplyModifiedProperties();
    }
}
