using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomPropertyDrawer(typeof(RangedFloat), true)]
public class RangedFloatDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        label = EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, label);

        SerializedProperty minProp = property.FindPropertyRelative("minValue");
        SerializedProperty maxProp = property.FindPropertyRelative("maxValue");

        float minValue = minProp.floatValue;
        float maxValue = maxProp.floatValue;

        float rangeMin = 0;
        float rangeMax = 1;

        if(minValue > 0 && rangeMin != 0)
        {
            rangeMin = 0;
        }
        else if (minValue < rangeMin)
        {
            rangeMin = minValue;
        }

        if(maxValue<1 && rangeMin != 1)
        {
            rangeMax = 1;
        }
        else if(maxValue > rangeMax)
        {
            rangeMax = maxValue;
        }

        var ranges = (MinMaxRangeAttribute[])fieldInfo.GetCustomAttributes(typeof(MinMaxRangeAttribute), true);
        if (ranges.Length > 0)
        {
            rangeMin = ranges[0].Min;
            rangeMax = ranges[0].Max;
        }

        const float rangeBoundsLabelWidth = 60f;

        position.x -= 20f;
        position.width += 20f;

        var maxLabelRect = new Rect(position);
        maxLabelRect.x = position.x + position.width - rangeBoundsLabelWidth - 5f;
        maxLabelRect.width = rangeBoundsLabelWidth;

        var minLabelRect = new Rect(position);
        minLabelRect.width = rangeBoundsLabelWidth;

        position.x += rangeBoundsLabelWidth;
        position.width -= rangeBoundsLabelWidth * 2;

        var sliderRect = new Rect(position);


        EditorGUI.BeginChangeCheck();

        minValue = EditorGUI.FloatField(minLabelRect, minValue);
        EditorGUI.MinMaxSlider(sliderRect, ref minValue, ref maxValue, rangeMin, rangeMax);
        maxValue = EditorGUI.FloatField(maxLabelRect, maxValue);

        if (EditorGUI.EndChangeCheck())
        {
            minProp.floatValue = minValue;
            maxProp.floatValue = maxValue;
        }

        EditorGUI.EndProperty();
    }
}