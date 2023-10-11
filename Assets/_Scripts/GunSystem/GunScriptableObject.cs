using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(menuName = "Gun/Gun", fileName = "Gun")]
public class GunScriptableObject : ScriptableObject {
    public GunType GunType;
    public GameObject ModelPrefab;
    public GameObject VFXhitPrefab;
    public List<AudioClip> gunSoundClips;
    public AnimatorController animatorController;
    public Vector3 SpawnPosition;
    public Vector3 SpawnRotation;
    public int DamagePerHit;
    public int MaxAmmo;
    public float TimeBeetwenShoots;

    private GameObject _instance;
    private GunAudio _gunAudio;
    private Animator _animator;
    private float _lastTimeShoot;
    private int _currentAmmo;
    public int CurrentAmmo {
        get { return _currentAmmo; }
    }
    
    
    public void Spawn(Transform parent) {
        Reload();
        _instance = Instantiate(ModelPrefab, parent, true);
        _instance.transform.localPosition = SpawnPosition;
        _instance.transform.localEulerAngles = SpawnRotation;
        _animator = _instance.AddComponent<Animator>();
        _gunAudio = _instance.AddComponent<GunAudio>();
        _animator.runtimeAnimatorController = animatorController;
    }
    public void Fire(Ray ray) {
        if (!IsShootPossible()) return;
        if (!Physics.Raycast(ray, out RaycastHit hit)) return;
        _lastTimeShoot = Time.time + TimeBeetwenShoots;
        _currentAmmo--;
        Instantiate(VFXhitPrefab, hit.point + (hit.normal * .01f), Quaternion.FromToRotation(Vector3.up, hit.normal));
        _gunAudio.PlayRandomSound(gunSoundClips);
        _animator.Play("Shoot");
        EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
        if (enemyHealth == null) return;
        enemyHealth.GetDamage(DamagePerHit);
    }
    public void Reload() {
        _lastTimeShoot = 0;
        _currentAmmo = MaxAmmo;
    }
    private bool IsShootPossible() {
        if (_currentAmmo <= 0) return false;
        if (_lastTimeShoot > Time.time) return false;

        return true;
    }


}
