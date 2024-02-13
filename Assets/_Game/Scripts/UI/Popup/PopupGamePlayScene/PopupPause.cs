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
    [Header("Button")]
    [SerializeField] private Button btnClose;
    [SerializeField] private Button btnMusic;
    [SerializeField] private Button btnSound;
    [SerializeField] private Button btnHome;
    [SerializeField] private Button btnRestart;
    private UserSettings userSettings;
    private AudioController audioController;
    private Vector2 pivotLeft = new Vector2(0, 0.5f);
    private Vector2 pivotRight = new Vector2(1, 0.5f);
    public override void InitPopup()
    {
        userSettings = DBController.Instance.USER_SETTINGS;
        audioController = AudioController.Instance;
        InitBtnClose();
        InitBtnHome();
        InitBtnRestart();
        InitButtonMusic();
        InitButtonSound();
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
