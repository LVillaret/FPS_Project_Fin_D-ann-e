using System;
using UnityEngine;
using UnityEngine.AI;

public class BossControler : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject projectile;

    public float detectionRange = 10f;
    public float attackRange = 3f;
    public float timeBetweenAttacks = 2f;

    public int health = 50;
    
    public NavMeshAgent _agent;
    public event Action OnDeath;
    
    private bool _alreadyAttacked;
    
    private void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        

        if (distance < attackRange)
            Attack();
    }

    private void Attack()
    {
        _agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!_alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, _spawnPoint.position, Quaternion.identity)
                .GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 25f, ForceMode.Impulse);
            rb.AddForce(transform.up * 1.7f, ForceMode.Impulse);

            _alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack() => _alreadyAttacked = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Damage(1);
    }

    public void Damage(int damage)
    {
        health -= damage;

        if (health <= 0) ;
    }

    


}
