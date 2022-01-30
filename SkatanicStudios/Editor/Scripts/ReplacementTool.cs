using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReplacementTool : EditorWindow
{
    [MenuItem("Tools/Replacement Tool")]
    public static void Open()
    {
        GetWindow<ReplacementTool>();
    }

    public GameObject prefabToUse;
    public string name;
    public bool replaceAll;

    private void OnGUI()
    {

        prefabToUse = EditorGUILayout.ObjectField("Prefab To Use", prefabToUse, typeof(GameObject), false) as GameObject;
        name = EditorGUILayout.TextField("Object Name to Replace", name);

        GameObject[] selections = Selection.gameObjects;

        if (selections != null) {
            EditorGUILayout.LabelField("In Game Objects:" + selections.Length);

            if(GUILayout.Button("Go"))
            {
                if (selections.Length>1)
                {
                    ReplaceObjects(selections);
                }
                else
                {
                    ReplaceObject(selections[0].transform);
                }
            }
        }
        else
        {
            EditorGUILayout.LabelField("Select A GameObject");
        }

    }

    void ReplaceObjects(GameObject[] selections)
    {

        foreach(GameObject obj in selections)
        {
            ReplaceObject(obj.transform);
        }
    }

    void ReplaceObject(Transform transform)
    {
        Transform parent = transform.parent;

        Vector3 localPosition = transform.localPosition;
        Quaternion localRotation = transform.localRotation;
        Vector3 localScale = transform.localScale;

        GameObject.DestroyImmediate(transform.gameObject);

        GameObject newObject = (GameObject)PrefabUtility.InstantiatePrefab(prefabToUse, parent);
        newObject.transform.localPosition = localPosition;
        newObject.transform.localRotation = localRotation;
        newObject.transform.localScale = localScale;

    }
}
