using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Skatanic Studios/Steam Depot Settings")]
public class SteamBuilderDepotSettings : ScriptableObject
{
    public bool expand;
    public string depotId = "647551";
    public string contentRoot;
    public string localPath = "*";
    public string depotPath = ".";
    public string recursive = "1";
    public string fileExclusion = "*.pdb";
}