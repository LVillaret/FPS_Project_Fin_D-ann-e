using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private PlayerControllerFPS _player;
    [SerializeField] private float _rotationSpeed = 3f;
    [SerializeField] public float _enemyCurrentHealth = 8f;
    [SerializeField] private float _enemyMaxHealth = 8f;
    
    [Header("Attack Parameters")]
    [SerializeField] private float _attackCD = 3f;
    [SerializeField] private float _attackRange = 1f;
    [SerializeField] private float _damage = 20f;
    
    [SerializeField] private float _stopMoveAfterHit;
    
    private float _despawnTime;
    private float _timePassed;
    
    private bool _isFollowing = false;
    private bool _dead;
    private bool _hit;
    private bool _attack;
    
    private Vector3 _originalPosition;
    private Vector3 _direction;
    private float _lastHitTime;
    
    private Animator _animator;
    private NavMeshAgent _agent;
    
    
    private void Start()
    {
        _originalPosition = transform.position;
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _lastHitTime = _stopMoveAfterHit;
    }

    private void Update()
    {
        _agent.SetDestination(_player.transform.position);
        float z = _agent.velocity.z;
        float x = -_agent.velocity.x;
        _animator.SetFloat("Horizontal", z);
        _animator.SetFloat("Vertical", x);

        // despawn enemy when is dead
        if (_dead)
        {
            DespawnEnemy();
        }

        // Stop agent when taking damage
        if (_lastHitTime < _stopMoveAfterHit)
        {
            _lastHitTime += Time.deltaTime;
            _agent.isStopped = true;
        }
        else
        {
            _agent.isStopped = false;
        }
        
        // Enemy attack
        if (_timePassed >= _attackCD)
        {
            Attack();
        }
        _timePassed += Time.deltaTime;
        
    }

    void DespawnEnemy()
    {
        _despawnTime += Time.deltaTime;
        if (_despawnTime >= 3f)
        {
            Destroy(gameObject);
        }
    }

    void Attack()
    {
        if (Vector3.Distance(_player.transform.position, transform.position) <= _attackRange)
        {
            _animator.SetTrigger("Attack");
            _timePassed = 0;
            _attack =  true;

            _player._currentHealth = _player._currentHealth - _damage;
            Debug.Log(_player._currentHealth);
                
        }
    }

    public void TakeDamage(int damage)
    {
        _enemyCurrentHealth -= damage;
        if (_enemyCurrentHealth <= 0)
        {
            _animator.SetTrigger("Die");
            _agent.isStopped = true;
            GetComponent<CapsuleCollider>().enabled = false;
            _dead = true;
            
        }
        else if(!_dead)
        {
            _animator.SetTrigger("Hit" );
            _lastHitTime = 0f;
        }
    }
}
