using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class LocalDataAsset<T> : LoadingDataAsset<T> where T : struct 
{
    [SerializeField] protected string nameSaveLocal;

    public virtual void SaveData()
    {
        try
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            string nameKey = string.IsNullOrEmpty(nameSaveLocal) ? GetType().Name : nameSaveLocal;
            //Debug.Log(json);
            PlayerPrefsExtend.SetString(nameKey, json);
            
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save data: {e.Message}");
        }
    }

    public override async UniTask<LoadingType> LoadingData()
    {
        LoadData();
        return await base.LoadingData();
    }

    protected virtual void LoadData()
    {
        string nameKey = !string.IsNullOrEmpty(nameSaveLocal) ? nameSaveLocal : GetType().Name;
        if (!PlayerPrefsExtend.HasKey(nameKey))
        {
            Debug.Log($"Key {nameKey} in {GetType().Name} doesn't exist");
            SaveData();
            type = LoadingType.Fail;
            return;
        }
        //Debug.Log(nameKey);
        string json = PlayerPrefsExtend.GetString(nameKey);
        //Debug.Log(json);
        if (string.IsNullOrEmpty(json))
        {
            Debug.Log($"Key {nameKey} is null");
            type = LoadingType.Fail;
            return;
        }
        try
        {
            Debug.Log($"{json}");
            data = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            type = LoadingType.Success;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load data: {e.Message}");
            type = LoadingType.Fail;
        }
    }
}
