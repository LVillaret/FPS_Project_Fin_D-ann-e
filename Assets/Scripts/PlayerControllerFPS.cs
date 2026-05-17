using System;
using System.Collections;
using Unity.VisualScripting;
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
    [SerializeField] public float _rotationSpeed = 1400;
    [SerializeField] private  AudioClip _WalkSoundEffect;
    
    [Header("Health")]
    [SerializeField] private float _maxHealth = 100;
    public float _currentHealth = 100;
    public Image _healthOverlay;
    [SerializeField] private AnimationCurve _healthCurve;
    [SerializeField] private float _flashTime;
    
    [Header("Death")]
    public GameObject _deathPanel;
    
    [SerializeField] private Raycast _rc;
    [SerializeField] private PlayerControllerFPS _player;
    
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
    
    private bool _isPaused;
    
    private AudioSource _audioSource;

    private Coroutine _overlayCoroutine;

    private Color _originalColor;
    
    public DeathPanelManager _deathPanelManager;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _originalColor = _healthOverlay.color;
        _currentHealth  = _maxHealth;
        _deathPanel.SetActive(false);
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
        // Input
        

        //Keyboard
        _vertical = Input.GetAxis("Vertical");
        _horizontal = Input.GetAxis("Horizontal");

        
        //Mouse
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");
        //  Camera Rotation
        
        _bodyRotation = new Vector3(0, _mouseX, 0) * (_rotationSpeed * Time.fixedDeltaTime);
        transform.Rotate(_bodyRotation);
        
        //Camera Pitch
        _cameraPitch -= _mouseY * _rotationSpeed * Time.fixedDeltaTime;
        _cameraPitch = Mathf.Clamp(_cameraPitch, _xMinAngle, _xMaxAngle);
        
        //_cameraTransform.Rotate(_cameraTransform.localEulerAngles - new Vector3(0, _cameraPitch, 0f));
        _cameraTransform.localRotation = Quaternion.Euler(_cameraPitch, 0f, 0f);

        // Animation
        _animator.SetFloat("Vertical", _vertical);
        _animator.SetFloat("Horizontal", _horizontal);
    }

    private void FixedUpdate()
    {
        // Movement
        Vector3 move = new Vector3(_horizontal, 0f, _vertical);
        move = move.normalized;
        transform.Translate(move*_speed*Time.fixedDeltaTime);
    }

    private float ClampAngle(float angle, float from, float to)
    {
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }

    public void TakeDamage()
    {
        if (_overlayCoroutine != null)
        {
            StopCoroutine(_overlayCoroutine);
        }

        _overlayCoroutine = StartCoroutine(DamageOverlayCoroutine());
        
    }

    private IEnumerator DamageOverlayCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        float time = 0;
        while (time < _flashTime)
        {
            _healthOverlay.color = new Color(_healthOverlay.color.r, _healthOverlay.color.g, _healthOverlay.color.b, _healthCurve.Evaluate(time / _flashTime * 2));
            time += Time.fixedDeltaTime;
            yield return new WaitForNextFrameUnit();
        }
        
        _healthOverlay.color = new Color (_healthOverlay.color.r, _healthOverlay.color.g, _healthOverlay.color.b, 0f);
    }

    public void Die()
    {
        _deathPanelManager.ShowDeathPanel();
    }
}
