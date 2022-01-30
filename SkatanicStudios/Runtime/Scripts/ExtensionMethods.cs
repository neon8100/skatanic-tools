using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public static class ExtensionMethods
{

    public static void Destroy(this MonoBehaviour value)
    {
        if (value == null) { return; }
        GameObject.Destroy(value.gameObject);
    }
    public static void Destroy(this GameObject value)
    {
        GameObject.Destroy(value);
    }

    public static void RemoveAllChildren(this MonoBehaviour value)
    {
        foreach (Transform child in value.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public static void RemoveAllChildren(this Transform value)
    {
        foreach (Transform child in value)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public static void RemoveAllChildren(this Transform[] value)
    {
        foreach (Transform t in value)
        {
            RemoveAllChildren(t);
        }
    }

    public static void RemoveAllChildren(this List<Transform> value)
    {
        foreach (Transform t in value)
        {
            RemoveAllChildren(t);
        }
    }

    public static void RemoveAllChildren(this GameObject[] value)
    {
        foreach (GameObject t in value)
        {
            RemoveAllChildren(t.transform);
        }
    }

    public static void RemoveAllChildren(this List<GameObject> value)
    {
        foreach (GameObject t in value)
        {
            RemoveAllChildren(t.transform);
        }
    }

    public static void RemoveAllComponents(this Transform value)
    {

        foreach (Component t in value.GetComponents(typeof(MonoBehaviour)))
        {
            if (Application.isEditor) { GameObject.DestroyImmediate(t); }
            else { GameObject.Destroy(t); };
        }
    }

    public static void AddChild(this GameObject value, GameObject child)
    {
        child.SetParent(value);
    }

    public static void AddChild(this Transform value, GameObject child)
    {
        child.SetParent(value);
    }

    public static void AddChild(this Transform value, Transform child)
    {
        child.SetParent(value);
    }

    public static void SetParent(this MonoBehaviour value, GameObject parent)
    {
        value.gameObject.transform.SetParent(parent.transform, false);
    }

    public static void SetParent(this MonoBehaviour value, Transform parent)
    {
        value.gameObject.transform.SetParent(parent, false);
    }
    public static void SetParent(this GameObject value, GameObject parent)
    {
        value.transform.SetParent(parent.transform, false);
    }

    public static void SetParent(this GameObject value, Transform parent)
    {
        value.transform.SetParent(parent, false);
    }

    public static void SetParent(this GameObject value, GameObject parent, bool worldPosition)
    {
        value.transform.SetParent(parent.transform, worldPosition);
    }

    public static void SetParent(this GameObject value, Transform parent, bool worldPosition)
    {
        value.transform.SetParent(parent, worldPosition);
    }

    public static void SetPosition(this GameObject value, Vector3 position)
    {
        value.transform.position = position;
    }

    public static void Show(this GameObject value)
    {
        value.SetActive(true);
    }

    public static void Hide(this GameObject value)
    {
        value.SetActive(false);
    }


    public static void Show(this Component value)
    {
        value.gameObject.SetActive(true);
    }

    public static void Hide(this Component value)
    {
        value.gameObject.SetActive(false);
    }

    public static void Hide(this GameObject[] value)
    {
        foreach (GameObject obj in value)
        {
            obj.Hide();
        }
    }

    public static void HideChildren(this Transform value)
    {
        foreach (Transform obj in value)
        {
            obj.Hide();
        }
    }

    public static void ShowChildren(this Transform value)
    {
        foreach (Transform obj in value)
        {
            obj.Show();
        }
    }

    public static void Show(this GameObject[] value)
    {
        foreach (GameObject obj in value)
        {
            obj.Show();
        }
    }

    public static void Hide(this Component[] value)
    {
        foreach (Component obj in value)
        {
            obj.Hide();
        }
    }

    public static void Show(this Component[] value)
    {
        foreach (Component obj in value)
        {
            obj.Show();
        }
    }

    private static Transform _findInChildren(Transform trans, string name)
    {
        if (trans.name == name)
            return trans;
        else
        {
            Transform found;

            for (int i = 0; i < trans.childCount; i++)
            {
                found = _findInChildren(trans.GetChild(i), name);
                if (found != null)
                    return found;
            }

            return null;
        }
    }

    public static Transform FindInChildren(this Transform trans, string name)
    {
        return _findInChildren(trans, name);
    }

    /// <summary>
    /// Clamps an int between a min value and a max value.
    /// </summary>
    /// <returns></returns>
    public static int Clamp(this int value, int min, int max)
    {
        if (value > max)
        {
            value = max;
        }
        else if (value < min)
        {
            value = min;
        }

        return value;

    }

    public static float Round(this float value, float decimalPlaces)
    {
        var factor = 10 * decimalPlaces;

        return (Mathf.Round(value * (factor)) / factor);
    }

    /// <summary>
    /// Clamps a float between a min value and a max value.
    /// </summary>
    /// <returns></returns>
    public static float Clamp(this float value, float min, float max)
    {
        if (value > max)
        {
            value = max;
        }
        else if (value < min)
        {
            value = min;
        }

        return value;


    }

    public static Vector2 Clamp(this Vector2 value, Vector2 min, Vector2 max)
    {
        return new Vector2(value.x.Clamp(min.x, max.x), value.y.Clamp(min.y, max.y));
    }

    public static Vector3 Clamp(this Vector3 value, Vector3 min, Vector3 max)
    {
        return new Vector3(value.x.Clamp(min.x, max.x), value.y.Clamp(min.y, max.y), value.z.Clamp(min.z, max.z));
    }

    public static Vector2 Random(this Vector2 value, Vector2 min, Vector2 max)
    {
        float x = UnityEngine.Random.Range(min.x, max.x);
        float y = UnityEngine.Random.Range(min.y, max.y);
        return new Vector2(x, y);

    }

    public static Vector3 Random(this Vector3 value, Vector3 min, Vector3 max)
    {
        float x = UnityEngine.Random.Range(min.x, max.x);
        float y = UnityEngine.Random.Range(min.y, max.y);
        float z = UnityEngine.Random.Range(min.z, max.z);
        return new Vector3(x, y, z);

    }


    /// <summary>
    /// Clamps an int between 0 and a max value.
    /// </summary>
    /// <returns></returns>
    public static int ClampZero(this int value, int max)
    {
        if (value > max)
        {
            value = max;
        }
        else if (value < 0)
        {
            value = 0;
        }

        return value;
    }

    /// <summary>
    /// Clamps a float between 0 and a max value.
    /// </summary>
    /// <returns></returns>
    public static float ClampZero(this float value, float max)
    {
        if (value > max)
        {
            value = max;
        }
        else if (value < 0)
        {
            value = 0;
        }

        return value;
    }

    /// <summary>
    /// Clamps a float so it's not lower than 0
    /// </summary>
    /// <returns></returns>
    public static int Min(this int value)
    {
        if (value < 0)
        {
            value = 0;
        }

        return value;
    }
    /// <summary>
    /// Clamps a float so it's not lower than 0
    /// </summary>
    /// <returns></returns>
    public static float Min(this float value)
    {
        if (value < 0)
        {
            value = 0;
        }

        return value;
    }

    /// <summary>
    /// Wrap an int between a min and a max value. If the value exceeds the minimum or maximum, it will wrap around to the alternate value
    /// </summary>
    /// <returns></returns>
    public static int Wrap(this int value, int min, int max)
    {
        if (value > max)
        {
            value = min;
        }
        else if (value < 0)
        {
            value = max;
        }

        return value;
    }

    /// <summary>
    /// Wrap an float between a min and a max value. If the value exceeds the minimum or maximum, it will wrap around to the alternate value
    /// </summary>
    /// <returns></returns>
    public static float Wrap(this float value, float min, float max)
    {
        if (value > max)
        {
            value = min;
        }
        else if (value < 0)
        {
            value = max;
        }

        return value;
    }


    /// <summary>
    /// Wrap an int between 0 and a max value. If the value exceeds the minimum or maximum, it will wrap around to the alternate value
    /// </summary>
    /// <returns></returns>
    public static int WrapZero(this int value, int max)
    {
        if (value > max)
        {
            value = 0;
        }
        else if (value < 0)
        {
            value = max;
        }

        return value;
    }

    /// <summary>
    /// Wrap a float between 0 and a max value. If the value exceeds the minimum or maximum, it will wrap around to the alternate value
    /// </summary>
    /// <returns></returns>
    public static float WrapZero(this float value, float max)
    {
        if (value > max)
        {
            value = 0;
        }
        else if (value < 0)
        {
            value = max;
        }

        return value;
    }

    /// <summary>
    /// Returns a modified value of the original based on the mutliplier value
    /// </summary>
    /// <param name="value"></param>
    /// <param name="multiplier"></param>
    /// <returns></returns>
    public static float Modify(this float value, float multiplier)
    {
        return value * multiplier;

    }

    /// <summary>
    /// Returns a modified value of the original based on the mutliplier value
    /// </summary>
    /// <param name="value"></param>
    /// <param name="multiplier"></param>
    /// <returns></returns>
    public static int Modify(this int value, float multiplier)
    {
        return Mathf.RoundToInt(value * multiplier);
    }

    public static int ModifyWeighted(this int value, float multiplier, float weight)
    {

        float normalizedWeight = 1f - ((weight + 1f) / 2f);
        float inversedMultiplier = 2f - multiplier;
        Debug.Log($"{normalizedWeight * 100}% of {inversedMultiplier}");
        multiplier = Mathf.Lerp(multiplier, inversedMultiplier, normalizedWeight);
        Debug.Log($"Multiplier is {multiplier}");

        return Mathf.RoundToInt(value * multiplier);
    }

    public static string ToCoin(this int amount)
    {
        return amount.ToString("N0");
    }

    public static string AsNegative(this int amount)
    {
        return amount.ToString("-0");
    }
    public static string AsPositive(this int amount)
    {
        return amount.ToString("+0");
    }
    public static string ToSigned(this int amount)
    {
        return amount.ToString("+0; -0");
    }

    public static List<T> Clone<T>(this List<T> list)
    {
        List<T> clone = new List<T>();

        foreach (T item in list)
        {
            clone.Add(item);
        }

        return clone;
    }

    public static T[] CloneArray<T>(this T[] array)
    {
        T[] clone = new T[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            clone[i] = array[i];
        }

        return clone;
    }

    public static T GetRandom<T>(this List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            return default(T);
        }
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    public static T GetRandom<T>(this T[] list)
    {
        return list[UnityEngine.Random.Range(0, list.Length)];
    }

    public static void Add<T>(this List<T> list, List<T> itemsToAdd)
    {

        foreach (T item in itemsToAdd)
        {
            list.Add(item);
        }

    }

    public static void Iterate<T>(this List<T> list, Action<T> OnIterate)
    {
        foreach (T t in list)
        {
            OnIterate(t);
        }
    }

    public static void Iterate<T>(this T[] list, Action<T> OnIterate)
    {
        foreach (T t in list)
        {
            OnIterate(t);
        }
    }

    public static void IterateBackwards<T>(this List<T> list, Action<T> OnIterate)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            T t = list[i];

            OnIterate(t);
        }
    }

    public static void IterateBackwards<T>(this T[] list, Action<T> OnIterate)
    {
        for (int i = list.Length - 1; i >= 0; i--)
        {
            T t = list[i];

            OnIterate(t);
        }
    }

    public static T GetLast<T>(this List<T> list)
    {
        if (list != null)
        {
            return list[list.Count - 1];
        }
        return default(T);
    }

    public static T GetLast<T>(this T[] list)
    {
        if (list != null)
        {
            return list[list.Length - 1];
        }
        return default(T);
    }

    /// <summary>
    /// Move every item forward by one in an array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="myArray"></param>
    /// <returns></returns>
    public static T[] Shift<T>(this T[] myArray)
    {
        T[] tArray = new T[myArray.Length];
        for (int i = 0; i < myArray.Length; i++)
        {
            if (i < myArray.Length - 1)
            {
                tArray[i] = myArray[i + 1];
            }
            else
            {
                tArray[i] = myArray[0];
            }
        }
        return tArray;
    }

    /// <summary>
    /// Move every item forward by one in a list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="myList"></param>
    /// <returns></returns>
    public static List<T> Shift<T>(this List<T> myList)
    {
        return myList.Shift(false);
    }

    public static List<T> Shift<T>(this List<T> myList, bool removeFirst)
    {
        List<T> tList = new List<T>();
        for (int i = 0; i < myList.Count; i++)
        {
            if (i < myList.Count - 1)
            {
                tList.Add(myList[i + 1]);
            }
            else
            {
                if (!removeFirst)
                {
                    tList.Add(myList[0]);
                }
            }
        }
        return tList;
    }


    public static void SwapLayer(this GameObject obj, string layerName)
    {
        SwapLayer(obj, layerName, true);
    }

    public static void SwapLayer(this GameObject obj, string layerName, bool children)
    {
        obj.layer = LayerMask.NameToLayer(layerName);
        if (children)
        {
            foreach (Transform t in obj.transform)
            {
                SwapLayer(t.gameObject, layerName, children);
            }
        }
    }
#if UNITY_EDITOR
    public static IEnumerable<SerializedProperty> GetChildren(this SerializedProperty property)
    {

        property = property.Copy();
        var nextElement = property.Copy();

        bool hasNextElement = nextElement.NextVisible(false);
        if (!hasNextElement)
        {
            nextElement = null;
        }

        property.NextVisible(true);
        while (true)
        {
            if ((SerializedProperty.EqualContents(property, nextElement)))
            {
                yield break;
            }

            yield return property;

            bool hasNext;

            hasNext = property.NextVisible(false);

            if (!hasNext)
            {
                break;
            }
        }
    }
#endif





}
