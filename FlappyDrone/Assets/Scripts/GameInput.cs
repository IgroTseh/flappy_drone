using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameInput : MonoBehaviour
{
    // Events
    public UnityEvent OnFlap;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerFlap();
        }
    }

    public void TriggerFlap()
    {
            OnFlap?.Invoke();
    }
}
