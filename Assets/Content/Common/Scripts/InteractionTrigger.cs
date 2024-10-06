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
/// it has an editor script so you cannot easily change this class to what you want easily.
/// You can read (https://docs.unity3d.com/ScriptReference/Editor.html) for more details.
/// This script was made before I imported the Odin Inspector, so it doesn't use the Odin Inspector.
/// </summary>
public class InteractionTrigger : MonoBehaviour
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

    //[SerializeField] private Transform lookAtTarget;
    [SerializeField] private float lookAtAngle = 25f;
    [SerializeField] private float lookAtDistance;

    [Space(5)] [SerializeField] private UnityEvent onLookAtFirstEnter;
    [SerializeField] private UnityEvent onLookAtEnter;

    [SerializeField, Tooltip("It will be triggered when: LookAt event is triggerred and exit the distance")]
    private UnityEvent onLookAtDistanceExit;

    private bool _firstLookAtEnter = true;
    private bool _alreadyLookAt;

    #endregion

    #region "unity events"

    //it doesn't refer to UnityEvent but functions like Start...
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

    //[Header("Interaction Settings")] [SerializeField] private bool triggerOnlyOnce;

    private GameObject _player;
    private Transform _playerTransform;

    private const int CheckRateFreq = 25;

    #endregion

    #endregion


    private void Start()
    {
        CheckAndInitSetting();

        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player == null)
            Debug.LogError("No player found in the scene");
        _playerTransform = _player.transform;

        if (useEventsTriggers && useStartTrigger)
            onStart?.Invoke();
    }

    private void OnEnable()
    {
        if (useEventsTriggers && useOnEnableTrigger)
            onEnable?.Invoke();
    }


    private void Update()
    {
        if (useDistanceTrigger)
        {
            if (!CheckRateLimiter(CheckRateFreq)) return; //reduce the number of calculations

            if (InDistance(distance) && !_alreadyInDistance)
            {
                // To trigger each event only once when the player enters the distance
                if (_firstDistanceEnter)
                {
                    onDistanceFirstEnter?.Invoke();
                    _firstDistanceEnter = false;
                }

                onDistanceEnter?.Invoke();

                _alreadyInDistance = true;
            }
            else if (!InDistance(distance) && _alreadyInDistance)
            {
                onDistanceExit?.Invoke();
                _alreadyInDistance = false;
            }

            if (useEventsTriggers && useUpdateTrigger)
            {
                onUpdate?.Invoke();
            }
        }

        if (useLookAtTrigger)
        {
            if (!CheckRateLimiter(CheckRateFreq)) return; //reduce the number of calculations
            //print("Angle: " + Vector3.Angle(_playerTransform.forward, (transform.position - _playerTransform.position).normalized));
            if (InDistance(lookAtDistance) && !_alreadyLookAt)
            {
                if (Vector3.Angle(_playerTransform.forward,
                        (transform.position - _playerTransform.position).normalized) <= lookAtAngle)
                {
                    if (_firstLookAtEnter)
                    {
                        onLookAtFirstEnter?.Invoke();
                        _firstLookAtEnter = false;
                    }

                    onLookAtEnter?.Invoke();

                    _alreadyLookAt = true;
                }
            }

            else if (!InDistance(lookAtDistance) && _alreadyLookAt)
            {
                onLookAtDistanceExit?.Invoke();
                _alreadyLookAt = false;
            }
        }
    }

    private void OnDisable()
    {
        if (useEventsTriggers && useOnDisableTrigger)
            onDisable?.Invoke();
    }

    private bool InDistance(float distance)
    {
        return Vector3.Distance(transform.position, _playerTransform.position) <= distance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!useColliderTrigger) return;
        if (!other.CompareTag("Player")) return;

        //In this way, only player can go on and two less indent

        if (_firstColliderEnter)
        {
            onTriggerFirstEnter?.Invoke();
            _firstColliderEnter = false;
        }

        onTriggerEnter?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!useColliderTrigger) return;
        if (!other.CompareTag("Player")) return;

        onTriggerExit?.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
    }

    private void CheckAndInitSetting()
    {
        if (useColliderTrigger)
        {
            _colliderTrigger = GetComponent<Collider>();
            if (_colliderTrigger == null)
            {
                Debug.LogError("Collider Trigger is enabled but no collider is attached to " + gameObject.name);
            }

            if (onTriggerFirstEnter == null && onTriggerEnter == null && onTriggerExit == null)
                Debug.LogWarning("No events are assigned to Collider Trigger on " + gameObject.name);
        }

        if (useDistanceTrigger)
        {
            if (distance == 0)
            {
                Debug.LogWarning("Distance Trigger is enabled but distance is set to 0 on " + gameObject.name);
            }

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

    /// <summary>
    /// Checks if the current frame count is a multiple of the given frequency.
    /// This can be used as a rate limiter in game loops, for example to perform an action every N frames.
    /// </summary>
    /// <param name="frequency">The frequency to check against. This is the number of frames between each "tick".</param>
    /// <returns>True if the current frame count is a multiple of the frequency, false otherwise.</returns>
    public static bool CheckRateLimiter(float frequency)
    {
        return Time.frameCount % frequency == 0;
    }

    #region TriggerEachEvents

    public void TriggerOnTriggerFirstEnter()
    {
        onTriggerFirstEnter?.Invoke();
    }

    public void TriggerOnTriggerEnter()
    {
        onTriggerEnter?.Invoke();
    }

    public void TriggerOnTriggerExit()
    {
        onTriggerExit?.Invoke();
    }

    public void TriggerOnDistanceFirstEnter()
    {
        onDistanceFirstEnter?.Invoke();
    }

    public void TriggerOnDistanceEnter()
    {
        onDistanceEnter?.Invoke();
    }

    public void TriggerOnDistanceExit()
    {
        onDistanceExit?.Invoke();
    }

    public void TriggerOnLookAtFirstEnter()
    {
        onLookAtFirstEnter?.Invoke();
    }

    public void TriggerOnLookAtEnter()
    {
        onLookAtEnter?.Invoke();
    }

    public void TriggerOnLookAtDistanceExit()
    {
        onLookAtDistanceExit?.Invoke();
    }

    public void TriggerOnStart()
    {
        onStart?.Invoke();
    }

    public void TriggerOnEnable()
    {
        onEnable?.Invoke();
    }

    public void TriggerOnUpdate()
    {
        onUpdate?.Invoke();
    }

    public void TriggerOnDisable()
    {
        onDisable?.Invoke();
    }

    #endregion
}