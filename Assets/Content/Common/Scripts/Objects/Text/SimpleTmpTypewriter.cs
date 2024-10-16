using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public abstract class SimpleTmpTypewriter : MonoBehaviour
{
    [SerializeField, ReadOnly] protected TextMeshProUGUI textUI;

    [SerializeField, ReadOnly] protected TextMeshPro text;

    [CanBeNull, AssetsOnly] protected AudioSource _typeAudioSource;

    [Title("Content to type", bold: false),
     InfoBox("Put the content you want to type in the text area, the content of the TMP component will be ignored",
         InfoMessageType.Warning)]
    [HideLabel]
    [MultiLineProperty(8), SerializeField]
    protected string contentToType;

    [SerializeField] protected float TypeSpeed = 0.1f;

    [Title("Setting")] [SerializeField] protected bool typeOnEnable = false;


    [SerializeField] protected bool onlyTypeForTheFirstTime = false;
    [SerializeField] protected bool saveCrossScene = false;

    [CanBeNull, SerializeField,
     InfoBox("If you need to play a sound when typing, you need to set the AudioSource up and audioclip here"), Space]
    protected AudioClip typeSound;

    protected bool _isPlayed = false;
    protected bool _isPlaying = false;

    protected virtual void Awake()
    {
        TryGetComponent<TextMeshProUGUI>(out textUI);

        TryGetComponent<TextMeshPro>(out text);

        if (textUI == null == (text == null)) //It means both are null or both are not null, I write it for fun lol
        {
            Debug.LogWarning("Check the tmp/tmpUI component on " + gameObject.name);
        }

        TryGetComponent(out _typeAudioSource); // Try to get the AudioSource component
    }

    protected virtual void OnEnable()
    {
        if (typeOnEnable)
        {
            if ((PlayerPrefs.GetInt($"TextTypedOn{gameObject.name}On{SceneManager.GetActiveScene()}") == 1 ||
                 _isPlayed) && onlyTypeForTheFirstTime)
            {
                FinishTyping();
            }
            else
            {
                StartCoroutine(TypeText(contentToType));
            }
        }
    }

    protected virtual void OnDisable()
    {
        _isPlaying = false;
    }

    public virtual void StartTyping()
    {
        StartCoroutine(TypeText(contentToType));
    }

    public virtual void FinishTyping()
    {
        if (text) text.text = contentToType;
        if (textUI) textUI.text = contentToType;
    }

    protected virtual IEnumerator TypeText(string textToType)
    {
        if (_isPlaying)
        {
            Debug.LogWarning("Text has been typed on " + gameObject.name);
            yield break;
        }

        _isPlaying = true;

        if (text) text.text = "";
        if (textUI) textUI.text = "";

        foreach (var c in textToType)
        {
            if (text) text.text += c;
            if (textUI) textUI.text += c;
            if (typeSound && _typeAudioSource)
                _typeAudioSource.PlayOneShot(typeSound);
            yield return new WaitForSeconds(TypeSpeed);
        }

        if (saveCrossScene)
            PlayerPrefs.SetInt($"TextTypedOn{gameObject.name}On{SceneManager.GetActiveScene()}", 1);
        else
            _isPlayed = true;

        _isPlaying = false;
    }
}