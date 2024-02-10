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
        InitGame();
    }

    private void InitGame()
    {
        var lstTileID = gameHelper.LevelController.GetLstTileID(0);
        gameHelper.BoardController.InitBoard();
        gameHelper.BoardController.SpawnTile(lstTileID).Forget();
    }
}
