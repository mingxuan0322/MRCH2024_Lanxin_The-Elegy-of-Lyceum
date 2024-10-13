using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Interact/Touchable Object Manager")]
public class TouchManager : MonoBehaviour
{
    [InfoBox("Add Collider and TouchableObject.cs to the object you want to be touchable")]
    [Required,InfoBox("Assign this and all touchable Objects to a (special) layer", InfoMessageType.Warning)]
    public LayerMask touchableLayer; // Assign this in the Inspector to include only the touchable objects

    private static bool _isTouchable = true;
    
    [Space(10), Header("Universal Touch Event"), SerializeField]
    private UnityEvent universalTouchEvent;
    [SerializeField] private UnityEvent universalReturnEvent;

    [Title("Setting"), Wrap(0f,Mathf.Infinity), SerializeField]
    private float touchRange;
    
    private Camera _mainCam;

    [SerializeField,InfoBox("Enable this if you want other objects to be unable to interact after one is touched"),Tooltip("Enable this if you want other objects to be unable to interact after one is touched")] 
    private bool disableTouchOfOtherObjects;
    
    private void Start()
    {
        if (Camera.main == null)
        {
            Debug.LogError("Main Camera not found!!!");
        }
        _mainCam = Camera.main;
        
        if(touchableLayer == 0)
            Debug.LogWarning("Please check if you forgot to assign the touchable layer on " + gameObject.name);
    }

    private void Update()
    {
        if (Input.touchCount <= 0 || (disableTouchOfOtherObjects && !_isTouchable)) return; //touch exists
        
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            Ray ray = _mainCam.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray, out var hit, touchRange, touchableLayer))
            {
                var touchable = hit.transform.GetComponent<TouchableObject>();
                if (touchable)
                {
                    if(disableTouchOfOtherObjects)
                        _isTouchable = false;
                    Debug.Log("Universal Touch Event triggered");
                    universalTouchEvent?.Invoke();
                    touchable.OnTouch();
                }
            }
        }
    }

    public void OnReturn()
    {
        if(disableTouchOfOtherObjects)
            _isTouchable = true;
        Debug.Log("Universal Return Event triggered");
        universalReturnEvent?.Invoke();
    }
}
