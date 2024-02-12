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
        loadingSceneView.AnimationFillBar(() =>
        {
            ChangeScene(SceneType.MainScene);
        });
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        callBackLoadScreen?.Invoke();
        callBackLoadScreen = loadingSceneView.FadeOutBlackScreen;
    }

    public void ChangeScene(SceneType sceneType)
    {
        currentScene = sceneType;
        loadingSceneView.ChangSceneAnimation(() =>
        {
            //DOVirtual.DelayedCall(0.2f, () =>
            //{
            //    SceneManager.LoadScene($"{sceneType}");
            //});
            SceneManager.LoadScene($"{sceneType}");
        });
    }


}
public enum SceneType
{
    Loading = 0,
    MainScene = 1,
    GamePlay = 2,
}