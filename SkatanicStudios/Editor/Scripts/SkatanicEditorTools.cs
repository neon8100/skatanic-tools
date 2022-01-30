using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditorInternal;
using System.Collections;

/// <summary>
/// A series of helper classes for dealing with serialized objects and re-orderable lists
/// </summary>
public class SkatanicEditorTools
{
    public static List<string> GetPropertyList(SerializedObject serializedObject, string propertyName)
    {
       
        List<string> items = new List<string>();
        items.Add(string.Empty);

        var list = serializedObject.FindProperty(propertyName);
        if (list.isArray)
        {
            var size = list.arraySize;
            foreach (SerializedProperty prop in list)
            {
                items.Add(prop.displayName);
            }
        }

        return items;
    }

    public static string DrawPropertyList(SerializedObject serializedObject, string propertyName, string label, string currentString)
    {
        var propertyList = GetPropertyList(serializedObject, propertyName);

        var index = propertyList.IndexOf(currentString);

        if(index < 0) { index = 0; }

        index = EditorGUILayout.Popup(label, index, propertyList.ToArray());

        return propertyList[index];
    }

    public static string DrawPropertyList(Rect r, SerializedObject serializedObject, string propertyName, string label, string currentString)
    {
        var propertyList = GetPropertyList(serializedObject, propertyName);
        var index = propertyList.IndexOf(currentString);
        index = EditorGUI.Popup(r, label, index, propertyList.ToArray());
        return propertyList[index];
    }

    /// <summary>
    /// Creates a reorderable list.
    /// </summary>
    /// <param name="baseObject"></param>
    /// <param name="listPropertyName"></param>
    /// <param name="idPropertyName"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public static ReorderableList CreateReorderableList(SerializedObject baseObject, string listPropertyName, string idPropertyName, string label)
    {
        return CreateReorderableList(baseObject, baseObject.FindProperty(listPropertyName), idPropertyName, label);
    }

    /// <summary>
    /// Creates a reorderable list.
    /// </summary>
    /// <param name="baseObject"></param>
    /// <param name="listProperty"> The serialized prop to create the list from</param>
    /// <param name="idPropertyName"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public static ReorderableList CreateReorderableList(SerializedObject baseObject, SerializedProperty listProperty, string idPropertyName, string label)
    {
        return CreateReorderableList(baseObject, listProperty, idPropertyName, label, false);
    }

    public static ReorderableList CreateReorderableList(SerializedObject baseObject, SerializedProperty listProperty, string idPropertyName, string label, bool drawProperty)
    {

        ReorderableList reorderableList = new ReorderableList(baseObject, listProperty, true, false, true, true);
        
        reorderableList.drawElementCallback = (rect, index, isActive, isFocused) =>
        {
            var element = reorderableList.serializedProperty.GetArrayElementAtIndex(index);
            var prop = element.FindPropertyRelative(idPropertyName);

            string name;

            if (prop.propertyType == SerializedPropertyType.Enum)
            {
                name = prop.enumNames[prop.enumValueIndex];
            }
            else
            {
                name = prop.stringValue;
            }


            string labelFormat = string.Format("{0} {1}: {2}", label, index, name);
            

            if (drawProperty)
            {
                
                foreach (SerializedProperty child in element.GetChildren())
                {

                    rect.height = EditorGUI.GetPropertyHeight(child);
                    EditorGUI.PropertyField(rect, child);
                    rect.y += rect.height;
                }


            }
            else
            {
                EditorGUI.LabelField(rect, labelFormat);
            }
        
        };

        return reorderableList;
    }


    public static ReorderableList CreateReorderablePropertyList(SerializedObject baseObject, string listPropertyName, SerializedObject idObject, string idListPropertyName, string label)
    {

        ReorderableList reorderableList = new ReorderableList(baseObject, baseObject.FindProperty(listPropertyName), true, false, true, true);

        reorderableList.drawElementCallback = (rect, index, isActive, isFocused) =>
        {
            var element = reorderableList.serializedProperty.GetArrayElementAtIndex(index);

            element.stringValue = DrawPropertyList(rect, idObject, idListPropertyName, label, element.stringValue);
        };

        return reorderableList;
    }


}