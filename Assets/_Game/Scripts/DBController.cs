using System;
using UnityEngine;
using UnityEngine.Events;

public class DBController : Singleton<DBController>
{
    #region VARIABLE

    private int _coin;
    public int COIN
    {
        get => _coin;
        set
        {
            _coin = value;
            Save(DBKey.COIN, value);
        }
    }
    
    #endregion
    protected override void CustomAwake()
    {
        Initializing();
    }

    void Initializing()
    {
        CheckDependency(DBKey.COIN, key =>
        {
            COIN = 0;
        });
        Load();
    }


    void CheckDependency(string key, UnityAction<string> onComplete)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            onComplete?.Invoke(key);
        }
    }

    public void Save<T>(string key, T values)
    {
        
        if (typeof(T) == typeof(int) ||
            typeof(T) == typeof(bool) ||
            typeof(T) == typeof(string) ||
            typeof(T) == typeof(float) ||
            typeof(T) == typeof(long) ||
            typeof(T) == typeof(Quaternion) ||
            typeof(T) == typeof(Vector2) ||
            typeof(T) == typeof(Vector3) ||
            typeof(T) == typeof(Vector2Int) ||
            typeof(T) == typeof(Vector3Int))
        {
            PlayerPrefs.SetString(key, values.ToString());
        }
        else
        {
            try
            {
                string json = JsonUtility.ToJson(values);
                PlayerPrefs.SetString(key, json);
            }
            catch (UnityException e)
            {
                throw new UnityException(e.Message);
            }
        }
    }

    public T LoadDataByKey<T>(string key)
    {
        if (typeof(T) == typeof(int) ||
            typeof(T) == typeof(bool) ||
            typeof(T) == typeof(string) ||
            typeof(T) == typeof(float) ||
            typeof(T) == typeof(long) ||
            typeof(T) == typeof(Quaternion) ||
            typeof(T) == typeof(Vector2) ||
            typeof(T) == typeof(Vector3) ||
            typeof(T) == typeof(Vector2Int) ||
            typeof(T) == typeof(Vector3Int))
        {
            string stringValue = PlayerPrefs.GetString(key);
            return (T)Convert.ChangeType(stringValue, typeof(T));
        }
        else
        {
            string json = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<T>(json);
        }
    }

    public void Delete(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }

    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    void Load()
    {
        _coin = LoadDataByKey<int>(DBKey.COIN); 
    }
}

public class DBKey
{
    public readonly static string COIN = "COIN";
}
