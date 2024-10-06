using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class MoveAndRotate : MonoBehaviour
{
    [Title("Move Options"),Required] 
    public Transform moveTarget; 
    [Unit(Units.MetersPerSecond)]
    public float moveSpeed = 2f;    
    [HideIf("moveForthAndBackOnEnable")]
    public bool moveForOnceOnEnable = false;
    [HideIf("moveForOnceOnEnable")]
    public bool moveForthAndBackOnEnable = false;  

    [Title("Rotate Options")]
    public bool keepRotatingOnEnable = false;      
    public Vector3 rotationAxis = Vector3.up;  
    
    [Unit(Units.Second)]
    public float rotateDuration = 10f;

    private Vector3 _initalPosition;
    
    private Tween _moveTween;
    private Tween _rotateTween;

    private void Awake()
    {
        _initalPosition = transform.position;
    }

    private void OnEnable()
    {
        
        if (moveForthAndBackOnEnable)
        {
            MoveForthAndBack();
        }
        else if(moveForOnceOnEnable)
        {
            MoveForOnce();
        }
        
        if (keepRotatingOnEnable)
        {
            RotateObject();
        }
    }

    public void MoveForOnce()
    {
        if (!moveTarget)
        {
            Debug.LogError("Move target not set on MoveAndRotate" + gameObject.name);
            return;
        }
        _moveTween = transform.DOMove(moveTarget.position, moveSpeed);
    }

    public void MoveBackForOnce()
    {
        _moveTween = transform.DOMove(_initalPosition, moveSpeed);
    }

    public void JumpBackToInitialPosition()
    {
        transform.position = _initalPosition;
    }

    public void MoveForthAndBack()
    {
        if (!moveTarget)
        {
            Debug.LogError("Move target not set on MoveAndRotate" + gameObject.name);
            return;
        }
        
        _moveTween = transform.DOMove(moveTarget.position, moveSpeed)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo);  
    }

    private void RotateObject()
    {
        _rotateTween = transform.DORotate(rotationAxis * 360, rotateDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);  
    }

    public void StopMovement()
    {
        _moveTween.Kill();
    }

    public void StopRotation()
    {
        _rotateTween.Kill();
    }

    private void OnDisable()
    {
        // Kill all tweens on this GameObject when disabled
        transform.DOKill();
    }

}
