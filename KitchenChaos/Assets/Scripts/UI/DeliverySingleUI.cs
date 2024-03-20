using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliverySingleUI : MonoBehaviour
{
    private const string ImgeTextCode = "Imgae";
    
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    public void SetRecipe(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.recipeName;

        foreach(KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Transform newIcon = Instantiate(iconTemplate, iconContainer);
            newIcon.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }

    public string GetRecipeName()
    {
        return recipeNameText.text;
    }
}
