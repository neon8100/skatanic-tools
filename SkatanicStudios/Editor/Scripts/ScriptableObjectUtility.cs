using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ScriptableObjectUtility {

    public static T CreateAsset<T>() where T : ScriptableObject
    {
        return CreateAsset<T>(null);
    }

    /// <summary>
    //	This makes it easy to create, name and place unique new ScriptableObject asset files.
    /// </summary>
    public static T CreateAsset<T>(string assetName) where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T>();

        string path = "Assets/Scriptable Objects/Data/";
        if (assetName == null)
        {
            assetName = "New_" + asset.GetType().ToString();
        }
        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + assetName + ".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);

        Save();

        return asset;
    }

    public static T CreateAssetAt<T>(string pathName, string assetName) where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T>();

        string path = pathName;

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/" + assetName + ".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);

        Save();

        return asset;
    }




    public static void Save()
    {
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
    }

}

