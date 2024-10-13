using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    [SerializeField] private UserSettingsAsset _userSettingsAsset;
    [SerializeField] private AudioSO soundSO;
    private Sound soundMusic;
    private Sound[] arrSound;
    private async void Start()
    {
        await _userSettingsAsset.LoadingData();
        CreateAudioSource();
        CreateAudioSourceBackround();
        PlayBackroundMusic(SoundName.BG_00);
        SetVolumeMusic(_userSettingsAsset.Data.isMusic);
        SetVolumeSound(_userSettingsAsset.Data.isSound);
    }

    public void SetVolumeSound(bool isSound)
    {
        for (int i = 0; i < arrSound.Length; i++)
        {
            var sound = arrSound[i];
            sound.source.mute = !isSound;
        }
        _userSettingsAsset.SetSound(isSound);
    }

    public void SetVolumeMusic(bool isMusic)
    {
        soundMusic.source.mute = !isMusic;
        _userSettingsAsset.SetMusic(isMusic);
    }

    public void SetVolumeSound(float volume)
    {
        for (int i = 0; i < arrSound.Length; i++)
        {
            var sound = arrSound[i];
            var cacheVolume = sound.volume;
            sound.source.volume = volume * cacheVolume;
        }
    }

    public void SetVolumeMusic(float volume)
    {
        var cacheVolume = soundMusic.volume;
        soundMusic.source.volume = volume * cacheVolume;
    }

    private void CreateAudioSource()
    {
        var soundCount = soundSO.arrSound.Length;
        arrSound = new Sound[soundCount];
        for (int i = 0; i < soundCount; i++)
        {
            var sound = soundSO.arrSound[i];
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
            arrSound[i] = sound;
        }
    }

    private void CreateAudioSourceBackround()
    {
        soundMusic = new Sound();
        soundMusic.source = gameObject.AddComponent<AudioSource>();
    }

    public void PlayBackroundMusic(SoundName soundNameBG)
    {
        Sound music = Array.Find(soundSO.arrMusic, soundMusic => soundMusic.name == soundNameBG);
        if (music == null)
        {
            Debug.LogError("Unable to play music " + soundNameBG);
            return;
        }
        soundMusic.volume = music.volume;
        soundMusic.clip = music.clip;
        soundMusic.loop = music.loop;
        soundMusic.source.clip = soundMusic.clip;
        soundMusic.source.volume = soundMusic.volume;
        soundMusic.source.loop = soundMusic.loop;
        soundMusic.source.Play();
        //Debug.Log($"Volume: {soundMusic.volume}, Source: {soundMusic.source.volume}");
    }

    public void PlaySound(SoundName name)
    {
        Sound effect = Array.Find(arrSound, effect => effect.name == name);
        if (effect == null)
        {
            Debug.LogError("Unable to play effect " + name);
            return;
        }
        effect.source.Play();
    }
}

