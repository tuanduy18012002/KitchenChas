using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateObject plateObject;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        ClearIcons();      
    }

    // Start is called before the first frame update
    void Start()
    {
        plateObject.OnAddKitchenObject += PlateObject_OnAddKitchenObject;
    }

    private void PlateObject_OnAddKitchenObject(object sender, PlateObject.OnAddKitchenObjectEventArgs e)
    {
        //Add Icon
        Transform iconTransform = Instantiate(iconTemplate, transform);
        iconTransform.GetComponent<PlateSingleIcon>().SetImage(e.kitchenObjectSO);
    }

    private void ClearIcons()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
