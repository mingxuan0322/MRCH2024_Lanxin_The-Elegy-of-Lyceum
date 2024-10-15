using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapModel : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_EDITOR
        gameObject.hideFlags = HideFlags.DontSaveInBuild;
        // This may prevent the reference model from being saved into the build.
#endif
    }
}