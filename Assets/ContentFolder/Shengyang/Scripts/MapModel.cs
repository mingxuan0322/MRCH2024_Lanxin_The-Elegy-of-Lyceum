using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapModel : MonoBehaviour
{
    void Start()
    {
        if (!Application.isEditor)
        {
            // Deactivate the GameObject this script is attached to
            gameObject.SetActive(false);
        }
    }

}
