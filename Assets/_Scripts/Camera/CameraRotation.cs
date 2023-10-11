using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] float sensitivity = 5f;
    [SerializeField] float yAxisClamp = 90f;
    
    private Camera firstPersonCamera;
    private PlayerInputActions playerInputActions;
    
    float xRotation = 0f; 
    float yRotation = 0f; 
    
    
    private void Awake() {
        firstPersonCamera = GetComponent<Camera>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Camera.Enable();
    }


    void Update()
    {   
        Vector2 mousePosition = playerInputActions.Camera.Mouse.ReadValue<Vector2>();
        
        xRotation += mousePosition.x * sensitivity;
        yRotation -= mousePosition.y * sensitivity;
        yRotation = Mathf.Clamp(yRotation, -yAxisClamp, yAxisClamp);
        
        firstPersonCamera.transform.localEulerAngles = new Vector3(yRotation, 0f, 0f);
        transform.parent.transform.eulerAngles = new Vector3(0f, xRotation, 0f);
    }
}
