using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SoundSO", menuName = "SO/Sound")]
public class AudioSO : ScriptableObject
{
    public Sound[] arrMusic;
    public Sound[] arrSound;
}


[Serializable]
public class Sound
{
    public SoundName name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 1f;
    [HideInInspector]
    public AudioSource source;
    public bool loop = false;
}


public enum SoundName
{
    BG_00 = 0,
    BG_01 = 1,
    ClickBtn = 2,
    ClickTile = 3,
    Merge = 4,
    Win = 5,
    Lose = 6,
}
