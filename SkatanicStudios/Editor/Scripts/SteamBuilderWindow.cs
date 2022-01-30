using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

public class SteamBuilderWindow : EditorWindow
{
    public static void Open()
    {
        SteamBuilderWindow window = GetWindow<SteamBuilderWindow>();
        window.titleContent = new GUIContent(" Steam Builder", (Texture)EditorGUIUtility.Load("steam_w.png"), "Steam Builder");
    }

    public string steamBuildToolsFolder = "G:\\Projects\\Unity Projects\\Skatanic Studios\\steam-sdk\\tools\\ContentBuilder";

    [Multiline()]
    public string buildDescription;
    public int setLiveOn;
    public SteamBuilderConfig config;

    public static string[] branches = { "none", "default", "test"};

    private void OnEnable()
    {
        //Check if there's a config file
        var configFilePath = AssetDatabase.FindAssets("t:SteamBuilderConfig");
        if (configFilePath.Length > 0)
        {
            UnityEngine.Debug.Log(configFilePath[0]);
            
            config = AssetDatabase.LoadAssetAtPath<SteamBuilderConfig>(AssetDatabase.GUIDToAssetPath(configFilePath[0]));
        }
        else
        {
            config = ScriptableObjectUtility.CreateAssetAt<SteamBuilderConfig>("Assets/_SkatanicStudios", "Steam Config");
            AssetDatabase.Refresh();
        }

        buildDescription = "v" + Application.version;
    }

    bool expandConfig;
    SerializedObject serializedConfig;
    public void OnGUI()
    {
        EditorGUILayout.BeginVertical("Box");
        config = (SteamBuilderConfig)EditorGUILayout.ObjectField("Config Asset", config, typeof(SteamBuilderConfig), false);
        serializedConfig = new SerializedObject(config);
        expandConfig = EditorGUILayout.Foldout(expandConfig, "Steam Builder Configuration");
        if (expandConfig)
        {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.PropertyField(serializedConfig.FindProperty("appId"));
            EditorGUILayout.PropertyField(serializedConfig.FindProperty("steamId"));
            EditorGUILayout.PropertyField(serializedConfig.FindProperty("password"));
            EditorGUILayout.PropertyField(serializedConfig.FindProperty("preview"));
            EditorGUILayout.PropertyField(serializedConfig.FindProperty("local"));
            EditorGUILayout.EndVertical();

            serializedConfig.ApplyModifiedProperties();
        }

        DrawDepots();

        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Build Description", EditorStyles.boldLabel);
        buildDescription = EditorGUILayout.TextArea(buildDescription, GUILayout.Height(45));
        setLiveOn = EditorGUILayout.Popup("Set Live On", setLiveOn, branches);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        steamBuildToolsFolder = EditorGUILayout.TextField("Steam Build Tools Path:", steamBuildToolsFolder);
        if (GUILayout.Button("Find"))
        {
            steamBuildToolsFolder = EditorUtility.OpenFolderPanel("Select Steam Build Tools Folder", "", "");
        }
        EditorGUILayout.EndHorizontal();


        DrawButtons();
    }

    void DrawButtons()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Upload to Steam"))
        {
            UploadToSteam();
        }
        EditorGUILayout.EndHorizontal();
    }



    void DrawDepots()
    {


        EditorGUI.BeginChangeCheck();
        SerializedProperty prop = serializedConfig.FindProperty("depots");
        EditorGUILayout.BeginHorizontal();
        config.showDepots = EditorGUILayout.Foldout(config.showDepots, $"Depots ({config.depots.Count})");
        if(GUILayout.Button("Add New Depot"))
        {
            var depot = ScriptableObjectUtility.CreateAssetAt<SteamBuilderDepotSettings>("Assets/_SkatanicStudios", "Steam Depot");
            AssetDatabase.Refresh();
            //Create a new depot asset
            config.depots.Add(depot);
        }
        EditorGUILayout.EndHorizontal();

        if (config.showDepots)
        {
            for (int i = 0; i < config.depots.Count; i++)
            {
                EditorGUILayout.Space();
                EditorGUI.indentLevel++;
                EditorGUILayout.BeginVertical("Box");
                config.depots[i] = (SteamBuilderDepotSettings)EditorGUILayout.ObjectField($"Depot #{config.depots[i].depotId}", config.depots[i], typeof(SteamBuilderDepotSettings), false);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.TextField("Content Location", config.depots[i].contentRoot);
                if (GUILayout.Button("Set"))
                {
                    config.depots[i].contentRoot = EditorUtility.OpenFolderPanel("Select Content Root", "", "Living The Deal");
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
                EditorGUI.indentLevel--;
            }
        }


        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(config);
        }
    }



    public void UploadToSteam()
    {
        string scriptsFolder = string.Format("{0}/scripts/", steamBuildToolsFolder);

        foreach (SteamBuilderDepotSettings depot in config.depots)
        {
            string depotIdFilePath = string.Format("{0}depot_build_{1}.vdf", scriptsFolder, depot.depotId);

            StreamWriter depotWriter = new StreamWriter(depotIdFilePath, false);

            string depotString = "\"DepotBuildConfig\"{" +
                "\"DepotID\" \"" + depot.depotId + "\"" +
                "\"ContentRoot\" \"" + depot.contentRoot + "\"" +
                "\"FileMapping\" {" +
                "\"LocalPath\" \"" + depot.localPath + "\"" +
                "\"DepotPath\" \"" + depot.depotPath + "\"" +
                "\"recursive\" \"" + depot.recursive + "\"" +
                "}" +
                "\"FileExclusion\" \""+depot.fileExclusion+"\"" +
                "}";

            depotWriter.Write(depotString);
            depotWriter.Close();
        }

        string appIdFilePath = string.Format("{0}app_build_{1}.vdf", scriptsFolder, config.appId);


        StreamWriter writer = new StreamWriter(appIdFilePath, false);
        string depotsLine = "{\n";
        foreach(SteamBuilderDepotSettings depot in config.depots)
        {
            string depotString = "\"" + depot.depotId + "\" \"depot_build_" + depot.depotId + ".vdf\"";
            depotsLine += depotString + "\n";
        }
        depotsLine += "\n}";

        var branch = "";
        if (setLiveOn > 0)
        {
            branch = branches[setLiveOn];
        }
        string appIdLine = "\"appbuild\"" +
            "{\n" +
            "\"appid\" \""+ config.appId + "\"\n" +
            "\"desc\" \""+buildDescription+"\"\n" +
            "\"buildoutput\" \""+config.buildOutput+"\"\n" +
            "\"contentroot\" \""+ config.contentRoot +"\"\n" +
            "\"setlive\" \""+branch+"\"\n" +
            "\"preview\" \""+ config.preview.ToString()+"\"\n" +
            "\"local\" \""+ config.local +"\"\n\n" +
            "\"depots\""+depotsLine+"\n" +
            "}";

        writer.Write(appIdLine);
        writer.Close();
        

        //Open steam command and load the game
        StreamWriter runSteamCmdBatch = new StreamWriter(steamBuildToolsFolder + "/run_build.bat");
        runSteamCmdBatch.Write($"builder\\steamcmd.exe +login {config.steamId} {config.password} +run_app_build_http ..\\scripts\\app_build_{config.appId}.vdf");
        runSteamCmdBatch.Close();

        UnityEngine.Debug.Log(steamBuildToolsFolder);
        ProcessStartInfo info = new ProcessStartInfo("run_build.bat");
        info.WorkingDirectory = steamBuildToolsFolder;
        Process.Start(info);
        
    }
}

