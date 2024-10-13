using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AudioControllerModel
{
    public float VolumeMusic;
    public float VolumeEffect;
}

[CreateAssetMenu(fileName = "AudioDataAsset", menuName = "SO/Data/AudioDataAsset")]
public class AudioDataAsset : LocalDataAsset<AudioControllerModel>
{
    public override void SaveData()
    {
        base.SaveData();
    }

    public void SetVolumeMusic(float volume)
    {
        data.VolumeMusic = volume;
        SaveData();
    }

    public void SetVolumeSound(float volume)
    {
        data.VolumeEffect = volume;
        SaveData();
    }
}
