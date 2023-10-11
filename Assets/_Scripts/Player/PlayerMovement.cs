using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Vector3 jumpOffset;
    
    private Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;
    
    private Vector3 movementDirection;
    
    
    private void Awake() {
        rigidbody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
 
        
    }
    private void Update() {
        Vector2 direction = playerInputActions.Player.Move.ReadValue<Vector2>();
        movementDirection = transform.forward.normalized * direction.y + transform.right.normalized * direction.x;
        
        transform.position += movementDirection * Time.deltaTime * speed;
    }
    public void Jump(InputAction.CallbackContext callbackContext){
        if(!IsGrounded()) return;
        Vector3 jumpDirection = Vector3.up + movementDirection + jumpOffset;
        rigidbody.AddForce(jumpDirection.normalized * jumpForce, ForceMode.Impulse);
        
    }
    private bool IsGrounded(){
        if(Physics.Raycast(capsuleCollider.bounds.center, Vector3.down,  capsuleCollider.bounds.extents.y + .1f)){
            return true;
        };
        return false;
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + movementDirection);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.up + movementDirection + jumpOffset).normalized);
    }
}
