using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    [SerializeField] private float _openAngle = -90f;
    [SerializeField] private float _openSpeed = 2f;
    
    private bool _isOpen = false;
    private Quaternion _closedRotation;
    private Quaternion _openRotation;
    private Coroutine _currentCoroutine;
    
    private void Start()
    {
        _closedRotation = transform.rotation;
        _openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, _openAngle, 0));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        { 
            if(_currentCoroutine != null) StopCoroutine(_currentCoroutine);
            _currentCoroutine = StartCoroutine(ToggleDoor());
        }
        
    }

    private IEnumerator ToggleDoor()
    {
        Quaternion targetRotation = _isOpen ? _closedRotation : _openRotation;
        _isOpen = !_isOpen;

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _openSpeed);
            yield return null;
        }
        
        transform.rotation = targetRotation;
    }
}

