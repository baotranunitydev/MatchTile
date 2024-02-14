using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GamePlayController : MonoBehaviour
{
    [SerializeField] private GamePlayView gamePlayView;
    [SerializeField] private PopupController popupController;
    private GameManager gameManager;
    private AudioController audioController;
    private VibrateController vibrateController;
    private UserData userData;
    private void Start()
    {
        gameManager = GameManager.Instance;
        audioController = AudioController.Instance;
        vibrateController = VibrateController.Instance;
        userData = DBController.Instance.USER_DATA;   
        InitBtnSettings();
        gamePlayView.SetLevelText(userData.level + 1);
    }

    private void InitBtnSettings()
    {
        gamePlayView.InitButtonBack(() =>
        {
            vibrateController.Vibrate();
            audioController.PlaySound(SoundName.ClickBtn);
            gameManager.StateGame = StateGame.PauseGame;
            var popupSettings = popupController.GetPopupByType(PopupType.PopupPause);
            popupSettings.ShowPopup();
        });
    }

    public void ShowPopupWin(UnityAction onCompleteShow = null)
    {
        var popupWin = popupController.GetPopupByType(PopupType.PopupWin);
        popupWin.ShowPopup(onCompleteShow);
    }

    public void ShowPopupLose(UnityAction onCompleteShow = null)
    {
        var popupLose = popupController.GetPopupByType(PopupType.PopupLose);
        popupLose.ShowPopup(onCompleteShow);
    }

    public void SetStatusImageCover(bool isStatus) => gamePlayView.SetStatusImageCover(isStatus);
} 
