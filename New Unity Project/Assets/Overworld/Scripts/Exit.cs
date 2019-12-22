using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Overworld
{


    public class ScenePicker : MonoBehaviour
    {
        [SerializeField] public string SceneNameToTransitionTo;

        [SerializeField] public string ScenePathToTransitionTo;
    }

    public class Exit : ScenePicker
    {
        public AudioSource TransitionSound = null; //Drag a reference to the audio source which will play the music.

        public void Transition()
        {
            if (TransitionSound != null)
            {
                TransitionSound.Play();
            }

            CameraManager.instance.Fade(false, TransitionScene);
        }

        private void TransitionScene()
        {
            //Load the last scene loaded, in this case Main, the only scene in the game.
            SceneManager.LoadScene(SceneNameToTransitionTo, LoadSceneMode.Single);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

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

}