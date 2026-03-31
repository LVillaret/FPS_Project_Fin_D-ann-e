using UnityEngine;

public class PlayerControllerFPS : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _cameraTransform;
    
    
    [Header("Settings")]
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _xMaxAngle = 30;
    [SerializeField] private float _xMinAngle = -45;
    [SerializeField] private float _rotationSpeed = 2000;
        
    private Animator _animator;
    private Vector3 _move;
    private float _cameraPitch;
    
    private float _yLook;
    private float _xLook;
        
    private void Awake(){
        _animator = GetComponent<Animator>();
    }
    
    private void Update(){ 
        
        // Input
        //Mouse
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        
        Vector3 bodyRotation = new Vector3(0 , mouseX, 0) * (_rotationSpeed * Time.deltaTime);
        
        transform.Rotate(bodyRotation);
        
        //Keyboard
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
            
        // Animation
        _animator.SetFloat("Vertical", vertical);
        _animator.SetFloat("Horizontal", horizontal);
            
        // Movement
        transform.Translate(horizontal * Time.deltaTime * _speed, 0, vertical * Time.deltaTime * _speed);
            
        //  Camera Rotation
        
        _cameraPitch -= mouseY * _rotationSpeed * Time.deltaTime;
        _cameraPitch = Mathf.Clamp(_cameraPitch, _xMinAngle, _xMaxAngle);
        _cameraTransform.localRotation = Quaternion.Euler(_cameraPitch, 0f, 0f);
        
    }

    private float ClampAngle(float angle, float from, float to){
    
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360+from);
        return Mathf.Min(angle, to);
    }
}
