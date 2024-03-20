using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownUI : MonoBehaviour
{
    const string COUNTDOWN_CHANGE = "CountdownChange";

    [SerializeField] private TextMeshProUGUI countdownText;
    private Animator animator;
    private string preCountdownText;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        Hide();
    }

    private void KitchenGameManager_OnStateChanged(object sender, KitchenGameManager.OnStateChangedEventArgs e)
    {
        if (e.state == KitchenGameManager.State.CountdownToStart)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            countdownText.text = Mathf.Ceil(KitchenGameManager.Instance.GetTimer()).ToString();
            if (countdownText.text != preCountdownText)
            {
                preCountdownText = countdownText.text;
                animator.SetTrigger(COUNTDOWN_CHANGE);
                SoundManager.Instance.PlayWarningSound(Vector3.zero);
            }
        }
    }
}
