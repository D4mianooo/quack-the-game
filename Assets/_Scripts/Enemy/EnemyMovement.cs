using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyHealth))]
public class EnemyMovement : MonoBehaviour {
    [SerializeField] private float _range;
    private EnemyHealth _enemyHealth;
    private Transform _player;
    private NavMeshAgent _navMeshAgent;
    private Rigidbody _rigidbody;

    private void Awake() {
        _player = FindObjectOfType<PlayerMovement>().transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, _player.position);
        if(distance <= _range){
            _navMeshAgent.destination = _player.position;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
