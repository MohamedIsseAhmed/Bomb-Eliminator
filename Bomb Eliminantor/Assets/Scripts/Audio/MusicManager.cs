using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    private AudioSource audioSource;
    private float volume = 0.10f;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
        if ( instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
      

    }

    public void IncreaOrDecreaseseVolume(float volumeAmountTo›ncrease)
    {
        print("volume" + volume);
        volume = volumeAmountTo›ncrease;
        volume = Mathf.Clamp01(volume);
        audioSource.volume = volume;
    }
}
