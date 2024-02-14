using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupWin : PopupBase
{
    [SerializeField] private Button btnHome;
    [SerializeField] private Button btnNextLevel;
    private AudioController audioController;
    private VibrateController vibrateController;
    public override void InitPopup()
    {
        audioController = AudioController.Instance;
        vibrateController = VibrateController.Instance;
        InitBtnNextLevel();
        InitBtnHome();
        base.InitPopup();
    }
    private void InitBtnNextLevel()
    {
        btnNextLevel.onClick.RemoveAllListeners();
        btnNextLevel.onClick.AddListener(() =>
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
}
