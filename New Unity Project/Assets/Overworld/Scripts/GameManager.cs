using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Overworld
{

    public enum GameProgress
    {
        Start = 0,
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                GameObject.DontDestroyOnLoad(this);
                SceneManager.LoadScene(1, LoadSceneMode.Single); // This only loads in bootstrapper which is index 0.
            }
            else
            {
                GameObject.Destroy(this);
            }
        }

        void SetProgress(GameProgress nextProgress)
        {
            if (nextProgress < Progress)
            {
                Debug.LogWarningFormat("Progess {0} is being set but current progress is larger {1}", nextProgress,
                    Progress);
            }

            Progress = nextProgress;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SaveGame()
        {
            Debug.Log("Saving game.");
            PlayerPrefs.SetInt(s_SceneIdKey, SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.SetInt(s_ProgressIdKey, (int) Progress);
        }

        public void LoadGame()
        {
            Debug.Log("Loading game.");
            int sceneId;
            GameProgress progress;
            if (!TryGetSavedSceneId(out sceneId) || !TryGetSavedProgressId(out progress))
            {
                Debug.LogError("Could not load game because settings were missing.");
                return;
            }

            Debug.Log($"Loaded Progress {progress} and Scene {sceneId} from save.");
            Progress = progress;
            SceneManager.LoadScene(sceneId, LoadSceneMode.Single);

            switch (Progress)
            {
                case GameProgress.Start:
                    break;
                // Add cases for each if the progress changes the world state somehow :)
            }
        }

        public static bool HasSavedGame()
        {
            int sceneId;
            GameProgress progress;
            return TryGetSavedSceneId(out sceneId) && !TryGetSavedProgressId(out progress);
        }

        private static bool TryGetSavedSceneId(out int sceneId)
        {
            sceneId = -1;
            if (PlayerPrefs.HasKey(s_SceneIdKey))
            {
                sceneId = PlayerPrefs.GetInt(s_SceneIdKey);
                return true;
            }

            return false;
        }

        private static bool TryGetSavedProgressId(out GameProgress progress)
        {
            progress = GameProgress.Start;
            if (PlayerPrefs.HasKey(s_ProgressIdKey))
            {
                progress = (GameProgress) PlayerPrefs.GetInt(s_ProgressIdKey);
                return true;
            }

            return false;
        }

        public static void DeleteSaveData()
        {
            PlayerPrefs.DeleteAll();
        }

        public GameProgress Progress { get; private set; }
        private const string s_SceneIdKey = "SceneId";
        private const string s_ProgressIdKey = "ProgressId";
    }
}