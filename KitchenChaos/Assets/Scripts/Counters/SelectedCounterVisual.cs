using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;

    // Start is called before the first frame update
    void Start()
    {
        Player.Instance.OnSeclectedCounterChanged += Player_OnSeclectedCounterChanged;
    }

    private void Player_OnSeclectedCounterChanged(object sender, Player.OnSeclectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter != baseCounter)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(false);
        }
    }
}
