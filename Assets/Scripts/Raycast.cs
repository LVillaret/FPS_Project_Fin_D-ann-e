
using System;
using Unity.VisualScripting;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    [SerializeField] private float _maxAmmo = 30f;
    [SerializeField] private float _currentAmmo = 30f;
    [SerializeField] private Transform _originTransform;
    private float _reloadTime = 2f;
    private float _reloadTimer;
    private bool _reloading;

    public GameObject _fire;
    public GameObject _hitPoint;

    private void Update()
    {
        Shooting();
    }

    public void Shooting()
    {
        // reload with R
        if (Input.GetKey(KeyCode.R) && !_reloading)
        {
            _reloading = true;
        }

        if (_reloading)
        {
            _reloadTimer += Time.deltaTime;
            if (_reloadTimer >= _reloadTime)
            {
                _reloading = false;
                _reloadTimer = 0f;
                _currentAmmo = _maxAmmo;
            }
        }

        // stop shoot if ammo = 0
        else if (_currentAmmo == 0) return;

        // create raycast
        Debug.DrawRay(_originTransform.position, _originTransform.forward * 100, Color.yellow);
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = new Ray(_originTransform.position, _originTransform.forward);

            //Damage if enemy are hit
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                
                EnemyController enemy = hit.transform.GetComponent<EnemyController>();
                if (enemy != null) enemy.TakeDamage(1);
            }

            // particles effects 
            GameObject a = Instantiate(_fire, _originTransform.position, _originTransform.rotation, _originTransform);
            GameObject b = Instantiate(_hitPoint, hit.point, Quaternion.identity);
            //hit.normal
            
            // destroy particles effects after time
            Destroy(a, 0.2f);
            Destroy(b, 2f);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            _currentAmmo--;
        }
    }
}



