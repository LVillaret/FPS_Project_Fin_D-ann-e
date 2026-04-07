using System;
using UnityEngine;

public class PlayerControllerFPS : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private Transform _cameraTransform;

    [Header("Settings")] [SerializeField] private float _speed = 3;
    [SerializeField] private float _xMaxAngle = 30;
    [SerializeField] private float _xMinAngle = -45;
    [SerializeField] private float _rotationSpeed = 2000;
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _currentHealth = 100;

    private Animator _animator;
    private Vector3 _move;
    private float _cameraPitch;

    private float _yLook;
    private float _xLook;

    private float _mouseX;
    private float _mouseY;

    private Vector3 _bodyRotation;

    private float _horizontal;
    private float _vertical;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
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
    }

    private void FixedUpdate()
    {
        // Movement
        transform.Translate(_horizontal * Time.deltaTime * _speed, 0, _vertical * Time.deltaTime * _speed);

        _bodyRotation = new Vector3(0, _mouseX, 0) * (_rotationSpeed * Time.deltaTime);
        transform.Rotate(_bodyRotation);

        //  Camera Rotation
        _cameraPitch -= _mouseY * _rotationSpeed * Time.deltaTime;
        _cameraPitch = Mathf.Clamp(_cameraPitch, _xMinAngle, _xMaxAngle);
        _cameraTransform.localRotation = Quaternion.Euler(_cameraPitch, 0f, 0f);
    }

    private float ClampAngle(float angle, float from, float to)
    {
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }
   
}
