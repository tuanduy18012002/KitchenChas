using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO() { return kitchenObjectSO; }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenOP)
    {
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        this.kitchenObjectParent = kitchenOP;

        if (kitchenOP.HasKitchenObject())
        {
            Debug.Log("KitchenObjectParent already has a KitchenObject!");
        }

        kitchenOP.SetKitchenObject(this);

        //Move to another counter
        transform.parent = kitchenOP.GetHoldPoint();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GeKitchenObjectParent() { return kitchenObjectParent; }

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateObject plate)
    {
        if (this is PlateObject)
        {
            plate = this as PlateObject;
            return true;
        }
        else
        {
            plate = null;
            return false;
        }
    }
}
