using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerFPS : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private Transform _cameraTransform;

    [Header("Settings")] 
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _xMaxAngle = 30;
    [SerializeField] private float _xMinAngle = -45;
    [SerializeField] private float _rotationSpeed = 1400;
    [SerializeField] private  AudioClip _WalkSoundEffect;
    
    [Header("Health")]
    [SerializeField] private float _maxHealth = 100;
    public float _currentHealth = 100;
    private float _durationTimer;
    public Image overlay;
    public float overlayDuration;
    public float fadeSpeed;

    private Animator _animator;
    
    private Vector3 _move;
    private Vector3 _bodyRotation;
    

    private float _yLook;
    private float _xLook;

    private float _mouseX;
    private float _mouseY;
    private float _cameraPitch;
    
    private float _horizontal;
    private float _vertical;
    
    private AudioSource _audioSource;
    

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _currentHealth  = _maxHealth;
        //HealthUI
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0f);
        _audioSource = GetComponent<AudioSource>();
        
        
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.D))   
        {
            _audioSource.PlayOneShot(_WalkSoundEffect);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _audioSource.PlayOneShot(_WalkSoundEffect);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _audioSource.PlayOneShot(_WalkSoundEffect);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _audioSource.PlayOneShot(_WalkSoundEffect);
        }
        TakeDamage();
        // Input
        //Mouse
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");

        //Keyboard
        _vertical = Input.GetAxis("Vertical");
        _horizontal = Input.GetAxis("Horizontal");

        // Animation
        _animator.SetFloat("Vertical", _vertical);
        _animator.SetFloat("Horizontal", _horizontal);
        
        
        _bodyRotation = new Vector3(0, _mouseX, 0) * (_rotationSpeed * Time.fixedDeltaTime);
        transform.Rotate(_bodyRotation);
        
        //  Camera Rotation
        _cameraPitch -= _mouseY * _rotationSpeed * Time.fixedDeltaTime;
        _cameraPitch = Mathf.Clamp(_cameraPitch, _xMinAngle, _xMaxAngle);
        
        //_cameraTransform.Rotate(_cameraTransform.localEulerAngles - new Vector3(0, _cameraPitch, 0f));
        _cameraTransform.localRotation = Quaternion.Euler(_cameraPitch, 0f, 0f);
        
        // healthUI
        if (_currentHealth < 100)
        {
            if (overlay.color.a >= 0f)
            {
                _durationTimer += Time.deltaTime;
                if (_durationTimer >= overlayDuration)
                {
                    float tempAlpha = overlay.color.a;
                    tempAlpha -= Time.deltaTime * fadeSpeed;
                    overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
                }
            }
        }
       
    }

    private void FixedUpdate()
    {
        // Movement
        transform.Translate(_horizontal * Time.fixedDeltaTime * _speed, 0, _vertical * Time.fixedDeltaTime * _speed);
        
    }

    private float ClampAngle(float angle, float from, float to)
    {
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }

    private void TakeDamage()
    {
        //HealthUI
        _durationTimer = 0f;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
        
    }
    
}
