using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.UI;
using System;
public enum PopupType
{
    PopupSettings = 0,
    PopupPause = 1,
    PopupWin = 2,
    PopupLose = 3,
    PopupBooster = 4,
    PopupWarning = 5,
}

public abstract class PopupBase : MonoBehaviour
{
    [SerializeField] private PopupType popupType;
    [SerializeField] private Image imgFade;
    [SerializeField] private RectTransform rtfmPopup;
    public Action onHide;
    private float timer = 0.5f;

    public PopupType PopupType { get => popupType; }

    public virtual void InitPopup()
    {
        imgFade.color = new Color(0, 0, 0, 0.8f);
        rtfmPopup.anchoredPosition = new Vector2(0, Screen.height * 2);
    }

    public virtual void ShowPopup(UnityAction onShowComplete = null)
    {
        SetStatusPopup(true);
        imgFade.DOFade(0.8f, timer);
        rtfmPopup.DOAnchorPosY(0, timer).OnComplete(() =>
        {
            onShowComplete?.Invoke();
        });
    }

    public virtual void HidePopup(UnityAction onHideComplete = null)
    {
        imgFade.DOFade(0, timer);
        rtfmPopup.DOAnchorPosY(Screen.height * 2, timer).OnComplete(() =>
        {
            SetStatusPopup(false);
            onHideComplete?.Invoke();
            onHide?.Invoke();
        });
    }

    private void SetStatusPopup(bool status)
    {
        rtfmPopup.gameObject.SetActive(status);
        imgFade.gameObject.SetActive(status);
    }
}
