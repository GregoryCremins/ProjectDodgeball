using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenePicker : MonoBehaviour
{
    [SerializeField]
    public string SceneNameToTransitionTo;

    [SerializeField]
    public string ScenePathToTransitionTo;
}

public class GoToScene : ScenePicker
{
    public Button b;
    // Start is called before the first frame update
    void Start()
    {
        b = gameObject.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    [SerializeField]
    public void GoToSceneNow()
    {
        SceneManager.LoadScene(SceneNameToTransitionTo, LoadSceneMode.Single);
    }
}

[CustomEditor(typeof(ScenePicker), true)]
public class ScenePickerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var picker = target as ScenePicker;
        if (picker == null)
        {
            return;
        }

        var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(picker.ScenePathToTransitionTo);

        serializedObject.Update();

        EditorGUI.BeginChangeCheck();
        var newScene =
            EditorGUILayout.ObjectField("Scene To Transition To", oldScene, typeof(SceneAsset),
                false) as SceneAsset;

        if (EditorGUI.EndChangeCheck())
        {
            var newPath = AssetDatabase.GetAssetPath(newScene);
            var scenePathProperty = serializedObject.FindProperty("ScenePathToTransitionTo");
            scenePathProperty.stringValue = newPath;
            var sceneNameProperty = serializedObject.FindProperty("SceneNameToTransitionTo");
            sceneNameProperty.stringValue = newScene?.name;
        }
        serializedObject.ApplyModifiedProperties();
    }
}
