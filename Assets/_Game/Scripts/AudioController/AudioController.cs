﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioController : Singleton<AudioController> 
{
    [SerializeField] private AudioSO soundSO;
    private Sound soundMusic;
    private Sound[] arrEffect;
    private UserSettings userSettings;
    private void Start()
    {
        userSettings = DBController.Instance.USER_SETTINGS;
        Application.targetFrameRate = 60;
        Input.multiTouchEnabled = false;
        CreateAudioSource();
        CreateAudioSourceBackround();
        PlayBackroundMusic(SoundName.BackroundMusic);
        SetVolumeMusic(userSettings.isMusic);
        SetVolumeEffect(userSettings.isEffect);
    }

    public void SetVolumeEffect(bool isEffect)
    {
        for (int i = 0; i < arrEffect.Length; i++)
        {
            var sound = arrEffect[i];
            sound.source.mute = isEffect;
        }
        userSettings.SetEffect(isEffect);
    }

    public void SetVolumeMusic(bool isMusic)
    {
        soundMusic.source.mute = isMusic;
        userSettings.SetMusic(isMusic);
    }

    public void SetVolumeEffect(float volume)
    {
        for (int i = 0; i < arrEffect.Length; i++)
        {
            var sound = arrEffect[i];
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
        arrEffect = new Sound[soundCount];
        for (int i = 0; i < soundCount; i++)
        {
            var sound = soundSO.arrSound[i];
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
            arrEffect[i] = sound;
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
        //Debug.Log($"Volume: {soundMusic.volume}, Source: {soundMusic.source.volume}");
    }

    public void PlayEffect(SoundName name)
    {
        Sound effect = Array.Find(arrEffect, effect => effect.name == name);
        if (effect == null)
        {
            Debug.LogError("Unable to play effect " + name);
            return;
        }
        effect.source.Play();
    }
}
