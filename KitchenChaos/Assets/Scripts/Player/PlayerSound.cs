using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private Player player;
    private float footstepTimer;
    private float footstepTimerMax = 0.2f;

    private void Awake()
    {
        player = GetComponent<Player>();
        footstepTimer = 0f;
    }

    private void Update()
    {
        if (player.GetIsWalking())
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0)
            {
                footstepTimer = footstepTimerMax;
                SoundManager.Instance.PlaySoundFootStep(player.transform.position);
            }
        }
    }
}
