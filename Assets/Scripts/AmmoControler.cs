using UnityEngine;
using UnityEngine.AI;

public class AmmoControler : MonoBehaviour
{
    [SerializeField] private float _damage = 40f;
    [SerializeField] private float _speed = 5f;
    
    private Vector3 _direction;
    private Transform _player;
    private float _timer;


    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Players").transform;
        _direction = new Vector3(_player.position.x, _player.position.y, _player.position.z);
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
        _timer += Time.deltaTime;
        if (_timer > 3f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerControllerFPS player = other.GetComponent<PlayerControllerFPS>();
        if (player != null) 
        {
            player._currentHealth =  player._currentHealth -= _damage;
            Destroy(gameObject);
        }
    }
}
