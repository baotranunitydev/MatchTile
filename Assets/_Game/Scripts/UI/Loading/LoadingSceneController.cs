using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneController : Singleton<LoadingSceneController>
{
    [SerializeField] private SceneType currentScene;
    [SerializeField] private LoadingSceneView loadingSceneView;
    private UnityAction callBackLoadScreen;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        Input.multiTouchEnabled = false;
        loadingSceneView.AnimationFillBar(() =>
        {
            ChangeScene(SceneType.MainScene, () => loadingSceneView.DisableLoadingScreen());
        });
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        callBackLoadScreen?.Invoke();
        callBackLoadScreen = loadingSceneView.FadeOutBlackScreen;
    }

    public void ChangeScene(SceneType sceneType, UnityAction onCompleteFadeIn = null)
    {
        currentScene = sceneType;
        loadingSceneView.ChangSceneAnimation(() =>
        {
            SceneManager.LoadScene($"{sceneType}");
            onCompleteFadeIn?.Invoke();
        });
    }
}
public enum SceneType
{
    Loading = 0,
    MainScene = 1,
    GamePlay = 2,
}