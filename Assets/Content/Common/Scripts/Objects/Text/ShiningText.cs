using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShiningText : MonoBehaviour
{
    [SerializeField, ReadOnly]
    private TextMeshProUGUI textUI;
    [SerializeField, ReadOnly]
    private TextMeshPro text;
    public float cycleTime = 2.0f;

    private float _elapsedTime = 0.0f; // 记录经过的时间
    
    void Awake()
    {
        TryGetComponent(out textUI);
        TryGetComponent(out text);
    }

    void Update()
    {
        if(!textUI && !text) return;
        if (textUI || text)
        {
            _elapsedTime += Time.deltaTime;
            
            float cycleProgress = _elapsedTime % cycleTime / cycleTime;
            
            float alpha = Mathf.Abs(Mathf.Cos(cycleProgress * Mathf.PI));

            if(textUI)
            {
                Color color = textUI.color;
                color.a = alpha;
                textUI.color = color;
            }

            else if (text)
            {
                Color color = text.color;
                color.a = alpha;
                text.color = color;
            }
        }
    }
}
