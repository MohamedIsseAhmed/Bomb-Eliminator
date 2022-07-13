using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;
    public static SoundManager instance;
    private Dictionary<Sound, AudioClip> soundsAudioClipDictionary;
    private float volume = 0.5f;
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
        audioSource.volume = volume;
        soundsAudioClipDictionary = new Dictionary<Sound, AudioClip>();
        foreach (Sound sound in Enum.GetValues(typeof(Sound)))
        {
           
            soundsAudioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
       
    }
  
    public void PlaySound(Sound sound)
    {
 
        audioSource.PlayOneShot(soundsAudioClipDictionary[sound]);
    }
    public void IncreaOrDecreaseseVolume(float volumeAmountTo›ncrease)
    {
        print("volume" + volume);
        volume = volumeAmountTo›ncrease;
       volume = Mathf.Clamp01(volume);
        audioSource.volume = volume;
    }
  
    public float GetVolume()
    {
        return volume;
    }
}
