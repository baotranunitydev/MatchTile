using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDataSO", menuName = "SO/LevelDataSO")]
public class LevelDataSO : ScriptableObject
{
    public Vector2Int GenAmountTile;
    public Level[] arrLevel;
    public Level GetLevel(int level) => Array.Find(arrLevel, item => item.level == level);


    [ContextMenu("Gen Level")]
    private void GenLevel()
    {
        var max = (GenAmountTile.y - GenAmountTile.x) / 3;
        arrLevel = new Level[max + 1];
        for (int i = 0; i <= max; i++)
        {
            arrLevel[i] = new Level();
            var level = arrLevel[i];
            level.level = i;
            level.tileAmount = GenAmountTile.x + i * 3;
            level.tileType = 7;
        }
    }
}

[Serializable]
public class Level
{
    public int level;
    public int tileAmount;
    public int tileType;
}