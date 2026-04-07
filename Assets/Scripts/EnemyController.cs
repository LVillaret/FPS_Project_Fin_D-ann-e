using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _currenteHealth;
    [SerializeField] private Transform _player;
    [SerializeField] private float _followStartRadius = 3f;
    [SerializeField] private float _followEndRadius = 5f;
    [SerializeField] private float _rotationSpeed = 3f;
    [SerializeField] private float _damage = 20f;
    private float _despawnTime;
    
    private bool _isFollowing = false;
    private bool _dead;
    private bool _hit;
    
    private Vector3 _originalPosition;
    private Vector3 _direction;
    
    private Animator _animator;
    private NavMeshAgent _agent;
    

    private void Start()
    {
        _originalPosition = transform.position;
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _agent.SetDestination(_player.position);
        float z = _agent.velocity.z;
        float x = -_agent.velocity.x;
        _animator.SetFloat("Horizontal", z);
        _animator.SetFloat("Vertical", x);

        if (_dead)
        {
            _despawnTime += Time.deltaTime;
            if (_despawnTime >= 3f)
            {
                Destroy(gameObject);
            }
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

    public void GiveDamage(int damage)
    {
        
    }
}
