using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _rotationSpeed = 3f;
    [SerializeField] private float _currenteHealth = 8f;
    [SerializeField] private float _maxHealth = 8f;
    
    [Header("Attack Parameters")]
    [SerializeField] private float _attackCD = 3f;
    [SerializeField] private float _attackRange = 1f;
    [SerializeField] private float _damage = 20f;
    
    private float _despawnTime;
    private float _timePassed;
    
    private bool _isFollowing = false;
    private bool _dead;
    private bool _hit;
    
    private Vector3 _originalPosition;
    private Vector3 _direction;
    
    private Animator _animator;
    private NavMeshAgent _agent;

    public bool _canAttack = true;
    
    
    private void Start()
    {
        _originalPosition = transform.position;
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Players").transform;
    }

    private void Update()
    {
        _agent.SetDestination(_player.position);
        float z = _agent.velocity.z;
        float x = -_agent.velocity.x;
        _animator.SetFloat("Horizontal", z);
        _animator.SetFloat("Vertical", x);

        // despawn enemy when is dead
        if (_dead)
        {
            _despawnTime += Time.deltaTime;
            if (_despawnTime >= 3f)
            {
                Destroy(gameObject);
            }
        }
        
        
        if (_timePassed >= _attackCD)
        {
            if (Vector3.Distance(_player.position, transform.position) <= _attackRange)
            {
                _animator.SetTrigger("Attack");
                _timePassed = 0;
            }
        }
        _timePassed += Time.deltaTime;

        if (_canAttack)
        {
            AttackPlayer();
        }
        
        
    }

    public void TakeDamage(int damage)
    {
        _currenteHealth -= damage;
        if (_currenteHealth <= 0)
        {
            _animator.SetTrigger("Die");
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            _dead = true;
            
        }
        else 
        {
            _animator.SetTrigger("Hit");
        }
    }

    private void AttackPlayer()
    {
        StartCoroutine(AttackTime());
    }

    IEnumerator AttackTime()
    {
        _canAttack = false;
        yield return new WaitForSeconds(0.5f);
        PlayerControllerFPS.singleton.PlayerDamage(_damage);
        yield return new WaitForSeconds(_attackRange);
        _canAttack = true;
    }
    
    
}
