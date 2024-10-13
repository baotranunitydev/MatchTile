using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public sealed class LoadingSceneView : MonoBehaviour
{
    [SerializeField] private Image imgFadeUI;
    [SerializeField] private GameObject gobjLoading;
    [SerializeField] private Image imgFill;
    [SerializeField] private TextMeshProUGUI txtLoadingValue;
    [SerializeField] private TextMeshProUGUI txtVersion;
    private float cacheValue;

    public void SetVersion()
    {
        txtVersion.text = $"v{Application.version}";
    }

    public void FadeOutBlackScreen(float duration)
    {
        imgFadeUI.DOFade(0, duration).SetEase(Ease.Linear).OnComplete(() => { imgFadeUI.gameObject.SetActive(false); });
    }

    public void SetStatusLoading(bool status)
    {
        gobjLoading.gameObject.SetActive(status);
    }

    public async UniTask<bool> AnimationLoadByStepValue(int index, int total, float durationTotal,
        CancellationTokenSource cancellationTokenSource)
    {
        try
        {
            var timer = 0f;
            float value = index * 1f / total;
            var durationOneStep = durationTotal / total * 1f;
            var cacheInitValue = cacheValue <= 0 ? value : cacheValue;
            var timerLoading = durationOneStep * (value / cacheInitValue);
            var cacheValuePrevious = cacheValue;
            var valueNext = value - cacheValue;
            while (timer <= timerLoading)
            {
                await UniTask.Yield(cancellationToken: cancellationTokenSource.Token);
                timer += Time.deltaTime;
                cacheValue = cacheValuePrevious + (timer / timerLoading) * valueNext;
                imgFill.fillAmount = cacheValue;
                var per = Mathf.Clamp((cacheValue * 100), 0, 100);
                txtLoadingValue.text = $"Loading... {per:F0}%";
            }

            if (index == total)
            {
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    public async UniTask FadeIn(float durationFadeIn, UnityAction onCompleteFade = null)
    {
        imgFadeUI.gameObject.SetActive(true);
        await imgFadeUI.DOFade(1, durationFadeIn).SetEase(Ease.Linear);
        onCompleteFade?.Invoke();
    }

    public void ChangSceneAnimation(float duration, UnityAction onCompleteFade = null)
    {
        imgFadeUI.gameObject.SetActive(true);
        imgFadeUI.DOFade(1, duration).SetEase(Ease.Linear).OnComplete(() => { onCompleteFade?.Invoke(); });
    }
}