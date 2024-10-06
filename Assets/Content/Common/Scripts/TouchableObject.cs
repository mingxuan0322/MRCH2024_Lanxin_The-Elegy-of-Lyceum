using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[AddComponentMenu("Interact/Touchable Object")]
public class TouchableObject : MonoBehaviour
{
    [SerializeField] private UnityEvent onTouchEvent;
    
    public void OnTouch()
    {
        onTouchEvent?.Invoke();
    }
}
