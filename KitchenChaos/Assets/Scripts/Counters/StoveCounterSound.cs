using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = SoundManager.Instance.GetVolume();
        stoveCounter.OnFryingStateChanged += StoveCounter_OnFryingStateChanged;
        SoundManager.Instance.OnVolumeChanged += SoundManager_OnVolumeChanged;
    }

    private void SoundManager_OnVolumeChanged(object sender, SoundManager.VolumeChangeEventArgs e)
    {
        audioSource.volume = e.volume;
    }

    private void StoveCounter_OnFryingStateChanged(object sender, StoveCounter.OnFryingStateChangedEventArgs e)
    {
        bool isPlaySound = e.state == StoveCounter.State.Fried || e.state == StoveCounter.State.Frying;
        if (isPlaySound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
