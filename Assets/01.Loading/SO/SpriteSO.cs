using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Game.Client.Ultils;
[Serializable]
public struct SpriteStruct
{
    public Sprite spr;
}

[Serializable]
public struct KeyStruct
{
    public int id;
    public int named;
}

[CreateAssetMenu(fileName = "SpriteSO", menuName ="SO/SpriteSO")]
public class SpriteSO : ScriptableObject
{
    //public SerializableDictionary<KeyStruct, SpriteStruct> spr;
}
