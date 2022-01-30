using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SubjectNerd.Utilities;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(LocalisationPropertyReference))]
public class LocalisationPropertyReferenceDrawer : PropertyDrawer
{
    List<LocalisationPropertyAttribute> propertyAttributes = new List<LocalisationPropertyAttribute>();
    List<string> displayNames = new List<string>();

    bool init = false;
    SerializedProperty prop;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (!init)
        {
            prop = property;
            GetProperties();
        }

        EditorGUI.BeginProperty(position, label, property);

        int index = property.FindPropertyRelative("index").intValue;
        //Load of the localisation attribute
        property.FindPropertyRelative("index").intValue = EditorGUI.Popup(position, "Property:", index, displayNames.ToArray());
        property.FindPropertyRelative("displayName").stringValue = propertyAttributes[index].displayName;
        property.FindPropertyRelative("referenceType").stringValue = propertyAttributes[index].type.Name;

        EditorGUI.EndProperty();
    }

    private void GetProperties()
    {
        Type[] allowedTypes = new Type[0];

        //Check if there's an allowed reference type on this property
        if (prop.HasAttribute<AllowedReferenceTypesAttribute>())
        {
            allowedTypes = ((AllowedReferenceTypesAttribute)prop.GetAttribute<AllowedReferenceTypesAttribute>()).types;
        }
        else if (prop.GetParentProp().isArray)
        {
            allowedTypes = ((AllowedReferenceTypesAttribute)prop.GetParentProp().GetParentProp().GetAttribute<AllowedReferenceTypesAttribute>()).types;
        }

        var typeFilter = allowedTypes.ToList();

        //Get all classes that have a property reference
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (Type type in assembly.GetTypes())
            {
                //Get all the properties on this type
                if (!typeFilter.Contains(type)) { continue; }

                var props = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic);

                foreach (PropertyInfo prop in props)
                {
                    if (Attribute.IsDefined(prop, typeof(LocalisationPropertyAttribute)))
                    {
                        LocalisationPropertyAttribute attr = (LocalisationPropertyAttribute)prop.GetCustomAttribute(typeof(LocalisationPropertyAttribute));
                        propertyAttributes.Add(attr);
                    }

                }

                var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where((field) => Attribute.IsDefined(field, typeof(LocalisationPropertyAttribute)));
                foreach (FieldInfo field in fields)
                {
                    LocalisationPropertyAttribute attr = (LocalisationPropertyAttribute)field.GetCustomAttribute(typeof(LocalisationPropertyAttribute));
                    propertyAttributes.Add(attr);


                }
            }

            foreach (LocalisationPropertyAttribute attribute in propertyAttributes)
            {
                displayNames.Add(attribute.displayName);
            }

            init = true;
        }
    }
}
