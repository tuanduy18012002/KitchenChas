using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    private const string UI_POPUP = "UIPopup";
    private Animator animator;

    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Image iconImage;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failureColor;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failureSprite;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFail += DeliveryManager_OnDeliveryFail;
        gameObject.SetActive(false);
    }

    private void DeliveryManager_OnDeliveryFail(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(UI_POPUP);
        background.color = failureColor;
        messageText.text = "Delivery\nFailure";
        iconImage.sprite = failureSprite;
    }

    private void DeliveryManager_OnDeliverySuccess(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(UI_POPUP);
        background.color = successColor;
        messageText.text = "Delivery\nSuccess";
        iconImage.sprite = successSprite;
    }
}
