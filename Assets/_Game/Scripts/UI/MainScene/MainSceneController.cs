using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loading;
public class MainSceneController : MonoBehaviour
{
    [SerializeField] private MainSceneView mainSceneView;
    [SerializeField] private PopupController popupController;
    private UserData userData;
    private AudioController audioController;
    private VibrateController vibrateController;
    private void Start()
    {
        userData = APIController.Instance.UserDataAsset.Data;
        audioController = AudioController.Instance;
        vibrateController = VibrateController.Instance;
        InitButtonPlay();
        InitButtonSettings();
    }

    private void InitButtonPlay()
    {
        var level = userData.level + 1;
        mainSceneView.InitButtonPlay(level, () =>
        {
            vibrateController.Vibrate();
            audioController.PlaySound(SoundName.ClickBtn);
            LoadingSceneController.Instance.ChangeScene(SceneType.GamePlay);
        });
    }

    private void InitButtonSettings()
    {
        mainSceneView.InitButtonSettings(() =>
        {
            vibrateController.Vibrate();
            audioController.PlaySound(SoundName.ClickBtn);
            popupController.GetPopupByType(PopupType.PopupSettings).ShowPopup();
        });
    }
}
