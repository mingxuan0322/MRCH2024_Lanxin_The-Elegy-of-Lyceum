using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[AddComponentMenu("Interact/Object Toolset")]
public class ObjectToolset : MonoBehaviour
{
    [InfoBox("Includes ToggleComponentEnabled for components and ToggleObjectEnabled for objects")]
    public void ToggleComponentEnabled(Component component)
    {
        if (component is Behaviour behaviourComponent)
        {
            behaviourComponent.enabled = !behaviourComponent.enabled;
        }
        else
        {
            Debug.LogWarning("The provided component doesn't have an 'enabled' property.");
        }
    }

    public void ToggleObjectEnabled(GameObject gameObject)
    {
        if (gameObject != null)
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
