using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunSystem : MonoBehaviour {
    [SerializeField] private GunScriptableObject _gunScriptableObject;
    [SerializeField] private GameHUD _gameHUD;

    private PlayerInputActions _playerInputActions;
    private Camera _camera;
    private AudioSource _audioSource;
    private void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
        _gameHUD = FindObjectOfType<GameHUD>();
        _camera = GetComponentInParent<Camera>();
        _audioSource = GetComponent<AudioSource>();
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();

        _playerInputActions.Player.Reload.performed += (InputAction.CallbackContext callbackContext) => {
            _gunScriptableObject.Reload();
            _gameHUD.UpdateUIClips(_gunScriptableObject.CurrentAmmo, _gunScriptableObject.MaxAmmo);
        };
    }

    private void Update() {
        if (_playerInputActions.Player.Shoot.IsPressed()) {
            Ray ray = _camera.ScreenPointToRay(_playerInputActions.Player.MousePosition.ReadValue<Vector2>());
            _gunScriptableObject.Fire(ray);
            _gameHUD.UpdateUIClips(_gunScriptableObject.CurrentAmmo, _gunScriptableObject.MaxAmmo);
        }
    }
    private void Start() {
        _gunScriptableObject.Spawn(transform);
        _gameHUD.UpdateUIClips(_gunScriptableObject.CurrentAmmo, _gunScriptableObject.MaxAmmo);
    }
}
