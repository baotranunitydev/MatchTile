using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingSceneView : MonoBehaviour
{
    [SerializeField] private Image imgFadeUI;
    [SerializeField] private GameObject gobjLoading;
    [SerializeField] private Image imgFill;

    public void FadeOutBlackScreen()
    {
        imgFadeUI.DOFade(0, 0.5f).SetEase(Ease.Linear).OnComplete(() => { imgFadeUI.gameObject.SetActive(false); });
    }

    public void AnimationFillBar(UnityAction onCompleteFade = null)
    {
        imgFill.DOFillAmount(1f, 2f).OnComplete(() =>
        {
            onCompleteFade?.Invoke();
        });
    }

    public void ChangSceneAnimation(UnityAction onCompleteFade = null)
    {
        imgFadeUI.gameObject.SetActive(true);
        imgFadeUI.DOFade(1, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            onCompleteFade?.Invoke();
        });
    }

    public void DisableLoadingScreen()
    {
        gobjLoading.SetActive(false);
    }
}
