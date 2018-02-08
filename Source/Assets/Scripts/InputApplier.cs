using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputApplier : MonoBehaviour
{
    public Action<Vector2> OnPlayerClicked;

    void Update()
    {
        if(Input.anyKeyDown && OnPlayerClicked != null)
            OnPlayerClicked.Invoke(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        #if UNITY_ANDROID && !UNITY_EDITOR

                if (Input.touchCount > 0)
           {
              OnPlayerClicked.Invoke(Camera.main.ScreenToWorldPoint(Input.mousePosition));
           }
        #endif
    }
}
