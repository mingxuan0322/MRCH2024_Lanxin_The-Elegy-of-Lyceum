using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

[AddComponentMenu("Interact/Keep Facing To Cam")]
public class KeepFacingToCam : MonoBehaviour
{
    private Camera _mainCam;
    
    private bool _faceToCam = false;
    
    [Title("Setting")]
    [SerializeField] private bool lockYAxis = false;
    [SerializeField] private bool faceToCamOnEnable = true;

    private void Start()
    {
        _mainCam = Camera.main;
        
        if (GetComponent(typeof(MoveAndRotate)) != null)
        {
            Debug.LogWarning($"{gameObject.name} has both 'TextFaceToCam' and 'Move and Rotate' component!");
        }

        _faceToCam = faceToCamOnEnable;
    }

    private void Update()
    {
        if (!_mainCam || !_faceToCam) return;
        
        var directionToCamera = _mainCam.transform.position - transform.position;
        if(lockYAxis)
            directionToCamera.y = 0; 
        transform.rotation = Quaternion.LookRotation(-directionToCamera);
    }

    public void SetFaceToCam(bool target)
    {
        _faceToCam = false;
    }
}