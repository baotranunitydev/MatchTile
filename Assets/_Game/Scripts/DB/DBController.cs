using System;
using UnityEngine;
using UnityEngine.Events;

public class DBController : Singleton<DBController>
{
    #region VARIABLE

    private UserData userData;
    public UserData USER_DATA
    {
        get => userData;
        set
        {
            userData = value;
            Save(DBKey.USER_DATA, userData);
        }
    }
    private UserSettings userSettings;
    public UserSettings USER_SETTINGS
    {
        get => userSettings;
        set
        {
            userSettings = value;
            Save(DBKey.USER_SETTINGS, userSettings);
        }
    }

    #endregion
    protected override void CustomAwake()
    {
        Initializing();
    }


    void CheckDependency(string key, UnityAction<string> onComplete)
    {
        if (!PlayerPrefsExtend.HasKey(key))
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
            PlayerPrefsExtend.SetString(key, values.ToString());
        }
        else
        {
            try
            {
                string json = JsonUtility.ToJson(values);
                PlayerPrefsExtend.SetString(key, json);
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
            string stringValue = PlayerPrefsExtend.GetString(key);
            return (T)Convert.ChangeType(stringValue, typeof(T));
        }
        else
        {
            string json = PlayerPrefsExtend.GetString(key);
            return JsonUtility.FromJson<T>(json);
        }
    }

    public void Delete(string key)
    {
        PlayerPrefsExtend.DeleteKey(key);
    }

    public void DeleteAll()
    {
        PlayerPrefsExtend.DeleteAll();
    }

    void Initializing()
    {
        CheckDependency(DBKey.USER_DATA, key =>
        {
            USER_DATA = new UserData();
        });
        CheckDependency(DBKey.USER_SETTINGS, key =>
        {
            USER_SETTINGS = new UserSettings();
        });
        Load();
    }

    void Load()
    {
        userData = LoadDataByKey<UserData>(DBKey.USER_DATA);
        userSettings = LoadDataByKey<UserSettings>(DBKey.USER_SETTINGS);
    }
}

public class DBKey
{
    public readonly static string USER_DATA = "USER_DATA";
    public readonly static string USER_SETTINGS = "USER_SETTINGS";
}
