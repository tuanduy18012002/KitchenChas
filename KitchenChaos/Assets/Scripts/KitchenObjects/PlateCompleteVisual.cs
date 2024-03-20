using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [SerializeField] private PlateObject plateObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjects;
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    private void Awake()
    {
        foreach (KitchenObjectSO_GameObject obj in kitchenObjects)
        {
            obj.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        plateObject.OnAddKitchenObject += PlateObject_OnAddKitchenObject;
    }

    private void PlateObject_OnAddKitchenObject(object sender, PlateObject.OnAddKitchenObjectEventArgs e)
    {
        foreach (KitchenObjectSO_GameObject obj in kitchenObjects)
        {
            if (obj.kitchenObjectSO == e.kitchenObjectSO)
            {
                obj.gameObject.SetActive(true);
                return;
            }
        }
    }
}
