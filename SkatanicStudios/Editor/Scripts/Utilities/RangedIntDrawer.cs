using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(RangedInt))]
public class RangedIntDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        position.width -= 34;
        
        Rect controlWidth = new Rect(position);
        controlWidth.width = (controlWidth.width / 2);

        var min = property.FindPropertyRelative("min");
        EditorGUI.LabelField(controlWidth, "Min");
        controlWidth.x += 30;
        controlWidth.width -= 10;
        min.intValue = EditorGUI.IntField(controlWidth, min.intValue);

        controlWidth.x += controlWidth.width + 5;

        var max = property.FindPropertyRelative("max");
        EditorGUI.LabelField(controlWidth, "Max");
        controlWidth.x += 30;
        controlWidth.width -= 10;
        max.intValue = EditorGUI.IntField(controlWidth, max.intValue);
 
    }

}
