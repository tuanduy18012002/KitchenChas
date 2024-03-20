using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> vailidKitchenObjectSOList;
    private List<KitchenObjectSO> kitchenObjectSOList;

    public event EventHandler<OnAddKitchenObjectEventArgs> OnAddKitchenObject;
    public class OnAddKitchenObjectEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIgredient(KitchenObjectSO obj)
    {
        if (!vailidKitchenObjectSOList.Contains(obj))
        {
            return false;
        }

        if (kitchenObjectSOList.Contains(obj))
        {
            return false;
        }
        else
        {
            kitchenObjectSOList.Add(obj);
            OnAddKitchenObject?.Invoke(this, new OnAddKitchenObjectEventArgs() { kitchenObjectSO = obj });
            return true;
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
