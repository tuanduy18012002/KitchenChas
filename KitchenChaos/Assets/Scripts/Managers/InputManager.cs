using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private const string PLAYER_INPUT_ACTION = "PlayerInputAction";
    private PlayerInputAction playerInputAction;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPausePressed;
    public event EventHandler OnTutorialEnd;
    public static InputManager Instance { get; private set; }

    public enum Binding
    {
        MoveUp, MoveDown, MoveLeft, MoveRight, Interact, InteractAlt, Pause, Interact_CTRLer, InteractAlt_CTRLer, Pause_CTRLer
    }

    private void Awake()
    {
        Instance = this;
        playerInputAction = new PlayerInputAction();
        if (PlayerPrefs.HasKey(PLAYER_INPUT_ACTION))
        {
            playerInputAction.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_INPUT_ACTION));
        }
        playerInputAction.Player.Enable();
        playerInputAction.UI.Enable();

        playerInputAction.Player.Interact.performed += Interact_performed;
        playerInputAction.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputAction.UI.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        playerInputAction.Player.Interact.performed -= Interact_performed;
        playerInputAction.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputAction.UI.Pause.performed -= Pause_performed;

        playerInputAction.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPausePressed?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) { return; }

        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (KitchenGameManager.Instance.IsWaitingToStart()) 
        { 
            OnTutorialEnd?.Invoke(this, EventArgs.Empty);
        }

        if (!KitchenGameManager.Instance.IsGamePlaying()) { return; }

        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    //Get input normalized of player's movement
    public Vector2 GetPlayerInputMovement()
    {
        Vector2 inputMove = playerInputAction.Player.Move.ReadValue<Vector2>();
        return inputMove.normalized;       //Normalize the vector so the character doesn't move faster when walking diagonally
    }

    public string GetBinding(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.MoveUp:
                return playerInputAction.Player.Move.bindings[1].ToDisplayString();
            case Binding.MoveDown:
                return playerInputAction.Player.Move.bindings[2].ToDisplayString();
            case Binding.MoveLeft:
                return playerInputAction.Player.Move.bindings[3].ToDisplayString();
            case Binding.MoveRight:
                return playerInputAction.Player.Move.bindings[4].ToDisplayString();
            case Binding.Interact:
                return playerInputAction.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlt:
                return playerInputAction.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Binding.Pause: 
                return playerInputAction.UI.Pause.bindings[0].ToDisplayString();
            case Binding.Interact_CTRLer:
                return playerInputAction.Player.Interact.bindings[1].ToDisplayString();
            case Binding.InteractAlt_CTRLer:
                return playerInputAction.Player.InteractAlternate.bindings[1].ToDisplayString();
            case Binding.Pause_CTRLer:
                return playerInputAction.UI.Pause.bindings[1].ToDisplayString();
        }
    }

    public void Rebind(Binding binding)
    {
        playerInputAction.Disable();
        InputAction inputAction = null;
        int index = 0;
        int indexKey = 0;

        switch(binding)
        {
            case Binding.MoveUp:
                inputAction = playerInputAction.Player.Move;
                index = 1;
                indexKey = 0;
                break;
            case Binding.MoveDown:
                inputAction = playerInputAction.Player.Move;
                index = 2;
                indexKey = 1;
                break;
            case Binding.MoveLeft:
                inputAction = playerInputAction.Player.Move;
                index = 3;
                indexKey = 2;
                break;
            case Binding.MoveRight:
                inputAction = playerInputAction.Player.Move;
                index = 4;
                indexKey = 3;
                break;
            case Binding.Interact:
                inputAction = playerInputAction.Player.Interact;
                index = 0;
                indexKey = 4;
                break;
            case Binding.InteractAlt:
                inputAction = playerInputAction.Player.InteractAlternate;
                index = 0;
                indexKey = 5;
                break;
            case Binding.Pause:
                inputAction = playerInputAction.UI.Pause;
                index = 0;
                indexKey = 6;
                break;
            case Binding.Interact_CTRLer:
                inputAction = playerInputAction.Player.Interact;
                index = 1;
                indexKey = 7;
                break;
            case Binding.InteractAlt_CTRLer:
                inputAction = playerInputAction.Player.InteractAlternate;
                index = 1;
                indexKey = 8;
                break;
            case Binding.Pause_CTRLer:
                inputAction = playerInputAction.UI.Pause;
                index = 1;
                indexKey = 9;
                break;
        }

        inputAction.PerformInteractiveRebinding(index)
            .OnComplete(callback =>
            {
                OptionsUI.Instance.ChangeKeyButtonText(indexKey, GetBinding(binding));
                callback.Dispose();
                playerInputAction.Enable();
            }).Start();
    }

    public void SaveInputAction()
    {
        PlayerPrefs.SetString(PLAYER_INPUT_ACTION, playerInputAction.SaveBindingOverridesAsJson());
        PlayerPrefs.Save();
    }
}
