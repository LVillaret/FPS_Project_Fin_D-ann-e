using UnityEngine;
using UnityEngine.UI;
public class Reticle : MonoBehaviour
{
    private RectTransform _reticle;
    private float _currentSize;
        
    [Range(50f, 250f)]
    public float _size;
    public float _restingSize;
    public float _maxSize;
    public float _speed;
    public Rigidbody rb;
    
    private void Start()
    {
        _reticle = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (rb.linearVelocity.sqrMagnitude != 0)
        {
            _currentSize = Mathf.Lerp(_currentSize, _maxSize, Time.deltaTime * _speed);
        }
        else
        {
            _currentSize = Mathf.Lerp(_currentSize, _restingSize, Time.deltaTime * _speed);
        }
        
        _reticle.sizeDelta = new Vector2(_currentSize, _currentSize);
    }

    bool isMoving()
    {
        if (Input.GetAxis("Horizontal") != 0 ||
            Input.GetAxis("Vertical") != 0 ||
            Input.GetAxis("Mouse X") != 0 ||
            Input.GetAxis("Mouse Y") != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
