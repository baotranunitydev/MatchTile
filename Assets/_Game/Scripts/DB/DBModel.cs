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
    public bool isSound;
    public bool isVibrate;

    public UserSettings()
    {
        isMusic = true;
        isSound = true;
        isVibrate = true;
    }

    public void SetMusic(bool isMusic)
    {
        this.isMusic = isMusic;
        DBController.Instance.USER_SETTINGS = this;
    }

    public void SetSound(bool isSound)
    {
        this.isSound = isSound;
        DBController.Instance.USER_SETTINGS = this;
    }

    public void SetVibrate(bool isVibrate)
    {
        this.isVibrate = isVibrate;
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
