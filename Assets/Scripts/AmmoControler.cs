using UnityEngine;
using UnityEngine.AI;

public class AmmoControler : MonoBehaviour
{
    [SerializeField] private float _damage = 35f;
    [SerializeField] private float _lifeTime = 5f;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Players"))
        {
            PlayerControllerFPS _player = other.GetComponent<PlayerControllerFPS>();

            if (_player != null)
            {
                _player._currentHealth -= _damage;
                
                Debug.Log(_player._currentHealth);
            }
            
            Destroy(gameObject);
        }
    }
}
