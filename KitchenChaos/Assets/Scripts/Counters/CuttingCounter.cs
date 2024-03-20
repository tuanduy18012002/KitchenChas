using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IHasProcessBar;

public class CuttingCounter : BaseCounter, IHasProcessBar {
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    private int cuttingProcess;

    public bool isFried { get; set; }
    public event EventHandler<ProcessChangedEventArgs> processChanged;
    public event EventHandler cuttingAnimation;
    public static event EventHandler OnAnyCutting;

    public static void ResetStaticData()
    {
        OnAnyCutting = null;
    }

    public override void Interact(Player player)
    {
        //Counter has not anything
        if (!HasKitchenObject())
        {
            //Player is carring kitchen object and the kitchen object is valid
            if (player.HasKitchenObject() && IsInCuttingRecipe(player.GetKitchenObject().GetKitchenObjectSO()))
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                cuttingProcess = 0;
                processChanged?.Invoke(this, new IHasProcessBar.ProcessChangedEventArgs(){ processPerMax = 0f });
            }
        }
        else //Counter has KitchenObject
        {
            //Player is not carring anything
            if (!player.HasKitchenObject())
            {
                processChanged?.Invoke(this, new IHasProcessBar.ProcessChangedEventArgs() { processPerMax = 0f});
                kitchenObject.SetKitchenObjectParent(player);
            } else
            {
                //Player is carring plate
                if (player.GetKitchenObject().TryGetPlate(out PlateObject plateObject))
                {
                    if (plateObject.TryAddIgredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && !player.HasKitchenObject())
        {
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());

            if (cuttingRecipeSO != null)
            {
                cuttingProcess++;
                processChanged?.Invoke(this, new IHasProcessBar.ProcessChangedEventArgs() { processPerMax = (float) cuttingProcess / cuttingRecipeSO.cuttingPrecessMax});
                cuttingAnimation?.Invoke(this, EventArgs.Empty);
                OnAnyCutting?.Invoke(this, EventArgs.Empty);

                if (cuttingProcess >= cuttingRecipeSO.cuttingPrecessMax)
                {
                    kitchenObject.DestroySelf();
                    BaseCounter.SpawnKitchenObject(cuttingRecipeSO.output, this);
                }
            }
        }
    }

    private bool IsInCuttingRecipe(KitchenObjectSO input)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOFromInput(input);
        return cuttingRecipeSO != null;
    }

    private CuttingRecipeSO GetCuttingRecipeSOFromInput(KitchenObjectSO input)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == input)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}