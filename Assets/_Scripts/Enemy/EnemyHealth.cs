using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour {
    [SerializeField] private int _hitPoints = 5;
    public int HitPoints
    {
        get { return _hitPoints; }
    }

    public event EventHandler onDamageTaken; 

    public void GetDamage(int damage) {
        onDamageTaken?.Invoke(this, EventArgs.Empty);
        _hitPoints -= damage;
        if(_hitPoints > 0) return;
        // gameObject.SetActive(false);
    }
}
