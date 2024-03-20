using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnVisual;
    [SerializeField] private GameObject sizzlingParticles;

    private void Start()
    {
        stoveCounter.OnFryingStateChanged += StoveCounter_OnFryingStateChanged;
    }

    private void StoveCounter_OnFryingStateChanged(object sender, StoveCounter.OnFryingStateChangedEventArgs e)
    {
        bool active = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        stoveOnVisual.SetActive(active);
        sizzlingParticles.SetActive(active);
    }
}
