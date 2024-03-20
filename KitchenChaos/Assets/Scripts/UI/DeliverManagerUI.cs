using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Switch;

public class DeliverManagerUI : MonoBehaviour
{
    [SerializeField] private Transform recipeTemplate;
    [SerializeField] private Transform container;

    private void Awake()
    {
        Clear();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnAddRecipe += DeliveryManager_OnAddRecipe;
        DeliveryManager.Instance.OnRemoveRecipe += DeliveryManager_OnRemoveRecipe;
    }

    private void DeliveryManager_OnRemoveRecipe(object sender, DeliveryManager.RecipeEventArgs e)
    {
        foreach (Transform child in container)
        {
            DeliverySingleUI recipeTemplate = child.gameObject.GetComponent<DeliverySingleUI>();
            if (recipeTemplate.GetRecipeName() == e.recipe.recipeName)
            {
                Destroy(child.gameObject);
                return;
            }
        }
    }

    private void DeliveryManager_OnAddRecipe(object sender, DeliveryManager.RecipeEventArgs e)
    {
        Transform newRcipe = Instantiate(recipeTemplate, container);
        newRcipe.gameObject.GetComponent<DeliverySingleUI>().SetRecipe(e.recipe);
    }

    private void Clear()
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }
}
