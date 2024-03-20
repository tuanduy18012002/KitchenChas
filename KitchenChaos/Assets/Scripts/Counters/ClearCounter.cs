using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        //Counter has not anything
        if (!HasKitchenObject())
        {
            //Player is carring kitchen object
            if (player.HasKitchenObject()) 
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        } else //Counter has KitchenObject
        {
            //Player is not carring anything
            if (!player.HasKitchenObject())
            {
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
                else
                {
                    if (GetKitchenObject().TryGetPlate(out plateObject))
                    {
                        if (plateObject.TryAddIgredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
        }
    }
}
