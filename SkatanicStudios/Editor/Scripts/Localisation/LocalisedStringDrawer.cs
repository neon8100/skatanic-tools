using UnityEngine;
using UnityEditor;

namespace SkatanicStudios.Localisation
{
    [CustomPropertyDrawer(typeof(LocalisedString))]
    public class LocalisedStringDrawer : PropertyDrawer
    {
        float height;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.isExpanded)
            {
                var value = TextLocalisation.GetLocalisedValue(property.FindPropertyRelative("key").stringValue);

                return height;
            } 


            return 21;
            
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Keyboard), label);
            position.height = 18;

            Rect foldoutRect = new Rect(position);
            foldoutRect.width = 25;
            property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, "", true);

            position.x += 35;
            position.width -= 75;

            TextLocaliserEditorGUI.TextPropertyField(position, property);

            if (property.isExpanded)
            {
                var value = TextLocalisation.GetLocalisedValue(property.FindPropertyRelative("key").stringValue);

                GUIStyle style = new GUIStyle(EditorStyles.wordWrappedLabel);

                var newHeight = style.CalcHeight(new GUIContent(value), position.width)+22;
                if(newHeight > height)
                {
                     height = newHeight;
                }

                position.height = height;
                position.y += 21;
                EditorGUI.LabelField(position, value, EditorStyles.wordWrappedLabel);
            }


            EditorGUI.EndProperty();
        }


    }
}