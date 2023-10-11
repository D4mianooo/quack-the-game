using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour {
    private EnemyHealth _enemyHealth;
    private Animator _animator;
    private void Awake() {
        _animator = GetComponent<Animator>();
        _enemyHealth = GetComponentInParent<EnemyHealth>();
        _enemyHealth.onDamageTaken += PlayHitAnimation;
    }
    private void PlayHitAnimation(object sender, EventArgs eventArgs) {
        if (_enemyHealth.HitPoints <= 0) {
            _animator.Play("Die");
            return;   
        }
        _animator.Play("GetHit");
    }
}
