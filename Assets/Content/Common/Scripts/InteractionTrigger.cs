using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// author: Shengyang Billiton Peng
/// 
/// Enables three kinds of triggers to invoke events: collider, distance, and look at.
/// DO NOT CHANGE THE SCRIPT! COPY THE CODE TO YOUR FOLDER AND CHANGE THE CLASS NAME IF YOU WANT TO MODIFY IT.
/// You can inherit this class and override specific methods.
/// </summary>
public abstract class InteractionTrigger : MonoBehaviour 
{
    #region Variables

    #region Position

    [Header("Collider Trigger"), Space(5)] [SerializeField]
    private bool useColliderTrigger;

    private Collider _colliderTrigger;

    [Space(5)] [SerializeField] private UnityEvent onTriggerFirstEnter;
    [SerializeField] private UnityEvent onTriggerEnter;
    [SerializeField] private UnityEvent onTriggerExit;

    private bool _firstColliderEnter = true;

    #endregion

    #region Distance

    [Header("Distance Trigger"), Space(5)] [SerializeField]
    private bool useDistanceTrigger;

    [SerializeField] private float distance = 10f;

    [Space(5)] [SerializeField] private UnityEvent onDistanceFirstEnter;
    [SerializeField] private UnityEvent onDistanceEnter;
    [SerializeField] private UnityEvent onDistanceExit;

    private bool _firstDistanceEnter = true;
    private bool _alreadyInDistance;

    #endregion

    #region Lookat

    [Header("LookAt Trigger"), Space(5)] [SerializeField]
    private bool useLookAtTrigger;

    [SerializeField] private float lookAtAngle = 25f;
    [SerializeField] private float lookAtDistance;

    [Space(5)] [SerializeField] private UnityEvent onLookAtFirstEnter;
    [SerializeField] private UnityEvent onLookAtEnter;
    [SerializeField] private UnityEvent onLookAtDistanceExit;

    private bool _firstLookAtEnter = true;
    private bool _alreadyLookAt;

    #endregion

    #region "unity events"

    [Header("Events Triggers"), Space(5)]
    [SerializeField] private bool useEventsTriggers;

    [SerializeField] private bool useStartTrigger;
    [SerializeField] private UnityEvent onStart;

    [SerializeField] private bool useOnEnableTrigger;
    [SerializeField] private UnityEvent onEnable;

    [SerializeField] private bool useUpdateTrigger;
    [SerializeField] private UnityEvent onUpdate;

    [SerializeField] private bool useOnDisableTrigger;
    [SerializeField] private UnityEvent onDisable;

    #endregion

    #region Global Variables

    private GameObject _player;
    private Transform _playerTransform;
    private const int CheckRateFreq = 25;

    #endregion

    #endregion

    protected virtual void Start()
    {
        CheckAndInitSetting();

        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player == null)
            Debug.LogError("No player found in the scene");
        _playerTransform = _player.transform;

        if (useEventsTriggers && useStartTrigger)
            onStart?.Invoke();
    }

    protected virtual void OnEnable()
    {
        if (useEventsTriggers && useOnEnableTrigger)
            onEnable?.Invoke();
    }

    protected virtual void Update()
    {
        if (useDistanceTrigger)
        {
            if (!CheckRateLimiter(CheckRateFreq)) return;

            if (InDistance(distance) && !_alreadyInDistance)
            {
                if (_firstDistanceEnter)
                {
                    Debug.Log("onDistanceFirstEnter is triggered on " + gameObject.name);
                    onDistanceFirstEnter?.Invoke();
                    _firstDistanceEnter = false;
                }

                onDistanceEnter?.Invoke();
                _alreadyInDistance = true;
            }
            else if (!InDistance(distance) && _alreadyInDistance)
            {
                Debug.Log("onDistanceExit is triggered on " + gameObject.name);
                onDistanceExit?.Invoke();
                _alreadyInDistance = false;
            }

            if (useEventsTriggers && useUpdateTrigger)
                onUpdate?.Invoke();
        }

        if (useLookAtTrigger)
        {
            if (!CheckRateLimiter(CheckRateFreq)) return;
            if (InDistance(lookAtDistance) && !_alreadyLookAt)
            {
                if (Vector3.Angle(_playerTransform.forward, (transform.position - _playerTransform.position).normalized) <= lookAtAngle)
                {
                    if (_firstLookAtEnter)
                    {
                        Debug.Log("onLookAtFirstEnter is triggered on " + gameObject.name);
                        onLookAtFirstEnter?.Invoke();
                        _firstLookAtEnter = false;
                    }

                    onLookAtEnter?.Invoke();
                    _alreadyLookAt = true;
                }
            }
            else if (!InDistance(lookAtDistance) && _alreadyLookAt)
            {
                Debug.Log("onLookAtDistanceExit is triggered on " + gameObject.name);
                onLookAtDistanceExit?.Invoke();
                _alreadyLookAt = false;
            }
        }
    }

    protected virtual void OnDisable()
    {
        if (useEventsTriggers && useOnDisableTrigger)
            onDisable?.Invoke();
    }

    protected bool InDistance(float dist)
    {
        return Vector3.Distance(transform.position, _playerTransform.position) <= dist;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (!useColliderTrigger) return;
        if (!(other.CompareTag("Player") || other.CompareTag("MainCamera"))) return;

        if (_firstColliderEnter)
        {
            Debug.Log("onTriggerFirstEnter is triggered on " + gameObject.name);
            onTriggerFirstEnter?.Invoke();
            _firstColliderEnter = false;
        }

        Debug.Log("onTriggerEnter is triggered on " + gameObject.name);
        onTriggerEnter?.Invoke();
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (!useColliderTrigger) return;
        if (!other.CompareTag("Player")) return;

        Debug.Log("onTriggerExit is triggered on " + gameObject.name);
        onTriggerExit?.Invoke();
    }

    public virtual void OnTriggerStay(Collider other) {}

    protected void CheckAndInitSetting()
    {
        if (useColliderTrigger)
        {
            _colliderTrigger = GetComponent<Collider>();
            if (_colliderTrigger == null)
                Debug.LogError("Collider Trigger is enabled but no collider is attached to " + gameObject.name);
            else if (_colliderTrigger.isTrigger == false)
                Debug.LogWarning("YOU SHOULD PROBABLY TURN ON 'isTrigger' of the collider on " + gameObject.name);

            if (onTriggerFirstEnter == null && onTriggerEnter == null && onTriggerExit == null)
                Debug.LogWarning("No events are assigned to Collider Trigger on " + gameObject.name);
        }

        if (useDistanceTrigger)
        {
            if (distance == 0)
                Debug.LogWarning("Distance Trigger is enabled but distance is set to 0 on " + gameObject.name);

            if (onDistanceFirstEnter == null && onDistanceEnter == null && onDistanceExit == null)
                Debug.LogWarning("No events are assigned to Distance Trigger on " + gameObject.name);
        }

        if (useLookAtTrigger)
        {
            if (onLookAtFirstEnter == null && onLookAtFirstEnter == null && onLookAtDistanceExit == null)
                Debug.LogWarning("No events are assigned to LookAt Trigger on " + gameObject.name);
        }

        if (useEventsTriggers)
        {
            if (useStartTrigger && onStart == null)
                Debug.LogWarning("No events are assigned to Start Trigger on " + gameObject.name);
            if (useOnEnableTrigger && onEnable == null)
                Debug.LogWarning("No events are assigned to OnEnable Trigger on " + gameObject.name);
            if (useUpdateTrigger && onUpdate == null)
                Debug.LogWarning("No events are assigned to Update Trigger on " + gameObject.name);
            if (useOnDisableTrigger && onDisable == null)
                Debug.LogWarning("No events are assigned to OnDisable Trigger on " + gameObject.name);
        }
    }

    public static bool CheckRateLimiter(float frequency)
    {
        return Time.frameCount % frequency == 0;
    }

    #region TriggerEachEvents

    public virtual void TriggerOnTriggerFirstEnter()
    {
        onTriggerFirstEnter?.Invoke();
    }

    public virtual void TriggerOnTriggerEnter()
    {
        onTriggerEnter?.Invoke();
    }

    public virtual void TriggerOnTriggerExit()
    {
        onTriggerExit?.Invoke();
    }

    public virtual void TriggerOnDistanceFirstEnter()
    {
        onDistanceFirstEnter?.Invoke();
    }

    public virtual void TriggerOnDistanceEnter()
    {
        onDistanceEnter?.Invoke();
    }

    public virtual void TriggerOnDistanceExit()
    {
        onDistanceExit?.Invoke();
    }

    public virtual void TriggerOnLookAtFirstEnter()
    {
        onLookAtFirstEnter?.Invoke();
    }

    public virtual void TriggerOnLookAtEnter()
    {
        onLookAtEnter?.Invoke();
    }

    public virtual void TriggerOnLookAtExit()
    {
        onLookAtDistanceExit?.Invoke();
    }

    public virtual void TriggerOnStart()
    {
        onStart?.Invoke();
    }

    public virtual void TriggerOnEnable()
    {
        onEnable?.Invoke();
    }

    public virtual void TriggerOnDisable()
    {
        onDisable?.Invoke();
    }

    #endregion
}
