using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    private AudioSource audioSource;
    private float volume = 0.10f;
    public float Volume { get { return volume; }}
    private float defaultMusicVolume=0.1f;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
   
        if ( instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        volume= PlayerPrefs.GetFloat("musicVolume");
        if (volume == 0)
        {
            volume = defaultMusicVolume;
        }
    }
    private void Start()
    {
        audioSource.volume = volume;
    }
    public void IncreaOrDecreaseseVolume(float volumeAmountTo›ncrease)
    {
        volume = volumeAmountTo›ncrease;
        volume = Mathf.Clamp01(volume);
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
}
