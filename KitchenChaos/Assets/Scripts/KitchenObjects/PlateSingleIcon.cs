using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateSingleIcon : MonoBehaviour
{
    [SerializeField] private Image image;

    public void SetImage(KitchenObjectSO kitchenObjectSO)
    {
        this.image.sprite = kitchenObjectSO.sprite;
    }
}
