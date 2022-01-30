using SkatanicStudios.Localisation;
using SubjectNerd.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Localised String Collection")]
public class LocalisedStringCollection : ScriptableObject
{
    [Reorderable()]
    public List<LocalisedString> strings;

    public LocalisedString GetAtIndex(int index)
    {
        return strings[index];
    }
}
