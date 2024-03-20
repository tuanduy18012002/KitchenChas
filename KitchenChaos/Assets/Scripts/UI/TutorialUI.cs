using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    private const int MOVE_UP = 0;
    private const int MOVE_DOWN = 1;
    private const int MOVE_LEFT = 2;
    private const int MOVE_RIGHT = 3;
    private const int INTERACT = 4;
    private const int INTERACT_ALTERNATE = 5;
    private const int PAUSE = 6;
    private const int INTERACT_CONTROLLER = 7;
    private const int INTERACT_ALT_CONTROLLER = 8;
    private const int PAUSE_CONTROLLER = 9;

    [SerializeField] List<TextMeshProUGUI> keyButtonTextArray;

    private void Start()
    {
        Show();
        InputManager.Instance.OnTutorialEnd += InputManager_OnTutorialEnd;
    }

    private void InputManager_OnTutorialEnd(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
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

    private void Show()
    {
        UpdateVisual();
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
}
