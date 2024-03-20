using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCallBack : MonoBehaviour
{
    private bool isFirstUpdate = true;

    // Update is called once per frame
    void Update()
    {
        if (isFirstUpdate)
        {
            isFirstUpdate = false;

            SceneLoader.LoaderCallBack();
        }
    }
}
