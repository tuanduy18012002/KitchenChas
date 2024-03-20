using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private Transform plateVisualPrefab;
    [SerializeField] private PlateCounter plateCounter;
    [SerializeField] private Transform counterTopPoint;

    private List<GameObject> plateVisualGameObjectList;

    private void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>();
    }

    private void Start()
    {
        plateCounter.OnSpawnPlate += PlateCounter_OnSpawnPlate;
        plateCounter.OnRemovePlate += PlateCounter_OnRemovePlate;
    }

    private void PlateCounter_OnRemovePlate(object sender, System.EventArgs e)
    {
        GameObject plateVisualGO = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];

        plateVisualGameObjectList.Remove(plateVisualGO);
        Destroy(plateVisualGO);
    }

    private void PlateCounter_OnSpawnPlate(object sender, System.EventArgs e)
    {
        Transform plateVisual =  Instantiate(plateVisualPrefab, counterTopPoint);

        float plateOffsetY = 0.1f;
        plateVisual.localPosition = new Vector3(0f, plateOffsetY * plateVisualGameObjectList.Count, 0f);
        plateVisualGameObjectList.Add(plateVisual.gameObject);
    }
}
