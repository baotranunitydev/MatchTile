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
    private UserData userData;
    public StateGame StateGame { get => stateGame; set => stateGame = value; }

    private void Start()
    {
        gameHelper = GameHelper.Instance;
        audioController = AudioController.Instance;
        vibrateController = VibrateController.Instance;
        userData = DBController.Instance.USER_DATA;
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
        gamePlayView.SetLevelText(userData.level + 1);
        SetStatusImageCover(true);
        scoreController.InitScore();
        await InitGame();
        stateGame = StateGame.PlayGame;
        SetStatusImageCover(false);
    }

    private async UniTask InitGame()
    {
        var lstTileID = gameHelper.LevelController.GetLstTileID(userData.level);
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
        gamePlayView.SetStarText(userData.star);
    }

    public void AddScore()
    {
        var score = Mathf.Clamp(comboController.Combo * 5, 5, 30);
        scoreController.AddScore(score);
    }

    public void Win()
    {
        userData.LevelUp();
        var score = scoreController.Score;
        userData.InscreaseResource(ResourceType.Star, score);
        UpdateStarText();
        SetStatusImageCover(true);
        stateGame = StateGame.EndGame;
        audioController.PlaySound(SoundName.Win);
        var popupWin = gameHelper.PopupController.GetPopupByType(PopupType.PopupWin) as PopupWin;
        popupWin.InitInfoPopupWin(score, userData.star - score);
        popupWin.ShowPopup(() =>
        {
            SetStatusImageCover(false);
            popupWin.AnimationText();
        });
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
