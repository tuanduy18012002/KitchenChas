using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundClipsSO audioClips;
    private float m_volume;

    public static SoundManager Instance { get; private set; }
    public event EventHandler<VolumeChangeEventArgs> OnVolumeChanged;
    public class VolumeChangeEventArgs : EventArgs
    {
        public float volume;
    }

    private void Awake()
    {
        Instance = this;
        m_volume = PlayerPrefs.GetFloat(OptionsUI.PLAYER_PREFS_SOUND_VOLUME, 1.0f);
    }

    private void Start()
    {
        CuttingCounter.OnAnyCutting += CuttingCounter_OnAnyCutting;
        DeliveryManager.Instance.OnDeliveryFail += DeliveryManager_OnDeliveryFail;
        DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
        Player.Instance.OnPickupSomething += Player_OnPickupSomething;
        Player.Instance.OnDropSomething += Player_OnDropSomething;
        TrashCounter.OnTrashSomething += TrashCounter_OnTrashSomething;
    }

    private void TrashCounter_OnTrashSomething(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClips.trash, trashCounter.transform.position, m_volume);
    }

    private void Player_OnDropSomething(object sender, System.EventArgs e)
    {
        PlaySound(audioClips.objectDrop, Player.Instance.transform.position, m_volume);
    }

    private void Player_OnPickupSomething(object sender, System.EventArgs e)
    {
        PlaySound(audioClips.objectPickup, Player.Instance.transform.position, m_volume);
    }

    private void DeliveryManager_OnDeliverySuccess(object sender, System.EventArgs e)
    {
        PlaySound(audioClips.deliverySuccess, DeliveryManager.Instance.transform.position, m_volume);
    }

    private void DeliveryManager_OnDeliveryFail(object sender, System.EventArgs e)
    {
        PlaySound(audioClips.deliveryFail, DeliveryManager.Instance.transform.position, m_volume);
    }

    private void CuttingCounter_OnAnyCutting(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClips.chop, cuttingCounter.transform.position, m_volume);
    }

    private void PlaySound(List<AudioClip> audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip[UnityEngine.Random.Range(0, audioClip.Count)], position, volume);
    }

    public void PlaySoundFootStep(Vector3 position, float volume = 1f)
    {
        PlaySound(audioClips.footstep, position, volume);
    }

    public void PlayWarningSound(Vector3 position, float volume = 1f)
    {
        PlaySound(audioClips.warning, position, volume);
    }

    public float IncreaseVolume()
    {
        if (m_volume >= 1f) 
        {
            m_volume = 0f;
        }
        else
        {
            m_volume += 0.1f;
        }
        OnVolumeChanged?.Invoke(this, new VolumeChangeEventArgs() { volume  = m_volume });
        return m_volume;
    }

    public float GetVolume() { return  m_volume; }
}
