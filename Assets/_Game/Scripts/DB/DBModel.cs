using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBModel
{

}
[Serializable]
public class UserSettings
{
    public bool isMusic;
    public bool isEffect;

    public UserSettings()
    {
        isMusic = true;
        isEffect = true;
    }

    public void SetMusic(bool isMusic)
    {
        this.isMusic = isMusic;
        DBController.Instance.USER_SETTINGS = this;
    }

    public void SetEffect(bool isEffect)
    {
        this.isEffect = isEffect;
        DBController.Instance.USER_SETTINGS = this;
    }
}

[Serializable]
public class UserData
{
    public int level;

    public UserData()
    {
        level = 0;
    }

    public void LevelUp()
    {
        level++;
        DBController.Instance.USER_DATA = this;
    }
}
