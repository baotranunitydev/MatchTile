using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupGamePlaySettings : PopupBase
{
    [SerializeField] private Button btnClose;
    [SerializeField] private Toggle toggleMusic;
    [SerializeField] private Toggle toggleEffect;
    [SerializeField] private Button btnRestart;
    [SerializeField] private Button btnHome;
    private UserSettings userSettings;
    private AudioController audioController;

    public override void InitPopup()
    {
        userSettings = DBController.Instance.USER_SETTINGS;
        audioController = AudioController.Instance;
        InitBtnClose();
        InitBtnHome();
        InitBtnRestart();
        IniToggleMusic();
        IniToggleEffect();
        base.InitPopup();
    }
    private void InitBtnRestart()
    {
        btnRestart.onClick.RemoveAllListeners();
        btnRestart.onClick.AddListener(() =>
        {
            LoadingSceneController.Instance.ChangeScene(SceneType.GamePlay);
        });
    }

    private void InitBtnHome()
    {
        btnHome.onClick.RemoveAllListeners();
        btnHome.onClick.AddListener(() =>
        {
            LoadingSceneController.Instance.ChangeScene(SceneType.MainScene);
        });
    }


    private void InitBtnClose()
    {
        btnClose.onClick.RemoveAllListeners();
        btnClose.onClick.AddListener(() =>
        {
            GameManager.Instance.StateGame = StateGame.PlayGame;
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
