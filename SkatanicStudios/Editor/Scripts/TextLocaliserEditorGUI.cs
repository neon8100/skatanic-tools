using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SkatanicStudios.Localisation
{
    public class TextLocaliserEditorGUI
    {
        public static void TextPropertyField(Rect rect, SerializedProperty property, string propertyName = "key")
        {
            var focusId = EditorGUIUtility.GetControlID(FocusType.Passive);

            property.FindPropertyRelative(propertyName).stringValue = GUI.TextField(rect, property.FindPropertyRelative(propertyName).stringValue);
            

            rect.x += rect.width + 2;
            rect.width = 17;
            rect.height = 17;

            DrawSearchButton(rect, property);

            rect.x += rect.width + 2;
            rect.width = 17;
            rect.height = 17;

            DrawAddButton(rect, property.FindPropertyRelative(propertyName).stringValue);
            DrawEditButton(rect, property.FindPropertyRelative(propertyName).stringValue);

            CheckIsValid(property.FindPropertyRelative(propertyName).stringValue);
            property.serializedObject.ApplyModifiedProperties();
        }

        public static string TextField(Rect rect, string key)
        {
            var focusId = EditorGUIUtility.GetControlID(FocusType.Passive);

            key = GUI.TextField(rect, key);

            rect.x += rect.width + 2;
            rect.width = 17;
            rect.height = 17;

            DrawSearchButton(rect, null);

            rect.x += rect.width + 2;
            rect.width = 17;
            rect.height = 17;

            DrawAddButton(rect, key);
            DrawEditButton(rect, key);

            CheckIsValid(key);

            return key;
        }

        public static string TextField(string label, string key)
        {

            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            key = EditorGUILayout.TextField(label, key);

            DrawSearchButton();
            DrawAddButton(key);
            DrawEditButton(key);

            EditorGUILayout.EndHorizontal();

            if (CheckIsValid(key))
            {
                GUI.skin.textField.wordWrap = true;
                EditorGUILayout.LabelField(" ", TextLocalisation.GetLocalisedValue(key), EditorStyles.wordWrappedLabel, GUILayout.Height(50));
                GUI.skin.textField.wordWrap = false;
            }

            EditorGUILayout.EndVertical();

            return key;
        }

        static void DrawSearchButton()
        {
            Texture searchIcon = (Texture)Resources.Load("search");

            GUIContent _content = new GUIContent(searchIcon);
            
            if (GUILayout.Button(_content, GUILayout.MaxHeight(17), GUILayout.MaxWidth(17)))
            {
                TextLocaliserGUIEditorSearchWindow.Open();
            }

        }

        static void DrawSearchButton(Rect position, SerializedProperty property)
        {
            Texture searchIcon = (Texture)Resources.Load("search");

            GUIContent _content = new GUIContent(searchIcon);

            if (GUI.Button(position, _content))
            {
                TextLocaliserGUIEditorSearchWindow.Open(property);
            }

        }

        static void DrawAddButton(string key)
        {

            Texture searchIcon = (Texture)Resources.Load("store");

            GUIContent _content = new GUIContent(searchIcon);

            if (!CheckIsValid(key))
            {
                if (GUILayout.Button(_content, GUILayout.MaxHeight(17), GUILayout.MaxWidth(17)))
                {
                    if(!CheckIsValid(key))
                    {
                        TextLocaliserGUIEditorAlertWindow.Open(key);
                    }
                }
            }
        }

        static void DrawAddButton(Rect position, string key)
        {

            Texture searchIcon = (Texture)Resources.Load("store");

            GUIContent _content = new GUIContent(searchIcon);

            if (!CheckIsValid(key))
            {
                if (GUI.Button(position, _content))
                {
                    if(!CheckIsValid(key))
                    {
                        TextLocaliserGUIEditorAlertWindow.Open(key);
                    }
                }
            }
        }

        static void DrawEditButton(string key)
        {
            Texture searchIcon = (Texture)Resources.Load("options");

            GUIContent _content = new GUIContent(searchIcon);

            if (CheckIsValid(key))
            {
                if (GUILayout.Button(_content, GUILayout.MaxHeight(17), GUILayout.MaxWidth(17)))
                {
                    if (CheckIsValid(key))
                    {
                        TextLocaliserGUIEditorEditWindow.Open(key);
                    }
                }
            }
        }

        static void DrawEditButton(Rect position, string key)
        {
            Texture searchIcon = (Texture)Resources.Load("options");

            GUIContent _content = new GUIContent(searchIcon);

            if (CheckIsValid(key))
            {
                if (GUI.Button(position, _content))
                {
                    if (CheckIsValid(key))
                    {
                        TextLocaliserGUIEditorEditWindow.Open(key);
                    }
                }
            }
        }
    

        static bool CheckIsValid(string key)
        {
            if(key == null) { return false; }
            if(key == string.Empty) { return false; }
            if(key == "") { return false; }

            if (TextLocalisation.GetLocalisedValue(key) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public class TextLocaliserGUIEditorAlertWindow : EditorWindow
    {
        [MenuItem("Living The Deal/Localisation/Add Key")]
        public static void Open()
        {
            TextLocaliserGUIEditorAlertWindow window = new TextLocaliserGUIEditorAlertWindow();
            window.titleContent = new GUIContent("Localiser");
            window.ShowUtility();
        }

        public static void Open(string key)
        {
            TextLocaliserGUIEditorAlertWindow window = new TextLocaliserGUIEditorAlertWindow();
            window.titleContent = new GUIContent("Localiser");
            window.ShowUtility();
            window.key = key;
        }

        public string key;
        public string localisationValue;

        public static string DrawToolbar(string value)
        {
            EditorGUILayout.LabelField("Quick Tags", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            GUI.color = Color.yellow;
            if (GUILayout.Button("Yellow", GUILayout.MaxWidth(90)))
            {
                GUI.FocusControl(null);
                value += "<color=#f1c40f></color>";
            }
            GUI.color = Color.red;
            if (GUILayout.Button("Red", GUILayout.MaxWidth(90)))
            {
                GUI.FocusControl(null);
                value += "<color=#e74c3c></color>";
            }
            GUI.color = Color.green;
            if (GUILayout.Button("Green", GUILayout.MaxWidth(90)))
            {
                GUI.FocusControl(null);
                value += "<color=#2ecc71></color>";
            }

            GUI.color = (new Color32(52, 152, 219, 255));
            if (GUILayout.Button("Blue", GUILayout.MaxWidth(90)))
            {
                GUI.FocusControl(null);
                value += "<color=#3498db></color>";
            }


            GUI.color = Color.white;

            if (GUILayout.Button("Sprite", GUILayout.MaxWidth(90)))
            {
                GUI.FocusControl(null);
                value += "<sprite name=\"\">";
            }
            EditorGUILayout.EndHorizontal();
            return value;
        }

        public void OnGUI()
        {
            key = EditorGUILayout.TextField("Key: ", key);

            localisationValue = DrawToolbar(localisationValue);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Value:", GUILayout.MaxWidth(50));

            EditorStyles.textArea.wordWrap = true;
            localisationValue = EditorGUILayout.TextArea(localisationValue, EditorStyles.textArea, GUILayout.Height(180), GUILayout.Width(400));
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Add"))
            {
                TextLocalisation.AddValue(localisationValue, key);
                TextLocalisation.Refresh();
                Close();
            }

            minSize = new Vector2(460, 280);
            maxSize = minSize;
        }
    }

    public class TextLocaliserGUIEditorSearchWindow : EditorWindow
    {
        public static int currentFocus;
        public static string currentKey;
        public bool isDropdown;

        Dictionary<string, string> dic;

        [MenuItem("Living The Deal/Localisation/Search Values")]
        public static void OpenMenuItem()
        {
            TextLocaliserGUIEditorSearchWindow window = new TextLocaliserGUIEditorSearchWindow();
            window.titleContent = new GUIContent("Localiser Key Search");
            window.ShowUtility();
        }

        public static void Open(SerializedProperty property = null)
        {

            TextLocaliserGUIEditorSearchWindow window = new TextLocaliserGUIEditorSearchWindow();
            window.titleContent = new GUIContent("Localiser Key Search");

            Vector2 mouse = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
            Rect r = new Rect(mouse.x - 450, mouse.y + 10, 10, 10);

            window.ShowAsDropDown(r, new Vector2(500, 300));
            window.property = property;
            window.isDropdown = true;
        }

        public string value;
        public Vector2 scroll;

        private void OnEnable()
        {
            dic = TextLocalisation.GetDictionaryForEditor();
        }

        public void OnGUI()
        {
            EditorGUILayout.BeginHorizontal("Box");
            if (GUILayout.Button("Refresh", GUILayout.MaxWidth(60)))
            {
                AssetDatabase.Refresh();

                TextLocalisation.Refresh();

                dic = TextLocalisation.GetDictionaryForEditor();
            }
            EditorGUILayout.LabelField("Search: ", EditorStyles.boldLabel);
            value = EditorGUILayout.TextField(value);
            EditorGUILayout.EndHorizontal();


            GetSearchResults();
        }

        SerializedProperty property;


            void GetSearchResults()
            {
            if (value == null) { return; }
            Color c = GUI.color;
            EditorGUILayout.BeginVertical();
            scroll = EditorGUILayout.BeginScrollView(scroll);
            foreach (KeyValuePair<string, string> element in dic)
            {
                if (element.Key.ToLower().Contains(value.ToLower()) || element.Value.ToLower().Contains(value.ToLower()))
                {
                    EditorGUILayout.BeginHorizontal("box");

                    //Draw a delete button
                    Texture closeIcon = (Texture)Resources.Load("close");
                    Texture saveIcon = (Texture)Resources.Load("edit");
                    GUIContent _content = new GUIContent(closeIcon);

                    
                    GUI.color = Color.red;
                    if (GUILayout.Button(_content, GUILayout.MaxHeight(20), GUILayout.MaxWidth(20)))
                    {
                        if (EditorUtility.DisplayDialog("Remove Key " + element.Key + "?", "This will remove the element from localisation. Are you sure?", "Do It!"))
                        {
                            TextLocalisation.RemoveKey(element.Key);

                            AssetDatabase.Refresh();
                            
                            TextLocalisation.Refresh();

                            dic = TextLocalisation.GetDictionaryForEditor();
                            
                        }
                    }
                    GUI.color = c;

                     _content = new GUIContent(saveIcon);

                    if (GUILayout.Button(_content, GUILayout.MaxHeight(20), GUILayout.MaxWidth(20)))
                    {
                        TextLocaliserGUIEditorEditWindow.Open(element.Key);
                    }

                    EditorGUILayout.TextField(element.Key);
                    EditorGUILayout.LabelField(element.Value);

                    if (this.isDropdown)
                    {
                        GUI.color = Color.yellow;
                        Texture addIcon = (Texture)Resources.Load("store");
                        GUIContent _add = new GUIContent(addIcon);

                        if (GUILayout.Button(_add, GUILayout.MaxHeight(20), GUILayout.MaxWidth(20)))
                        {
                            try
                            {
                                GameObject obj = Selection.activeGameObject;

                                if (obj.GetComponent<UITextLocaliser>() != null)
                                {
                                    obj.GetComponent<UITextLocaliser>().key = element.Key;
                                }
                                else
                                {
                                    if (property != null)
                                    {
                                        if (property.FindPropertyRelative("key") != null)
                                        {
                                            property.FindPropertyRelative("key").stringValue = element.Key;
                                        }
                                        else if (property.FindPropertyRelative("text") != null)
                                        {
                                            property.FindPropertyRelative("text").stringValue = element.Key;
                                        }

                                        property.serializedObject.ApplyModifiedProperties();
                                    }
                                }
                            }
                            catch
                            {
                                if (property != null)
                                {
                                    if (property.FindPropertyRelative("key") != null)
                                    {
                                        property.FindPropertyRelative("key").stringValue = element.Key;
                                    }
                                    else if (property.FindPropertyRelative("text") != null)
                                    {
                                        property.FindPropertyRelative("text").stringValue = element.Key;
                                    }

                                    property.serializedObject.ApplyModifiedProperties();
                                }
                            }

                            Close();
                        }
                        GUI.color = c;
                    }

                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

    }

    public class TextLocaliserGUIEditorEditWindow :EditorWindow
    {
        public static void Open(string key)
        {
            TextLocaliserGUIEditorEditWindow window = new TextLocaliserGUIEditorEditWindow();
            window.titleContent = new GUIContent("Edit Value");
            window.ShowUtility();
            window.key = key;
            window.localisationValue = TextLocalisation.GetLocalisedValue(key);
        }

        public string key;
        public string localisationValue;

        public void OnGUI()
        {
            EditorGUILayout.LabelField("Key: ", key);

            localisationValue = TextLocaliserGUIEditorAlertWindow.DrawToolbar(localisationValue);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Value:", GUILayout.MaxWidth(50));

            EditorStyles.textArea.wordWrap = true;
            localisationValue = EditorGUILayout.TextArea(localisationValue, EditorStyles.textArea, GUILayout.Height(180), GUILayout.Width(400));
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Add"))
            {
                TextLocalisation.ReplaceValue(localisationValue, key);
                TextLocalisation.Refresh();
                Close();
            }

            minSize = new Vector2(460, 280);
            maxSize = minSize;
        }
    }

}
