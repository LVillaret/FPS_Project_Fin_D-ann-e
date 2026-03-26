using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
   [SerializeField] private float _currenteHealth;
   [SerializeField] private Transform _player;
   [SerializeField] private float _speed = 1f;
   [SerializeField] private float _followStartRadius = 3f;
   [SerializeField] private float _followEndRadius = 5f;
   [SerializeField] private float _rotationSpeed = 3f;

   private Vector3 _originalPosition;
   private bool _isFollowing = false;
   private NavMeshAgent _agent;
   
   public Animator _animator;
   
    private void start()
    {
        _agent =  GetComponent<NavMeshAgent>();
        if(_animator == null) _animator = GetComponent<Animator>();
        
        _originalPosition = transform.position;
    }
   // private void Awake()
   // {
   //    _animator = GetComponent<Animator>(); 
   // }

   private void Update()
   {
       if (_player != null)
       {
           float distance = Vector3.Distance(transform.position, _player.position);

           if (distance <= _followStartRadius)
           {
               _isFollowing = true;
           }

           if (distance > _followEndRadius && _isFollowing)
           {
               _isFollowing = false;
           }

           if (_isFollowing)
           {
               Vector3 direction = (_player.position - transform.position).normalized;
               transform.position += direction * _speed * Time.deltaTime;
               Quaternion lookRotation = Quaternion.LookRotation(direction);
               transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation, Time.deltaTime * _rotationSpeed);
           }
           else
           {
               transform.position = Vector3.MoveTowards(transform.position, _originalPosition, _speed * Time.deltaTime);
               if (Vector3.Distance(transform.position, _originalPosition) < 0.1f)
               {
                   transform.position = _originalPosition;
               }
           }

       }
       // float vertical = Input.GetAxis("Vertical");
       // float horizontal = Input.GetAxis("Horizontal");
       //
       // _animator.SetFloat("Vertical", vertical); 
       // _animator.SetFloat("Horizontal", horizontal);
       
   }
   public void TakeDamage(int damage) 
   {
       _currenteHealth -= damage;
       if (_currenteHealth <= 0)
       {
           Destroy(gameObject);
       }
   }
}
