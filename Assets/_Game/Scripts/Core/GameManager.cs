using Cysharp.Threading.Tasks;
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
    private UserData userData;
    public StateGame StateGame { get => stateGame; set => stateGame = value; }
    public UnityAction onWin;
    public UnityAction onLose;
    private void Start()
    {
        gameHelper = GameHelper.Instance;
        userData = DBController.Instance.USER_DATA;
        onWin += Win;
        onLose += Lose;
        InitGame().Forget();
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
        stateGame = StateGame.PlayGame;
    }

    private void Win()
    {
        userData.LevelUp();
        gameHelper.GamePlayController.ShowPopupWin();
    }

    private void Lose()
    {
        gameHelper.GamePlayController.ShowPopupLose();
    }
}
