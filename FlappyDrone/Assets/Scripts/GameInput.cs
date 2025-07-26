using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameInput : MonoBehaviour
{
    // Events
    public UnityEvent OnFlap;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnFlap?.Invoke();
        }
    }
}
