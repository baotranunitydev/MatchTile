using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DBModel
{

}
[Serializable]
public struct UserSettings
{
    public bool isMusic;
    public bool isSound;
    public bool isVibrate;

    public void SetMusic(bool isMusic)
    {
        this.isMusic = isMusic;
    }

    public void SetSound(bool isSound)
    {
        this.isSound = isSound;
    }

    public void SetVibrate(bool isVibrate)
    {
        this.isVibrate = isVibrate;
    }
}

public enum ResourceType
{
    Star = 0,
    Hint = 1,
    Bomb = 2,
}

[Serializable]
public struct UserData
{
    public string firstName;
    public string lastName;
    public string telegramId;
    public int level;
    public int star;
    public int amountHint;
    public int amountBomb;

    public void LevelUp()
    {
        level++;
        //DBController.Instance.USER_DATA = this;
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
        //DBController.Instance.USER_DATA = this;
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
        //DBController.Instance.USER_DATA = this;
    }
}
