using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    public static MusicManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat(OptionsUI.PLAYER_PREFS_MUSIC_VOLUME, 0.5f);
    }

    public float IncreaseVolume()
    {
        if (audioSource.volume >= 1f)
        {
            audioSource.volume = 0f;
        }
        else
        {
            audioSource.volume += 0.1f;
        }
        return audioSource.volume;
    }

    public float GetVolume() { return audioSource.volume; }
}
