using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GamePlayController : MonoBehaviour
{
    [SerializeField] private GamePlayView gamePlayView;
    private GameHelper gameHelper;
    private GameManager gameManager;
    private AudioController audioController;
    private VibrateController vibrateController;
    private UserData userData;
    private void Start()
    {
        gameHelper = GameHelper.Instance;
        gameManager = GameManager.Instance;
        audioController = AudioController.Instance;
        vibrateController = VibrateController.Instance;
        userData = DBController.Instance.USER_DATA;
        InitBtnPause();
        UpdateStarText();
        gamePlayView.SetLevelText(userData.level + 1);
    }

    public void UpdateStarText()
    {
        gamePlayView.SetStarText(userData.star);
    }

    private void InitBtnPause()
    {
        gamePlayView.InitButtonBack(() =>
        {
            vibrateController.Vibrate();
            audioController.PlaySound(SoundName.ClickBtn);
            gameManager.StateGame = StateGame.PauseGame;
            var popupSettings = gameHelper.PopupController.GetPopupByType(PopupType.PopupPause);
            popupSettings.ShowPopup();
        });
    }

    public void ShowPopupWin(UnityAction onCompleteShow = null)
    {
        var popupWin = gameHelper.PopupController.GetPopupByType(PopupType.PopupWin);
        popupWin.ShowPopup(onCompleteShow);
    }

    public void ShowPopupLose(UnityAction onCompleteShow = null)
    {
        var popupLose = gameHelper.PopupController.GetPopupByType(PopupType.PopupLose);
        popupLose.ShowPopup(onCompleteShow);
    }
    public void SetStatusImageCover(bool isStatus) => gamePlayView.SetStatusImageCover(isStatus);
}
