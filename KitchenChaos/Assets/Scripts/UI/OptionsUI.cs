using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";
    public const string PLAYER_PREFS_SOUND_VOLUME = "SoundVolume";
    public const int MOVE_UP = 0;
    public const int MOVE_DOWN = 1;
    public const int MOVE_LEFT = 2;
    public const int MOVE_RIGHT = 3;
    public const int INTERACT = 4;
    public const int INTERACT_ALTERNATE = 5;
    public const int PAUSE = 6;
    public const int INTERACT_CONTROLLER = 7;
    public const int INTERACT_ALT_CONTROLLER = 8;
    public const int PAUSE_CONTROLLER = 9;

    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButon;
    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private List<Button> keyButtonArray;
    [SerializeField] private List<TextMeshProUGUI> keyButtonTextArray;
    [SerializeField] private Transform pressKeyUI;
    private bool isShow;

    private void Awake()
    {
        Instance = this;

        soundEffectButton.onClick.AddListener(() =>
        {
            ChangeSoundEffectText(SoundManager.Instance.IncreaseVolume());     
        });

        musicButton.onClick.AddListener(() =>
        {
            ChangeMusicText(MusicManager.Instance.IncreaseVolume());
        });

        closeButon.onClick.AddListener(() =>
        {
            PauseGameUI.Instance.Show();
            Hide();
        });

        keyButtonArray[MOVE_UP].onClick.AddListener(() =>
        {
            pressKeyUI.gameObject.SetActive(true);
            InputManager.Instance.Rebind(InputManager.Binding.MoveUp);
        });
        keyButtonArray[MOVE_DOWN].onClick.AddListener(() =>
        {
            pressKeyUI.gameObject.SetActive(true);
            InputManager.Instance.Rebind(InputManager.Binding.MoveDown);
        });
        keyButtonArray[MOVE_LEFT].onClick.AddListener(() =>
        {
            pressKeyUI.gameObject.SetActive(true);
            InputManager.Instance.Rebind(InputManager.Binding.MoveLeft);
        });
        keyButtonArray[MOVE_RIGHT].onClick.AddListener(() =>
        {
            pressKeyUI.gameObject.SetActive(true);
            InputManager.Instance.Rebind(InputManager.Binding.MoveRight);
        });
        keyButtonArray[INTERACT].onClick.AddListener(() =>
        {
            pressKeyUI.gameObject.SetActive(true);
            InputManager.Instance.Rebind(InputManager.Binding.Interact);
        });
        keyButtonArray[INTERACT_ALTERNATE].onClick.AddListener(() =>
        {
            pressKeyUI.gameObject.SetActive(true);
            InputManager.Instance.Rebind(InputManager.Binding.InteractAlt);
        });
        keyButtonArray[PAUSE].onClick.AddListener(() =>
        {
            pressKeyUI.gameObject.SetActive(true);
            InputManager.Instance.Rebind(InputManager.Binding.Pause);
        });
        keyButtonArray[INTERACT_CONTROLLER].onClick.AddListener(() =>
        {
            pressKeyUI.gameObject.SetActive(true);
            InputManager.Instance.Rebind(InputManager.Binding.Interact_CTRLer);
        });
        keyButtonArray[INTERACT_ALT_CONTROLLER].onClick.AddListener(() =>
        {
            pressKeyUI.gameObject.SetActive(true);
            InputManager.Instance.Rebind(InputManager.Binding.InteractAlt_CTRLer);
        });
        keyButtonArray[PAUSE_CONTROLLER].onClick.AddListener(() =>
        {
            pressKeyUI.gameObject.SetActive(true);
            InputManager.Instance.Rebind(InputManager.Binding.Pause_CTRLer);
        });
    }

    private void Start()
    {
        ChangeSoundEffectText(SoundManager.Instance.GetVolume());
        ChangeMusicText(MusicManager.Instance.GetVolume());
        ResetAllKeyButtonText();
        pressKeyUI.gameObject.SetActive(false);
        Hide();
    }

    public void Show()
    {
        isShow = true;
        soundEffectButton.Select();
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        isShow = false;
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_VOLUME, SoundManager.Instance.GetVolume());
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, MusicManager.Instance.GetVolume());
        PlayerPrefs.Save();
        InputManager.Instance.SaveInputAction();
        gameObject.SetActive(false);
    }

    private void ChangeSoundEffectText(float volume)
    {
        soundEffectText.text = "Sound Effect Volume: " + Mathf.Round(volume * 10f);
    }

    private void ChangeMusicText(float volume)
    {
        musicText.text = "Music Volume: " + Mathf.Round(volume * 10f);
    }

    public void ChangeKeyButtonText(int typeKey, string value)
    {
        pressKeyUI.gameObject.SetActive(false);
        keyButtonTextArray[typeKey].text = value;
    }

    private void ResetAllKeyButtonText ()
    {
        keyButtonTextArray[MOVE_UP].text = InputManager.Instance.GetBinding(InputManager.Binding.MoveUp);
        keyButtonTextArray[MOVE_DOWN].text = InputManager.Instance.GetBinding(InputManager.Binding.MoveDown);
        keyButtonTextArray[MOVE_LEFT].text = InputManager.Instance.GetBinding(InputManager.Binding.MoveLeft);
        keyButtonTextArray[MOVE_RIGHT].text = InputManager.Instance.GetBinding(InputManager.Binding.MoveRight);
        keyButtonTextArray[INTERACT].text = InputManager.Instance.GetBinding(InputManager.Binding.Interact);
        keyButtonTextArray[INTERACT_ALTERNATE].text = InputManager.Instance.GetBinding(InputManager.Binding.InteractAlt);
        keyButtonTextArray[PAUSE].text = InputManager.Instance.GetBinding(InputManager.Binding.Pause);
        keyButtonTextArray[INTERACT_CONTROLLER].text = InputManager.Instance.GetBinding(InputManager.Binding.Interact_CTRLer);
        keyButtonTextArray[INTERACT_ALT_CONTROLLER].text = InputManager.Instance.GetBinding(InputManager.Binding.InteractAlt_CTRLer);
        keyButtonTextArray[PAUSE_CONTROLLER].text = InputManager.Instance.GetBinding(InputManager.Binding.Pause_CTRLer);
    }

    public bool IsShow()
    {
        return isShow;
    }
}
