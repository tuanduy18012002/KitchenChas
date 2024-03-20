using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;
    bool isCountdown;

    private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        isCountdown = false;
        timerImage.fillAmount = 0f;
    }

    private void KitchenGameManager_OnStateChanged(object sender, KitchenGameManager.OnStateChangedEventArgs e)
    {
        if (e.state == KitchenGameManager.State.GamePlaying)
        {
            isCountdown = true;
        } else
        {
            isCountdown = false;
            timerImage.fillAmount = 0f;
        }
    }

    private void Update()
    {
        if (isCountdown) {
            timerImage.fillAmount = KitchenGameManager.Instance.GetPlayingTimerNormalize();
        }
    }
}
