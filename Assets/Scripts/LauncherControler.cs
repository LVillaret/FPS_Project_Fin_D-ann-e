using UnityEngine;
using UnityEngine.InputSystem;

public class LauncherControler : MonoBehaviour
{
    [Header("Stats")] 
    [SerializeField] private float _ammoPower = 200f;
    [SerializeField] private float _startShotsCooldown;
    [SerializeField] private float _shotCooldown;
    
    [Header("References")]
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _ammoPrefab; 
    [SerializeField] private Transform _spawnPoint;
    
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Players").transform;
    }

    private void Update()
    {
        if (_shotCooldown <= 0)
        {
            _shotCooldown = _startShotsCooldown;
        }
        else
        {
            _shotCooldown -= Time.deltaTime;
        }
    }
}
