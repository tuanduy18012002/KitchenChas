using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameUI : MonoBehaviour
{
    public static PauseGameUI Instance { get; private set; }

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        Instance = this;

        resumeButton.onClick.AddListener(() =>
        {
            KitchenGameManager.Instance.TogglePauseGame();
        });

        optionsButton.onClick.AddListener(() =>
        {
            Hide();
            OptionsUI.Instance.Show();
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            SceneLoader.Load(SceneLoader.Scene.MainMenuScene);
        });
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnPausedGame += KitchenGameManager_OnPausedGame;
        KitchenGameManager.Instance.OnUnPausedGame += KitchentGameManager_OnUnPausedGame;
        Hide();
    }

    private void KitchentGameManager_OnUnPausedGame(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void KitchenGameManager_OnPausedGame(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        resumeButton.Select();
        gameObject.SetActive(true);
    }
}
