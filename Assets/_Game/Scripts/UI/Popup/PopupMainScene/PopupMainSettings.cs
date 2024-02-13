using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupMainSettings : PopupBase
{
    [SerializeField] private Button btnClose;
    [SerializeField] private Toggle toggleMusic;
    [SerializeField] private Toggle toggleEffect;
    private UserSettings userSettings;
    private AudioController audioController;
    public override void InitPopup()
    {
        userSettings = DBController.Instance.USER_SETTINGS;
        audioController = AudioController.Instance;
        InitBtnClose();
        IniToggleEffect();
        IniToggleMusic();
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

    private void IniToggleMusic()
    {
        toggleMusic.onValueChanged.RemoveAllListeners();
        toggleMusic.onValueChanged.AddListener(audioController.SetVolumeMusic);
    }

    private void IniToggleEffect()
    {
        toggleEffect.onValueChanged.RemoveAllListeners();
        toggleEffect.onValueChanged.AddListener(audioController.SetVolumeEffect);
    }
    public override void ShowPopup(UnityAction onShowComplete = null)
    {
        CheckStatusToggle();
        base.ShowPopup(onShowComplete);
    }

    private void CheckStatusToggle()
    {
        toggleMusic.isOn = userSettings.isMusic;
        toggleEffect.isOn = userSettings.isEffect;
    }
}
