using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System;

public class Builder : EditorWindow
{
    [MenuItem("Window/ScrapDev/Builder")]
    public static void ShowExample()
    {
        Builder wnd = GetWindow<Builder>();
        wnd.titleContent = new GUIContent("ScrapDev Builder");
    }

    private Button majorUp, minorUp, patchUp;
    private Button majorDown, minorDown, patchDown;
    private Toggle preRelease;
    private Button startBuild;

    private TextField major, minor, patch, build;


    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/3rd-party/Editor/Builder.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);

        //assign text fields
        major = root.Q<TextField>("major-text");
        minor = root.Q<TextField>("minor-text");
        patch = root.Q<TextField>("patch-text");
        build = root.Q<TextField>("build-text");

        //up buttons
        majorUp = root.Q<Button>("major-up");
        minorUp = root.Q<Button>("minor-up");
        patchUp = root.Q<Button>("patch-up");


        //down buttons
        majorDown = root.Q<Button>("major-down");
        minorDown = root.Q<Button>("minor-down");
        patchDown = root.Q<Button>("patch-down");


        //config buttons
        preRelease = root.Q<Toggle>("pre-release");
        startBuild = root.Q<Button>("build-button");

        //assign on-clicks
        majorUp.clicked += () => UpdateVersion(major, 1);
        minorUp.clicked += () => UpdateVersion(minor, 1);
        patchUp.clicked += () => UpdateVersion(patch, 1);

        majorDown.SetEnabled(false);
        minorDown.SetEnabled(false);
        patchDown.SetEnabled(false);

        startBuild.clicked += () => Build();

        major.RegisterValueChangedCallback((e) =>
        {
            ResetVersion(minor);
            ResetVersion(patch);
        });
        minor.RegisterValueChangedCallback((e) => ResetVersion(patch));
    }

    public void UpdateVersion(TextField versionSegment, int incrementBy = 0)
    {
        int v;
        try
        {
            v = int.Parse(versionSegment.value.Trim());
            v += incrementBy;

            if (v < 0)
            {
                v = 0;
            }
        }
        catch (Exception e)
        {
            v = 0;
            Debug.LogError(e);
        }

        versionSegment.value = v + "";
    }

    public void ResetVersion(TextField versionSegment)
    {
        versionSegment.value = "0";
    }

    public void Build()
    {
        string version = $"{major.text}.{minor.text}.{patch.text}";
        
    }
}