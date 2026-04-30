
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Raycast : MonoBehaviour
{
    [SerializeField] private float _maxAmmo = 30f;
    [SerializeField] private float _currentAmmo = 30f;
    [SerializeField] private Transform _originTransform;
    [SerializeField] private Transform _cameraTransform;
    private float _reloadTime = 2f;
    private float _reloadTimer;
    private bool _autoShoot = false;
    private bool _reloading;
    private AudioSource _audioSource;
    

    public GameObject _fire;
    public GameObject _hitPoint;
    public AudioClip _fireSoundEffect;
    public AudioClip _reloadSoundEffect;
    public Text _currentAmmoText;


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        Shooting();
    }

    public void Shooting()
    {
        _currentAmmoText.text = _currentAmmo.ToString();
        // reload with R
        if (Input.GetKey(KeyCode.R) && !_reloading)
        {
            _reloading = true;
            _audioSource.PlayOneShot(_reloadSoundEffect);
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

        Debug.DrawRay(_cameraTransform.position, _cameraTransform.forward * 100, Color.yellow);
        if (Input.GetButtonDown("Fire1"))
        {
            _audioSource.PlayOneShot(_fireSoundEffect);
            Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);

            //Damage if enemy are hit
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                EnemyController enemy = hit.transform.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    enemy.TakeDamage(1);
                }
                    
            }

            // particles effects 
            GameObject a = Instantiate(_fire, _originTransform.position, _originTransform.rotation, _originTransform);
            GameObject b = Instantiate(_hitPoint, hit.point, Quaternion.LookRotation(hit.normal));

            // destroy particles effects after time
            Destroy(a, 0.2f);
            Destroy(b, 2f);


            if (Input.GetButtonDown("Fire1"))
            {
                _currentAmmo--;
            }
        }
    }
}

