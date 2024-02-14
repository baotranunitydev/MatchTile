using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

public enum ResourceType
{
    Star = 0,
    Hint = 1,
    Bomb = 2,
}

[Serializable]
public class UserData
{
    public int level;
    public int star;
    public int amountHint;
    public int amountBomb;

    public UserData()
    {
        level = 0;
        star = 3000;
        amountHint = 3;
        amountBomb = 3;
    }

    public void LevelUp()
    {
        level++;
        DBController.Instance.USER_DATA = this;
    }

    public void InscreaseResource(ResourceType resourceType, int amount)
    {
        switch (resourceType)
        {
            case ResourceType.Star:
                star += amount;
                break;
            case ResourceType.Hint:
                amountHint += amount;
                break;
            case ResourceType.Bomb:
                amountBomb += amount;
                break;
        }
        DBController.Instance.USER_DATA = this;
    }

    public void DescreaseResource(ResourceType resourceType, int amount)
    {
        switch (resourceType)
        {
            case ResourceType.Star:
                star -= amount;
                break;
            case ResourceType.Hint:
                amountHint -= amount;
                break;
            case ResourceType.Bomb:
                amountBomb -= amount;
                break;
        }
        DBController.Instance.USER_DATA = this;
    }
}
