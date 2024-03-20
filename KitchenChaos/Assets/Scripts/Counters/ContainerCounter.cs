using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public event EventHandler eventOpenCloseContainerCounter;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            BaseCounter.SpawnKitchenObject(kitchenObjectSO, player);
            eventOpenCloseContainerCounter?.Invoke(this, EventArgs.Empty);
        }
    }
}
