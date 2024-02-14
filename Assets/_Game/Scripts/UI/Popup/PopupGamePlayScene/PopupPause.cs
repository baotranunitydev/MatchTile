using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupPause : PopupBase
{
    [Header("Image")]
    [SerializeField] private Image imgToggleMusic;
    [SerializeField] private Image imgToggleSound;
    [SerializeField] private Image imgToggleVibrate;
    [Header("Button")]
    [SerializeField] private Button btnClose;
    [SerializeField] private Button btnMusic;
    [SerializeField] private Button btnSound;
    [SerializeField] private Button btnVibrate;
    [SerializeField] private Button btnHome;
    [SerializeField] private Button btnRestart;
    private UserSettings userSettings;
    private AudioController audioController;
    private VibrateController vibrateController;
    private Vector2 pivotLeft = new Vector2(0, 0.5f);
    private Vector2 pivotRight = new Vector2(1, 0.5f);
    public override void InitPopup()
    {
        userSettings = DBController.Instance.USER_SETTINGS;
        audioController = AudioController.Instance;
        vibrateController = VibrateController.Instance;
        InitBtnClose();
        InitBtnHome();
        InitBtnRestart();
        InitButtonMusic();
        InitButtonSound();
        InitButtonVibrate();
        base.InitPopup();
    }
    private void InitBtnRestart()
    {
        btnRestart.onClick.RemoveAllListeners();
        btnRestart.onClick.AddListener(() =>
        {
            vibrateController.Vibrate();
            audioController.PlaySound(SoundName.ClickBtn);
            LoadingSceneController.Instance.ChangeScene(SceneType.GamePlay);
        });
    }

    private void InitBtnHome()
    {
        btnHome.onClick.RemoveAllListeners();
        btnHome.onClick.AddListener(() =>
        {
            vibrateController.Vibrate();
            audioController.PlaySound(SoundName.ClickBtn);
            LoadingSceneController.Instance.ChangeScene(SceneType.MainScene);
        });
    }


    private void InitBtnClose()
    {
        btnClose.onClick.RemoveAllListeners();
        btnClose.onClick.AddListener(() =>
        {
            vibrateController.Vibrate();
            audioController.PlaySound(SoundName.ClickBtn);
            GameManager.Instance.StateGame = StateGame.PlayGame;
            HidePopup();
        });
    }

    private void InitButtonMusic()
    {
        btnMusic.onClick.RemoveAllListeners();
        btnMusic.onClick.AddListener(() =>
        {
            vibrateController.Vibrate();
            audioController.PlaySound(SoundName.ClickBtn);
            audioController.SetVolumeMusic(!userSettings.isMusic);
            SetStatusToggle(userSettings.isMusic, imgToggleMusic);
        });
    }

    private void InitButtonSound()
    {
        btnSound.onClick.RemoveAllListeners();
        btnSound.onClick.AddListener(() =>
        {
            vibrateController.Vibrate();
            audioController.PlaySound(SoundName.ClickBtn);
            audioController.SetVolumeSound(!userSettings.isSound);
            SetStatusToggle(userSettings.isSound, imgToggleSound);
        });
    }
    private void InitButtonVibrate()
    {
        btnVibrate.onClick.RemoveAllListeners();
        btnVibrate.onClick.AddListener(() =>
        {
            audioController.PlaySound(SoundName.ClickBtn);
            vibrateController.SetVibrate(!userSettings.isVibrate);
            SetStatusToggle(userSettings.isVibrate, imgToggleVibrate);
            vibrateController.Vibrate();
        });
    }
    private void CheckStatusToggle()
    {
        SetStatusToggle(userSettings.isMusic, imgToggleMusic);
        SetStatusToggle(userSettings.isSound, imgToggleSound);
        SetStatusToggle(userSettings.isVibrate, imgToggleVibrate);
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
