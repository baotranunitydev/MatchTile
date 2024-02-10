using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateGame
{
    PlayGame = 0,
    PauseGame = 1,
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private StateGame stateGame;
    private GameHelper gameHelper;
    public StateGame StateGame { get => stateGame; set => stateGame = value; }

    private void Start()
    {
        gameHelper = GameHelper.Instance;
        stateGame = StateGame.PauseGame;
        InitGame().Forget();
    }

    private void Update()
    {
        switch (stateGame)
        {
            case StateGame.PlayGame:
                gameHelper.InputHandle.UpdateInputHandle();
                break;
            case StateGame.PauseGame:
                break;
        }
    }

    private async UnitaskVoid InitGame()
    {
        var lstTileID = gameHelper.LevelController.GetLstTileID(0);
        gameHelper.BoardController.InitBoard();
        await gameHelper.BoardController.SpawnTile(lstTileID);
        stateGame = StateGame.PlayGame;
    }
}
