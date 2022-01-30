using SkatanicStudios.Localisation;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UITextLocaliser))]
public class UITextLocaliserEditor : Editor
{

    public bool gotKey;

    UITextLocaliser instance;

    private void OnEnable()
    {
        instance = (UITextLocaliser)target;
        CheckIsValid();
    }

    public override void OnInspectorGUI()
    {
        instance.GetComponent<TextMeshProUGUI>().text = (instance.preview && gotKey) ? TextLocalisation.GetLocalisedValue(instance.key) : instance.key;

        EditorGUILayout.BeginHorizontal();

        EditorGUI.BeginChangeCheck();
        instance.key = TextLocaliserEditorGUI.TextField("Localiser Key", instance.key);

        if (EditorGUI.EndChangeCheck())
        {
            CheckIsValid();
            EditorUtility.SetDirty(target);
        }

        EditorGUILayout.EndHorizontal();

        instance.preview = EditorGUILayout.Toggle("Preview?", instance.preview);
    }

    void CheckIsValid()
    {

        if (TextLocalisation.GetLocalisedValue(instance.key) == null)
        {
            gotKey = false;
        }
        else
        {
            gotKey = true;
        }
    }



}


