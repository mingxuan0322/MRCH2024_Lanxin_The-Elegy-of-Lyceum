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
    [Required,InfoBox("Assign this and all touchable Objects to a (special) layer", InfoMessageType.Warning, "TouchableLayerAssigned")]
    public LayerMask touchableLayer; // Assign this in the Inspector to include only the touchable objects
    private bool TouchableLayerAssigned => touchableLayer == 0;

    private static bool _isTouchable = true;
    
    [Space(10), Header("Universal Touch Event"), SerializeField]
    private UnityEvent universalTouchEvent;
    [SerializeField] private UnityEvent universalReturnEvent;

    [Title("Setting"), PropertyRange(0f,300f), SerializeField]
    private float touchRange;
    
    private Camera _mainCam;

    [SerializeField,InfoBox("Enabx1le this if you want other objects to be unable to interact after one is touched"),Tooltip("Enable this if you want other objects to be unable to interact after one is touched")] 
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
        if (Input.touchCount <= 0 && !Input.GetMouseButtonDown(0)) return; //touch exists
        
        Vector3 inputPosition;
        
        if (Input.touchCount > 0) // Touch input
        {
            Touch touch = Input.GetTouch(0);
            inputPosition = touch.position;

            if (touch.phase != TouchPhase.Began) return; 
        }
        else // Mouse input
        {
            inputPosition = Input.mousePosition;

            if (!Input.GetMouseButtonDown(0)) return; 
        }
        
        var ray = _mainCam.ScreenPointToRay(inputPosition);
        if (Physics.Raycast(ray, out var hit, touchRange, touchableLayer))
        {
            var touchable = hit.transform.GetComponent<TouchableObject>();
            if (touchable)
            {
                if (touchable.isReturn)
                {
                    universalTouchEvent?.Invoke();
                    touchable.OnTouch();
                
                    if(touchable.isReturn) OnReturn();
                }
                else
                {
                    if(disableTouchOfOtherObjects && !_isTouchable) return;
                    
                    if (disableTouchOfOtherObjects)
                        _isTouchable = false;
                    Debug.Log("Universal Touch/Click Event triggered");
                    universalTouchEvent?.Invoke();
                    touchable.OnTouch();
                }
            }
            else
            {
                Debug.LogWarning(hit.transform.name + " has no TouchableObject component");
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
