using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    [SerializeField] private GamePlayView gamePlayView;
    [SerializeField] private PopupController popupController;
    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.Instance;
        InitBtnSettings();
    }

    private void InitBtnSettings()
    {
        gamePlayView.InitButtonBack(() =>
        {
            gameManager.StateGame = StateGame.PauseGame;
            var popupSettings = popupController.GetPopupByType(PopupType.PopupSettings);
            popupSettings.ShowPopup();
        });
    }

    public void ShowPopupWin()
    {
        gameManager.StateGame = StateGame.EndGame;
        var popupWin = popupController.GetPopupByType(PopupType.PopupWin);
        popupWin.ShowPopup();
    }

    public void ShowPopupLose()
    {
        gameManager.StateGame = StateGame.EndGame;
        var popupLose = popupController.GetPopupByType(PopupType.PopupSettings);
        popupLose.ShowPopup();
    }
}
