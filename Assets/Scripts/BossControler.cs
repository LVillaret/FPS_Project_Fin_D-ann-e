using System;
using UnityEngine;
using UnityEngine.AI;

public class BossControler : MonoBehaviour
{
    [Header("Stats")] 
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth = 50f;

    [Header("Shooting")]
    [SerializeField] private GameObject _ammoPrefab;
    [SerializeField] Transform _firePoint;
    [SerializeField] private float _fireRate = 1f;
    [SerializeField] private float _projectileSpeed = 20f;

    private float _fireTimer;
    
    [Header("Activation")]
    public bool _isActive = true;
    
    public Transform _player;
    public PlayerControllerFPS _playerControllerFPS;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void Update()
    {
        if (!_isActive)
        {
            CheckEnemiesCount();
            return;
        }
        
        if (_player == null)
        {
            Debug.Log("player");
            return;
        }


        PlayerDamage();
        HandleShooting();
    }

    private void CheckEnemiesCount()
    {
        if (EnemyController.EnemiesNumber <= 0)
        {
            _isActive = true;
        }
    }
    private void HandleShooting()
    {
        _fireTimer += Time.deltaTime;

        if (_fireTimer >= _fireRate)
        {
            Shoot();
            _fireTimer = 0f;
        }
    }

    private void Shoot()
    {
        GameObject _ammo = Instantiate(_ammoPrefab, _firePoint.position, Quaternion.identity);
        
        Vector3 _direction = (_player.position - _firePoint.position).normalized;
        
        Rigidbody _rb = _ammo.GetComponent<Rigidbody>();
        _rb.linearVelocity = _direction * _projectileSpeed;
        
    }

    private void PlayerDamage()
    {
        if (_playerControllerFPS._currentHealth <= 0)
        {
            _player.GetComponent<PlayerControllerFPS>().Die();
        }
    }
    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
