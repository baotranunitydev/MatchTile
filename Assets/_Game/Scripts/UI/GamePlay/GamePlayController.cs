using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

public enum StateGame
{
    InitBoard = 0,
    PlayGame = 1,
    PauseGame = 2,
    EndGame = 3,
}


public class GamePlayController : MonoBehaviour
{
    [SerializeField] private StateGame stateGame;
    [SerializeField] private GamePlayView gamePlayView;
    [SerializeField] private ScoreController scoreController;
    [SerializeField] private ComboController comboController;
    private GameHelper gameHelper;
    private AudioController audioController;
    private VibrateController vibrateController;
    public StateGame StateGame { get => stateGame; set => stateGame = value; }
    public APIController apiController => APIController.Instance;

    private void Start()
    {
        gameHelper = GameHelper.Instance;
        audioController = AudioController.Instance;
        vibrateController = VibrateController.Instance;
        InitGamePlayScene();
    }
    private void Update()
    {
        switch (stateGame)
        {
            case StateGame.InitBoard:
                break;
            case StateGame.PlayGame:
                gameHelper.InputHandle.UpdateInputHandle();
                break;
            case StateGame.PauseGame:
                break;
            case StateGame.EndGame:
                break;

        }
    }

    private async void InitGamePlayScene()
    {
        InitBtnPause();
        UpdateStarText();
        gamePlayView.SetLevelText(apiController.UserDataAsset.Data.level + 1);
        SetStatusImageCover(true);
        scoreController.InitScore();
        await InitGame();
        stateGame = StateGame.PlayGame;
        SetStatusImageCover(false);
    }

    private async UniTask InitGame()
    {
        var lstTileID = gameHelper.LevelController.GetLstTileID(apiController.UserDataAsset.Data.level);
        gameHelper.BoardController.InitBoard();
        await gameHelper.BoardController.SpawnTile(lstTileID);
    }

    private void InitBtnPause()
    {
        gamePlayView.InitButtonBack(() =>
        {
            vibrateController.Vibrate();
            audioController.PlaySound(SoundName.ClickBtn);
            stateGame = StateGame.PauseGame;
            var popupSettings = gameHelper.PopupController.GetPopupByType(PopupType.PopupPause);
            popupSettings.ShowPopup();
        });
    }

    public void SetStatusImageCover(bool isStatus) => gamePlayView.SetStatusImageCover(isStatus);

    public void StartCombo()
    {
        comboController.StartCombo();
    }

    public void UpdateStarText()
    {
        gamePlayView.SetStarText(apiController.UserDataAsset.Data.level);
    }

    public void AddScore()
    {
        var score = Mathf.Clamp(comboController.Combo * 5, 5, 30);
        scoreController.AddScore(score);
    }

    public async void Win()
    {
        var score = scoreController.Score;
        var result = await APIController.Instance.PostWin(score);
        if (result)
        {
            UpdateStarText();
            SetStatusImageCover(true);
            stateGame = StateGame.EndGame;
            audioController.PlaySound(SoundName.Win);
            var popupWin = gameHelper.PopupController.GetPopupByType(PopupType.PopupWin) as PopupWin;
            popupWin.InitInfoPopupWin(score, APIController.Instance.UserDataAsset.Data.level - score);
            popupWin.ShowPopup(() =>
            {
                SetStatusImageCover(false);
                popupWin.AnimationText();
            });
        }
    }

    public void Lose()
    {
        stateGame = StateGame.EndGame;
        SetStatusImageCover(true);
        audioController.PlaySound(SoundName.Lose);
        var popupLose = gameHelper.PopupController.GetPopupByType(PopupType.PopupLose);
        popupLose.ShowPopup(() => SetStatusImageCover(false));
    }
}
