using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDataSO", menuName = "SO/LevelDataSO")]
public class LevelDataSO : ScriptableObject
{
    public Level[] arrLevel;

    public Level GetLevel(int level) => Array.Find(arrLevel, item => item.level == level);
}

[Serializable]
public class Level
{
    public int level;
    public int tileAmount;
    public int tileType;
}