using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;
    public static SoundManager instance;
    private Dictionary<Sound, AudioClip> soundsAudioClipDictionary;
    private float volume=0.10f;
    public float Volume { get { return volume; }  }
    private float defaultSoundVolume = 0.2f;
    public enum Sound
    {
        NormalBullet,
        NormalBullet1,
        LaserGunBulletSound,
        EnemyDie,
        LevelComplated,
        GameOver
    }
    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
     
        soundsAudioClipDictionary = new Dictionary<Sound, AudioClip>();
        foreach (Sound sound in Enum.GetValues(typeof(Sound)))
        {
            soundsAudioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
        volume = PlayerPrefs.GetFloat("soundVolume");
        if (volume == 0)
        {
            volume =defaultSoundVolume;
        }
    }
    private void Start()
    {
        audioSource.volume = volume;
    }
    public void PlaySound(Sound sound)
    {
        audioSource.PlayOneShot(soundsAudioClipDictionary[sound]);
    }
    public void IncreaOrDecreaseseVolume(float volumeAmountTo›ncrease)
    {
        volume = volumeAmountTo›ncrease;
        volume = Mathf.Clamp01(volume);
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("soundVolume", volume);
      
    }
  
    public float GetVolume()
    {
        return volume;
    }
}
