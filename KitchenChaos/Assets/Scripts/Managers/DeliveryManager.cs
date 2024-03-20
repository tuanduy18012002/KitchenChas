using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnTimer;
    private float spawnTimerMax = 4f;
    private int spawnCountMax = 4;
    private int deliverdRecipeCount;

    public event EventHandler OnDeliveryFail;
    public event EventHandler OnDeliverySuccess;
    public event EventHandler<RecipeEventArgs> OnAddRecipe;
    public event EventHandler<RecipeEventArgs> OnRemoveRecipe;
    public class RecipeEventArgs : EventArgs
    {
        public RecipeSO recipe;
    }

    private void Awake()
    {
        Instance = this;
        spawnTimer = 0f;
        deliverdRecipeCount = 0;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        if (waitingRecipeSOList.Count < spawnCountMax)
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0f)
            {
                spawnTimer = spawnTimerMax;

                RecipeSO newRecipe = recipeListSO.recipeList[UnityEngine.Random.Range(0, recipeListSO.recipeList.Count)];
                waitingRecipeSOList.Add(newRecipe);
                OnAddRecipe?.Invoke(this, new RecipeEventArgs() { recipe = newRecipe });
            }
        }
    }

    public void DeliveryRecipe(PlateObject plateObject)
    {
        List<KitchenObjectSO> plateKOList = plateObject.GetKitchenObjectSOList();

        foreach (RecipeSO waitingecipeSO in waitingRecipeSOList)
        {
            //Check Amount
            if (plateKOList.Count == waitingecipeSO.kitchenObjectSOList.Count)
            {
                //Check ingredient
                bool check_correct = true;
                foreach (KitchenObjectSO plate_ingredient in plateKOList)
                {
                    //Check each plate_ingredient is the same as this recipe
                    bool check_ingredient = false;
                    foreach (KitchenObjectSO waiting_KO in waitingecipeSO.kitchenObjectSOList)
                    {
                        if (plate_ingredient == waiting_KO)
                        {
                            check_ingredient = true;
                            break;
                        }
                    }

                    if (!check_ingredient)
                    {
                        check_correct = false;
                        break;
                    }
                }

                if (check_correct)
                {
                    waitingRecipeSOList.Remove(waitingecipeSO);
                    deliverdRecipeCount++;
                    OnRemoveRecipe?.Invoke(this, new RecipeEventArgs() { recipe = waitingecipeSO });
                    OnDeliverySuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        Debug.Log("The plate is incorrect");
        OnDeliveryFail?.Invoke(this, EventArgs.Empty );
    }

    public int GetDeliverdRecipeCount()
    {
        return deliverdRecipeCount;
    }
}
