using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform counterTopPoint;

    protected KitchenObject kitchenObject;

    public virtual void Interact(Player player) 
    {
        Debug.LogWarning("BaseCounter.Interact()!");
    }

    public virtual void InteractAlternate(Player player) 
    {
        Debug.LogWarning("BaseCounter.InteractAlternate()!");
    }

    public Transform GetHoldPoint() { return counterTopPoint; }

    public KitchenObject GetKitchenObject() { return kitchenObject; }

    public void SetKitchenObject(KitchenObject kitchenObject) { this.kitchenObject = kitchenObject; }

    public void ClearKitchenObject() { this.kitchenObject = null; }

    public bool HasKitchenObject() { return kitchenObject != null; }

    public static void SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent parent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefabOject);
        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(parent);
    }
}
