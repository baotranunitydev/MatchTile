using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private LevelDataSO levelDataSO;
    public List<int> GetLstTileID(int level)
    {
        var levelLoad = GetLevelLoad(level, levelDataSO.arrLevel.Length - 1, 9);
        //Debug.Log($"Current Level: {level}, Max: {levelDataSO.arrLevel.Length - 1}, Start: {0} - Level Load: {levelLoad}");
        var lstTileID = new List<int>();
        var levelSO = levelDataSO.GetLevel(levelLoad);
        var lstRandomID = GetRandomLstID(levelSO.tileType);
        int currentIndexRandom = 0;
        int countTileID = 0;
        var tileAmount = levelSO.tileAmount % 3 == 0 ? levelSO.tileAmount : levelSO.tileAmount - levelSO.tileAmount % 3;
        for (int i = 0; i < tileAmount; i++)
        {
            countTileID++;
            lstTileID.Add(currentIndexRandom);
            if (countTileID % 3 == 0)
            {
                currentIndexRandom++;
                if (currentIndexRandom >= lstRandomID.Count)
                {
                    currentIndexRandom = 0;
                }
            }
        }
        ShuffleList(lstTileID);
        return lstTileID;
    }

    private int GetLevelLoad(int current, int max, int start)
    {
        if (current > max)
        {
            while (current > max)
            {
                current = (current - (current / max) * max) + start;
            }
        }
        return current;
    }

    private List<int> GetRandomLstID(int amount)
    {
        var lstRandom = new List<int>();
        var lstID = new List<int>();
        var tileSO = GameHelper.Instance.ModelSO.tileSO;
        var amountType = tileSO.lstTileModelSO.Count > 0 ? tileSO.lstTileModelSO.Count : 1;
        for (int i = 0; i < amountType; i++)
        {
            lstID.Add(i);
        }
        for (int i = 0; i < amount; i++)
        {
            var randomId = Random.Range(0, lstID.Count);
            lstRandom.Add(randomId);
            lstID.Remove(randomId);
        }
        return lstRandom;
    }

    private void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
