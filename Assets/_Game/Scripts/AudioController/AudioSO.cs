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
    BackroundMusic = 0,
    Sound_OnClickButton = 1,
    Sound_Clear = 2,
    Sound_WinGame = 3,
    Sound_LoseGame = 4,
}
