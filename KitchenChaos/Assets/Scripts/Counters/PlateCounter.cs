using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    private float spawnTimer;
    private float spawnTimerMax = 4f;
    private int spawnAmount;
    private int spawnAmountMax = 4;
    [SerializeField] private KitchenObjectSO plateObjectSO;

    public event EventHandler OnSpawnPlate;
    public event EventHandler OnRemovePlate;

    private void Awake()
    {
        spawnTimer = 0f;
        spawnAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnAmount < spawnAmountMax)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnTimerMax) {
                spawnTimer = 0f;
                spawnAmount++;

                OnSpawnPlate?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //Player does not carry anything
            if (spawnAmount > 0)
            {
                //There's at least one Plate on Counter
                spawnAmount--;
                OnRemovePlate?.Invoke(this, EventArgs.Empty);
                SpawnKitchenObject(plateObjectSO, player);
            }
        }
    }
}
