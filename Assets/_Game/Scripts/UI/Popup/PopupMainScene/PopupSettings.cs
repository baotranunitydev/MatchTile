using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupSettings : PopupBase
{

    [Header("Image")]
    [SerializeField] private Image imgToggleMusic;
    [SerializeField] private Image imgToggleSound;
    [Header("Button")]
    [SerializeField] private Button btnClose;
    [SerializeField] private Button btnMusic;
    [SerializeField] private Button btnSound;
    private UserSettings userSettings;
    private AudioController audioController;
    private Vector2 pivotLeft = new Vector2(0, 0.5f);
    private Vector2 pivotRight = new Vector2(1, 0.5f);
    public override void InitPopup()
    {
        userSettings = DBController.Instance.USER_SETTINGS;
        audioController = AudioController.Instance;
        InitBtnClose();
        InitButtonMusic();
        InitButtonSound();
        base.InitPopup();
    }

    private void InitBtnClose()
    {
        btnClose.onClick.RemoveAllListeners();
        btnClose.onClick.AddListener(() =>
        {
            HidePopup();
        });
    }


    private void InitButtonMusic()
    {
        btnMusic.onClick.RemoveAllListeners();
        btnMusic.onClick.AddListener(() =>
        {
            audioController.SetVolumeMusic(!userSettings.isMusic);
            SetStatusToggle(userSettings.isMusic, imgToggleMusic);
        });
    }

    private void InitButtonSound()
    {
        btnSound.onClick.RemoveAllListeners();
        btnSound.onClick.AddListener(() =>
        {
            audioController.SetVolumeSound(!userSettings.isSound);
            SetStatusToggle(userSettings.isSound, imgToggleSound);
        });
    }
    private void CheckStatusToggle()
    {
        SetStatusToggle(userSettings.isMusic, imgToggleMusic);
        SetStatusToggle(userSettings.isSound, imgToggleSound);

    }

    private void SetStatusToggle(bool toggle, Image imgToggle)
    {
        imgToggle.rectTransform.pivot = toggle ? pivotLeft : pivotRight;
        imgToggle.rectTransform.anchoredPosition = Vector2.zero;
    }

    public override void ShowPopup(UnityAction onShowComplete = null)
    {
        CheckStatusToggle();
        base.ShowPopup(onShowComplete);
    }
}
