using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using SubjectNerd.Utilities;

[CreateAssetMenu(menuName = "Skatanic Studios/Steam Builder Config")]
public class SteamBuilderConfig : ScriptableObject
{
    public bool showDepots;

    public string appId;
    public string buildOutput = "..\\output\\";
    public string contentRoot = "..\\content\\";
    public string steamId;
    public string password;

    public bool preview;
    public string local;

    [Reorderable()]
    public List<SteamBuilderDepotSettings> depots = new List<SteamBuilderDepotSettings>();
}
