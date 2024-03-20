using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryGetPlate(out PlateObject plate))
            {
                //Check plate
                DeliveryManager.Instance.DeliveryRecipe(plate);

                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
