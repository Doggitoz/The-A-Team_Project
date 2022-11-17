using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource UISource;
    [SerializeField] AudioSource EffectsSource;

    float MasterVolume = 1.0f;
    float MusicVolume = 1.0f;
    float UIVolume = 1.0f;
    float EffectVolume = 1.0f;

    //CREATE SINGLETON HERE

    private void Update()
    {
        Debug.Log("Master volume: " + MasterVolume);
        Debug.Log("Music volume: " + MusicVolume);
    }
    public void PlayMusic(AudioClip clip, bool fadeInOut = true)
    {
        MusicSource.Stop();
        //TODO: LOGIC TO FADE IN/OUT GOES HERE


        MusicSource.clip = clip;
        MusicSource.Play();
    }

    void UpdateAudioSourceVolumes()
    {
        MusicSource.volume = MasterVolume * MusicVolume;
        UISource.volume = MasterVolume * UIVolume;
        EffectsSource.volume = MasterVolume * EffectVolume;
    }

    public void SetMasterVolume(float volume)
    {
        MasterVolume = volume;
        UpdateAudioSourceVolumes();
    }

    public void SetMusicVolume(float volume)
    {
        MusicVolume = volume;
        UpdateAudioSourceVolumes();
    }

}
