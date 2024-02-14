using Cysharp.Threading.Tasks;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum StateGame
{
    InitBoard = 0,
    PlayGame = 1,
    PauseGame = 2,
    EndGame = 3,
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private StateGame stateGame;
    private GameHelper gameHelper;
    private AudioController audioController;
    private UserData userData;
    public StateGame StateGame { get => stateGame; set => stateGame = value; }
    public UnityAction onWin;
    public UnityAction onLose;
    private async void Start()
    {
        gameHelper = GameHelper.Instance;
        audioController = AudioController.Instance;
        userData = DBController.Instance.USER_DATA;
        onWin += Win;
        onLose += Lose;
        SetStausImageCover(true);
        await InitGame();
        stateGame = StateGame.PlayGame;
        SetStausImageCover(false);
    }

    private void OnDestroy()
    {
        onWin -= Win;
        onLose -= Lose;
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

    private async UnitaskVoid InitGame()
    {
        var lstTileID = gameHelper.LevelController.GetLstTileID(userData.level);
        gameHelper.BoardController.InitBoard();
        await gameHelper.BoardController.SpawnTile(lstTileID);
    }

    private void Win()
    {
        userData.LevelUp();
        SetStausImageCover(true);
        stateGame = StateGame.EndGame;
        audioController.PlaySound(SoundName.Win);
        gameHelper.GamePlayController.ShowPopupWin(() => SetStausImageCover(false));
    }

    private void Lose()
    {
        stateGame = StateGame.EndGame;
        SetStausImageCover(true);
        audioController.PlaySound(SoundName.Lose);
        gameHelper.GamePlayController.ShowPopupLose(() => SetStausImageCover(false));
    }

    private void SetStausImageCover(bool isStatus) => gameHelper.GamePlayController.SetStatusImageCover(isStatus);
}
