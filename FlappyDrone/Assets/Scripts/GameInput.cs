using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameInput : MonoBehaviour
{
    // Events
    public UnityEvent OnFlap;

    
    public void TriggerFlap()
    {
            OnFlap?.Invoke();
    }
}
