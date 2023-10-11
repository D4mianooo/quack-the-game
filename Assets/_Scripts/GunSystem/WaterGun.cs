using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class WaterGun : MonoBehaviour
{

    private PlayerInputActions _playerInputActions;
    private TrailRenderer _trailRenderer;
    private ParticleSystem _muzzleFlash;
    private AudioSource _audioSource;
    private PlayerStats _playerStats;
    private Ammo _ammo;
    
    public bool isShooting;
    
    
    private void Awake() {
        isShooting = false;
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Reload.performed += Reload;
        Cursor.lockState = CursorLockMode.Locked;
        
        _playerStats = GetComponentInParent<PlayerStats>();
        _trailRenderer = GetComponentInChildren<TrailRenderer>();
        _muzzleFlash = GetComponentInChildren<ParticleSystem>();
        _audioSource = GetComponentInChildren<AudioSource>();
        _ammo = GetComponent<Ammo>();
    }

    
    public void Shoot(){
        if(isShooting) return;
        StartCoroutine(StartShooting());
       
    }
    private IEnumerator StartShooting(){
        isShooting = true;
        while(_ammo.CurrentAmmo > 0 && _playerInputActions.Player.Shoot.IsPressed()){
            Ray mouseRay = GetRayFromMousePosition();
            if (Physics.Raycast(mouseRay, out RaycastHit hit, Mathf.Infinity)){
                _ammo.CurrentAmmo--;
                _muzzleFlash.Play();
                _audioSource.Play();
                RenderTrail(hit);
                EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
                if(enemyHealth != null){
                    enemyHealth.GetDamage(1);
                    _playerStats.AddExperience(15f);
                }
            }
            float timeBeetwenShoots = 1f / _playerStats._attackSpeed.GetValue();
            yield return new WaitForSeconds(timeBeetwenShoots);
        }
        StopCoroutine(StartShooting());
        isShooting = false;

    }
    private void Reload(InputAction.CallbackContext callbackContext){
        _ammo.ResetAmmo();
    }
    private Ray GetRayFromMousePosition(){
        Vector2 mousePos = _playerInputActions.Player.MousePosition.ReadValue<Vector2>();
        Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);
        return mouseRay;
    }

    private void RenderTrail(RaycastHit hit){
        TrailRenderer trail = Instantiate(_trailRenderer, _muzzleFlash.transform.position, Quaternion.identity);
        trail.enabled = true;
        StartCoroutine(MoveTrail(trail, trail.transform.position, hit.point));
    }

    private IEnumerator MoveTrail(TrailRenderer trail, Vector3 from, Vector3 to){
        float speed = 5f;
        float i = 0f;
        while(i <= 1f){
            i += Time.deltaTime * speed;
            trail.transform.position = Vector3.Lerp(from, to, i);
            yield return new WaitForEndOfFrame(); 
        }
    }


}
