using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SkatanicStudios
{
    public class EditorSceneLoader : Editor
    {
        
        const string playFromFirstMenuStr = "Edit/Play Simulates Build &p";

        public static bool playFromFirstScene
        {
            get { return EditorPrefs.HasKey(playFromFirstMenuStr) && EditorPrefs.GetBool(playFromFirstMenuStr); }
            set { EditorPrefs.SetBool(playFromFirstMenuStr, value); }
        }

        // This method is called before any Awake. It's the perfect callback for this feature
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void LoadFirstSceneAtGameBegins()
        {

            if (!playFromFirstScene)
            {
                return;
            }

            playFromFirstScene = false;

            if (EditorBuildSettings.scenes.Length == 0)
            {
                Debug.LogWarning("The scene build list is empty. Can't play from first scene.");
                return;
            }

            foreach (GameObject go in Object.FindObjectsOfType<GameObject>())
                go.SetActive(false);

            SceneManager.LoadScene(0);
        }

        static void ShowNotifyOrLog(string msg)
        {
            if (Resources.FindObjectsOfTypeAll<SceneView>().Length > 0)
                EditorWindow.GetWindow<SceneView>().ShowNotification(new GUIContent(msg));
            else
                Debug.Log(msg); // When there's no scene view opened, we just print a log
        }
    }
}
