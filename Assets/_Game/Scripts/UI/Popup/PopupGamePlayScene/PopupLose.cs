using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupLose : PopupBase
{
    [SerializeField] private Button btnRetry;
    [SerializeField] private Button btnHome;

    private void InitBtnRestart()
    {
        btnRetry.onClick.RemoveAllListeners();
        btnRetry.onClick.AddListener(() =>
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

    public override void InitPopup()
    {
        InitBtnRestart();
        InitBtnHome();
        base.InitPopup();
    }
}
