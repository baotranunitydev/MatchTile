using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupLose : PopupBase
{
    [SerializeField] private Button btnHome;
    [SerializeField] private Button btnRetry;
    private AudioController audioController;
    private VibrateController vibrateController;
    public override void InitPopup()
    {
        audioController = AudioController.Instance;
        vibrateController = VibrateController.Instance;
        InitBtnRestart();
        InitBtnHome();
        base.InitPopup();
    }
    private void InitBtnRestart()
    {
        btnRetry.onClick.RemoveAllListeners();
        btnRetry.onClick.AddListener(() =>
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
