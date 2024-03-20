using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticDataManger : MonoBehaviour
{
    private void Awake()
    {
        CuttingCounter.ResetStaticData();
        TrashCounter.ResetStaticData();
    }
}
