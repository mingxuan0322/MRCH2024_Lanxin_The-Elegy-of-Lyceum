using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[AddComponentMenu("Interact/Touchable Object")]
public class TouchableObject : MonoBehaviour
{
    [Title("Setting")] public bool isReturn = false;
    [SerializeField,HideIf("isReturn"),Space] private UnityEvent onTouchEvent;
    [SerializeField,ShowIf("isReturn"),InfoBox("It would trigger the OnReturnEvent first then the UniversalReturnEvent on TouchManager"),Space] 
    private UnityEvent onReturnEvent;

    private void Start()
    {
        if(gameObject.layer == 0)
            Debug.LogWarning($"{gameObject.name} is on the Default Layer, do you need to assign it to a specific touchable layer?");
    }

    public void OnTouch()
    {
        if (isReturn)
        {
            Debug.Log($"Return event on {gameObject.name} triggerred");
            onReturnEvent?.Invoke();
        }
        else
        {
            Debug.Log($"Touch event on {gameObject.name} triggerred");
            onTouchEvent?.Invoke();
        }
        
    }
}
