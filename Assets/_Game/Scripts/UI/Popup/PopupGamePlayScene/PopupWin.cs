using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupWin : PopupBase
{
    [SerializeField] private Button btnHome;
    [SerializeField] private Button btnNextLevel;

    private void InitBtnNextLevel()
    {
        btnNextLevel.onClick.RemoveAllListeners();
        btnNextLevel.onClick.AddListener(() =>
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
        InitBtnNextLevel();
        InitBtnHome();
        base.InitPopup();
    }
}
