using UnityEngine;
using UnityEngine.UI;
public class Reticle : MonoBehaviour
{
    private RectTransform _reticle;
    private float _currentSize;

    [Range(50f, 250f)] public float _size;
    public float _restingSize;
    public float _maxSize;
    public float _speed;
    public Rigidbody rb;

    public Image _left;
    public Image _right;
    public Image _bottom;
    public Image _top;
    
    
    public Color _normalColor =  Color.white;
    public Color _ennemyColor =  Color.red;

    public bool _isTargetEnemy;
    
    private void Start()
    {
        _reticle = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (_isTargetEnemy)
        {
            _left.color = _ennemyColor;
            _right.color = _ennemyColor;
            _bottom.color = _ennemyColor;
            _top.color = _ennemyColor;
        }
        else
        {
            _left.color = _normalColor;
            _right.color = _normalColor;
            _bottom.color = _normalColor;
            _top.color = _normalColor;
        }
        
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
