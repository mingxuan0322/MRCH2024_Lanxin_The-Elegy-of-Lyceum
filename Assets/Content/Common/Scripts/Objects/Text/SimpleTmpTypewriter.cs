using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

[AddComponentMenu("Interact/Text/Simple Tmp Typewriter")]
public class SimpleTmpTypewriter : MonoBehaviour
{
    [SerializeField,ReadOnly]
    private TextMeshProUGUI textUI;

    [SerializeField, ReadOnly] 
    private TextMeshPro text;
    
    [CanBeNull,AssetsOnly] 
    private AudioSource _typeAudioSource;
    
    [Title("Content to type", bold: false),InfoBox("Put the content you want to type in the text area, the content of the TMP component will be ignored",InfoMessageType.Warning)]
    [HideLabel]
    [MultiLineProperty(8),SerializeField]
    private string contentToType;
    [SerializeField] private float TypeSpeed = 0.1f;

    [Title("Setting")]
    [SerializeField] private bool typeOnEnable = false;

    
    [SerializeField] private bool onlyTypeForTheFirstTime = false;
    [SerializeField] private bool saveCrossScene = false;
    [CanBeNull,SerializeField,InfoBox("If you need to play a sound when typing, you need to set the AudioSource up and audioclip here"),Space] 
    private AudioClip typeSound;
    
    private bool _isPlayed = false;
    private bool _isPlaying = false;

    private void Awake()
    {
        
        TryGetComponent<TextMeshProUGUI>(out textUI);
        
        TryGetComponent<TextMeshPro>(out text);
        
        if (textUI == null == (text == null)) //It means both are null or both are not null, I write it for fun lol
        {
            Debug.LogWarning("Check the tmp/tmpUI component on " + gameObject.name);
        }
        
        TryGetComponent(out _typeAudioSource); // Try to get the AudioSource component
        
    }

    private void OnEnable()
    {
        if (typeOnEnable)
        {
            if ((PlayerPrefs.GetInt($"TextTypedOn{gameObject.name}On{SceneManager.GetActiveScene()}") == 1||_isPlayed) && onlyTypeForTheFirstTime)
            {
                FinishTyping();
            }
            else
            {
                StartCoroutine(TypeText(contentToType));
            }
        }
    }

    private void OnDisable()
    {
        _isPlaying = false;
    }

    public void StartTyping()
    {
        StartCoroutine(TypeText(contentToType));
    }

    public void FinishTyping()
    {
        if (text) text.text = contentToType;
        if (textUI) textUI.text = contentToType;
    }
    
    private IEnumerator TypeText(string textToType)
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
            if(text) text.text += c;
            if(textUI) textUI.text += c;
            if(typeSound && _typeAudioSource)
                _typeAudioSource.PlayOneShot(typeSound);
            yield return new WaitForSeconds(TypeSpeed);
        }
        if(saveCrossScene)
            PlayerPrefs.SetInt($"TextTypedOn{gameObject.name}On{SceneManager.GetActiveScene()}", 1);
        else
            _isPlayed = true;
        
        _isPlaying = false;
    }
}
