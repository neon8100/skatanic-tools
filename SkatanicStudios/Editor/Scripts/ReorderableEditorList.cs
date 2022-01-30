using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;
using UnityEditor;
using System;

namespace SkatanicStudios.EditorTools
{
    public class EditorGUIReorderableList<T>
    {
        public ReorderableList list;

        T selectedItem;

        public IList<T> listElements;
        public Action<T> onSelect;
        public Func<int, string> onRequestName;
        public Action<Rect, int, bool, bool> onDrawOverride;

        public static  EditorGUIReorderableList<T> Create(IList listElements)
        {
            
            EditorGUIReorderableList<T> item = new EditorGUIReorderableList<T>();
            item.list = new ReorderableList(listElements, typeof(T), true, false, true, true);
            item.listElements = (IList<T>)listElements;
            item.list.onSelectCallback = item.OnSelectCallback;
            item.list.drawElementCallback = item.DrawElementCallback;
            return item;
        }

        public void Draw()
        {
            if (list != null && list.drawElementCallback!=null)
            {
                list.DoLayoutList();
            }
        }

        void OnSelectCallback(ReorderableList list)
        {
            if (onSelect != null)
            {
                onSelect(listElements[list.index]);
            }
        }

        void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
        {
            if (onDrawOverride != null)
            {
                onDrawOverride(rect, index, isActive, isFocused);
            }
            else
            {
                string name = onRequestName(index);
                EditorGUI.LabelField(rect, name);
            }

        }
    }

    public class EditorGUIList
    {

        public static string SearchableList(string label, string id, IList list, Func<int,string> listNameCallback, Action<string> onSelect)
        {
            string itemId = id;

            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(label, EditorStyles.boldLabel, GUILayout.ExpandWidth(false));
                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField(id);
            EditorGUILayout.EndHorizontal();
            

            if (GUILayout.Button("Select"))
            {
                EditorGUISearchableListPopupContent content = new EditorGUISearchableListPopupContent();
                EditorGUISearchableListPopupContent.currentString = id;
                content.list = list;
                content.drawCallback = listNameCallback;
                content.onClose = onSelect;
                Vector2 mouse = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
                Rect r = new Rect(mouse.x - 450, mouse.y + 10, 10, 10);
                
                content.ShowAsDropDown(r, new Vector2(450, 200));
            }
            EditorGUILayout.EndHorizontal();

            return itemId;
        }

        public static List<string> StringList(string label, List<string> list)
        {
            if (list == null)
            {
                list = new List<string>();
            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
            if (GUILayout.Button("Add", GUILayout.MaxWidth(50)))
            {
                list.Add(string.Empty);
            }
            
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical("box");
            for (int i = list.Count - 1; i >= 0; i--)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("X", GUILayout.MaxWidth(25)))
                {
                    list.RemoveAt(i);
                }

                list[i] = EditorGUILayout.TextField(list[i]);

                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndVertical();

            return list;
        }

        public static IList EnumList(string label, IList list, Type type) 
        {
            

            if(list == null)
            {
                list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(type));
            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
            if (GUILayout.Button("Add", GUILayout.MaxWidth(50)))
            {
                //This should cast a new enum of type;
                var enumVal = Enum.ToObject(type, 0);
                var listType = Convert.ChangeType(enumVal, type);

                list.Add(listType);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical("box");
            for (int i = list.Count - 1; i >= 0; i--)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("X", GUILayout.MaxWidth(25)))
                {
                    list.RemoveAt(i);
                }

                list[i] = Convert.ChangeType(EditorGUILayout.EnumPopup((Enum)list[i]), type);

                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndVertical();

            return list;
        }

        public static List<T> ObjectList<T>(string label, List<T> list) where T:UnityEngine.Object
        {
            if (list == null)
            {
                list = (List<T>)Activator.CreateInstance(typeof(List<T>));
            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
            if (GUILayout.Button("Add", GUILayout.MaxWidth(50)))
            {
                //This should cast a new enum of type;
                list.Add(null);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical("box");
            for (int i = list.Count - 1; i >= 0; i--)
            {
                EditorGUILayout.BeginHorizontal();

                list[i] = (T)EditorGUILayout.ObjectField(list[i], typeof(T), false);
                if (GUILayout.Button("X", GUILayout.MaxWidth(25)))
                {
                    list.RemoveAt(i);
                }

                //list[i] = Convert.ChangeType(EditorGUILayout.EnumPopup((Enum)list[i]), type);

                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndVertical();

            return list;

        }
    }

    public class EditorGUISearchableListPopupContent : EditorWindow
    {
        public static string currentString = "";

        public IList list;
        public Func<int, string> drawCallback;
        public Action<string> onClose;

        public void OnGUI()
        {
            currentString = EditorGUILayout.TextField("Search: ", currentString);
            EditorGUILayout.LabelField("Inventory Items", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("Box");

            bool found = false;

            for (int i = 0; i < list.Count; i++)
            {
                string id = drawCallback(i);
                if (id.Contains(currentString))
                {
                    found = true;

                    EditorGUILayout.BeginHorizontal("box");
                    if (GUILayout.Button(id, EditorStyles.whiteLabel))
                    {
                        onClose(id);
                        Close();
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }

            if (!found)
            {
                EditorGUILayout.LabelField("No Elements Found", EditorStyles.largeLabel);
            }
            EditorGUILayout.EndVertical();
        }
        
    }
        
}
