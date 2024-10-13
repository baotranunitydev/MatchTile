using Sirenix.OdinInspector;
using System;
using System.Threading;
using Ultils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


namespace Loading
{
    public enum SceneType
    {
        None = 0,
        Loading = 1,
        MainScene = 2,
        GamePlay = 3,
    }

    [Serializable]
    public struct LoadingSceneModel
    {
        public float durationLoading;
        public float durationFade;
    }

    public sealed class LoadingSceneController : MonoBehaviour
    {
        [SerializeField] private SceneType currentScene;
        [SerializeField] private LoadingSceneModel model;
        [SerializeField] private LoadingSceneView loadingSceneView;
        public static LoadingSceneController Instance;
        public SceneType CurrentScene => currentScene;

        private CancellationTokenSource CancellationTokenSource;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            LoadingDataController.onDoneIndexLoad += AnimationLoadingByStep;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            LoadingDataController.onDoneIndexLoad -= AnimationLoadingByStep;
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            loadingSceneView.FadeOutBlackScreen(model.durationFade);
        }

        private void CancleToken()
        {
            if (CancellationTokenSource != null)
            {
                CancellationTokenSource.Cancel();
                CancellationTokenSource.Dispose();
            }
        }

        private void Start()
        {
            Application.targetFrameRate = 60;
            Input.multiTouchEnabled = false;
            loadingSceneView.SetVersion();
        }

        [Button]
        public async void AnimationLoadingByStep(int index, int total)
        {
            CancleToken();
            CancellationTokenSource = new CancellationTokenSource();
            var isDone = await loadingSceneView.AnimationLoadByStepValue(index, total, model.durationLoading, CancellationTokenSource);
            if (isDone)
            {
                ChangeScene(SceneType.MainScene, () =>
                {
                    loadingSceneView.SetStatusLoading(false);
                });
            }
        }
        public void ChangeScene(SceneType sceneType, UnityAction onCompleteFadeIn = null)
        {
            currentScene = sceneType;
            loadingSceneView.ChangSceneAnimation(model.durationFade, () =>
            {
                SceneManager.LoadScene($"{sceneType}");
                onCompleteFadeIn?.Invoke();
            });
        }
    }
}
