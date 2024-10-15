using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public abstract class MoveAndRotate : MonoBehaviour
{
    [Title("Move Options"),Required] 
    public Transform moveTarget; 
    [Unit(Units.MetersPerSecond)]
    public float moveSpeed = 2f;    
    [HideIf("moveForthAndBackOnEnable")]
    public bool moveForOnceOnEnable = false;
    [HideIf("moveForOnceOnEnable")]
    public bool moveForthAndBackOnEnable = false;

    [Space, SerializeField] 
    protected Ease moveType = Ease.InOutSine;

    [Title("Rotate Options")]
    public bool keepRotatingOnEnable = false;      
    public Vector3 rotationAxis = Vector3.up;  
    
    [Unit(Units.Second)]
    public float rotateDuration = 10f;

    [Space, SerializeField] private Ease rotateType = Ease.Linear;

    private Vector3 _initalPosition;
    
    private Tween _moveTween;
    private Tween _rotateTween;

    protected virtual void Awake()
    {
        _initalPosition = transform.position;
    }

    protected virtual  void OnEnable()
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

    public virtual void MoveForOnce()
    {
        if (!moveTarget)
        {
            Debug.LogError("Move target not set on MoveAndRotate" + gameObject.name);
            return;
        }
        _moveTween = transform.DOMove(moveTarget.position, moveSpeed);
    }

    public virtual void MoveBackForOnce()
    {
        _moveTween = transform.DOMove(_initalPosition, moveSpeed);
    }

    public virtual void JumpBackToInitialPosition()
    {
        transform.position = _initalPosition;
    }

    public virtual void MoveForthAndBack()
    {
        if (!moveTarget)
        {
            Debug.LogError("Move target not set on MoveAndRotate" + gameObject.name);
            return;
        }
        
        _moveTween = transform.DOMove(moveTarget.position, moveSpeed)
            .SetEase(moveType)
            .SetLoops(-1, LoopType.Yoyo);  
    }

    public virtual void RotateObject()
    {
        _rotateTween = transform.DORotate(rotationAxis * 360, rotateDuration, RotateMode.FastBeyond360)
            .SetEase(rotateType)
            .SetLoops(-1, LoopType.Restart);  
    }

    public virtual void StopMovement()
    {
        _moveTween.Kill();
    }

    public virtual void StopRotation()
    {
        _rotateTween.Kill();
    }

    protected virtual void OnDisable()
    {
        // Kill all tweens on this GameObject when disabled
        transform.DOKill();
    }

}
