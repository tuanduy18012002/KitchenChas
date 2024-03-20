using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static CuttingCounter;
using static IHasProcessBar;

public class StoveCounter : BaseCounter, IHasProcessBar
{
    private const string MEATPATTYCOOKED = "MeatPattyCooked";

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    private float fryingTime;
    private FryingRecipeSO fryingRecipeSO;
    private State state;

    public bool isFried { get; set; }
    public event EventHandler<ProcessChangedEventArgs> processChanged;
    public event EventHandler<OnFryingStateChangedEventArgs> OnFryingStateChanged;
    public class OnFryingStateChangedEventArgs : EventArgs
    {
       public State state;
    }
    public enum State
    {
        Idle, Frying, Fried, Burned
    }

    private void Start()
    {
        StateChanged(State.Idle);
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTime += Time.deltaTime;
                    processChanged?.Invoke(this, new IHasProcessBar.ProcessChangedEventArgs() { processPerMax =  fryingTime / fryingRecipeSO.fryingTimerMax});
                    if (fryingTime >= fryingRecipeSO.fryingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        SpawnKitchenObject(fryingRecipeSO.output, this);
                        StateChanged(State.Fried);
                        fryingRecipeSO = GetFryingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());
                        fryingTime = 0f;
                    }
                    break;
                case State.Fried:
                    fryingTime += Time.deltaTime;
                    processChanged?.Invoke(this, new IHasProcessBar.ProcessChangedEventArgs() { processPerMax = fryingTime / fryingRecipeSO.fryingTimerMax });
                    if (fryingTime >= fryingRecipeSO.fryingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        SpawnKitchenObject(fryingRecipeSO.output, this);
                        StateChanged(State.Burned);
                        processChanged?.Invoke(this, new IHasProcessBar.ProcessChangedEventArgs() { processPerMax = 0f });
                        fryingTime = 0f;
                    }
                    break;
                case State.Burned:
                    break;
            }
        }
    }

    public override void Interact(Player player)
    {
        //Counter has not anything
        if (!HasKitchenObject())
        {
            //Player is carring kitchen object and the kitchen object is valid
            if (player.HasKitchenObject() && IsInFryingRecipe(player.GetKitchenObject().GetKitchenObjectSO()))
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);

                fryingTime = 0f;
                if (GetKitchenObject().GetKitchenObjectSO().name == MEATPATTYCOOKED)
                {
                    StateChanged(State.Fried);
                }
                else
                {
                    StateChanged(State.Frying);
                }
                fryingRecipeSO = GetFryingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());
                
            }
        }
        else //Counter has KitchenObject
        {
            //Player is not carring anything
            if (!player.HasKitchenObject())
            {
                kitchenObject.SetKitchenObjectParent(player);
                fryingTime = 0f;
                StateChanged(State.Idle);
                processChanged?.Invoke(this, new IHasProcessBar.ProcessChangedEventArgs() { processPerMax = 0f});
            } else
            {
                //Player is carring plate
                if (player.GetKitchenObject().TryGetPlate(out PlateObject plateObject))
                {
                    if (plateObject.TryAddIgredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                        fryingTime = 0f;
                        StateChanged(State.Idle);
                        processChanged?.Invoke(this, new IHasProcessBar.ProcessChangedEventArgs() { processPerMax = 0f });
                    }
                }
            }
        }
    }

    private bool IsInFryingRecipe(KitchenObjectSO input)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOFromInput(input);
        return fryingRecipeSO != null;
    }

    private FryingRecipeSO GetFryingRecipeSOFromInput(KitchenObjectSO input)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == input)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }

    private void StateChanged(State newState)
    {
        this.state = newState;
        OnFryingStateChanged?.Invoke(this, new OnFryingStateChangedEventArgs() { state = newState});
        isFried = state == State.Fried;
    }
}
