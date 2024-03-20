using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deliverdCountText;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            SceneLoader.Load(SceneLoader.Scene.MainMenuScene);
        });
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        mainMenuButton.Select();
        Hide();
    }

    private void KitchenGameManager_OnStateChanged(object sender, KitchenGameManager.OnStateChangedEventArgs e)
    {
        if (e.state == KitchenGameManager.State.GameOver)
        {
            deliverdCountText.text = DeliveryManager.Instance.GetDeliverdRecipeCount().ToString();
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
}
