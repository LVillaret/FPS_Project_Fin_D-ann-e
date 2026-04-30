using UnityEngine;
using UnityEngine.InputSystem;

public class LauncherControler : MonoBehaviour
{
    [SerializeField] private float ammoPower = 1250f;
    [SerializeField] private GameObject _ammoPrefab;
    [SerializeField] private Transform _spawnPoint;
 
    private void OnFire(InputValue value)
    {
        GameObject instantiate = Instantiate(_ammoPrefab, _spawnPoint.position, Quaternion.identity);
        Rigidbody rb = instantiate.GetComponent<Rigidbody>();
        rb.AddForce(_spawnPoint.forward * ammoPower);
    }
}
