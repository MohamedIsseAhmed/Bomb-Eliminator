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
        soundsAudioClipDictionary = new Dictionary<Sound, AudioClip>();
        foreach (Sound sound in Enum.GetValues(typeof(Sound)))
        {
            soundsAudioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
       
    }
    private void Start()
    {
        Debug.Log(soundsAudioClipDictionary.Count);
    }
    public void PlaySound(Sound sound)
    {
        audioSource.PlayOneShot(soundsAudioClipDictionary[sound], volume);
    }
    //public void IncreaseVolume()
    //{
    //    volume += 0.1f;
    //    volume = Mathf.Clamp01(volume);
    //    PlayerPrefs.SetFloat("soundVolume", volume);
    //}
    //public void DecreaseVolume()
    //{
    //    volume -= 0.1f;
    //    volume = Mathf.Clamp01(volume);
    //    PlayerPrefs.SetFloat("soundVolume", volume);
    //}
    //public float GetVolume()
    //{
    //    return volume;
    //}
}
