using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneController : MonoBehaviour
{
    [SerializeField] private MainSceneView mainSceneView;
    [SerializeField] private PopupController popupController;
    private UserData userData;
    private void Start()
    {
        userData = DBController.Instance.USER_DATA;
        InitButtonPlay();
        InitButtonSettings();
    }

    private void InitButtonPlay()
    {
        var level = userData.level + 1;
        mainSceneView.InitButtonPlay(level, () =>
        {
            LoadingSceneController.Instance.ChangeScene(SceneType.GamePlay);
        });
    }

    private void InitButtonSettings()
    {
        mainSceneView.InitButtonSettings(() =>
        {
            popupController.GetPopupByType(PopupType.PopupSettings).ShowPopup();
        });
    }
}
