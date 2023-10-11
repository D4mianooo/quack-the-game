using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
    [SerializeField] private Animator _animator;
    private void OnCollisionEnter(Collision other) {
        if (!other.transform.CompareTag("Player")) return;
        Debug.Log(other.transform.name);
    }
}
