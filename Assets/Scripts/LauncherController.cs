using UnityEngine;

public class LauncherController : MonoBehaviour
{
    [SerializeField] private float _bulletPower;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPoint;

    private void OnFire(Input value)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject instantiate = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity);
            Rigidbody rb = instantiate.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * _bulletPower);
        }
    }
}
