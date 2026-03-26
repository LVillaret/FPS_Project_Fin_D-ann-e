
using System;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    [SerializeField] private Transform _originTransform;
    public GameObject _fire;
    public GameObject _hitPoint;

    private void Update()
    {
        Shooting();
    }

    public void Shooting()
    {
        Debug.DrawRay(_originTransform.position, _originTransform.forward * 100, Color.yellow);
        if (Input.GetButtonDown("Fire1")) {
            Ray ray = new Ray(_originTransform.position, _originTransform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit)){
                TargetController target = hit.transform.GetComponent<TargetController>();
                if (target != null) target.TakeDamage(1);
            }
            GameObject a = Instantiate(_fire, _originTransform.position, Quaternion.identity);
            GameObject b = Instantiate(_hitPoint, hit.point, Quaternion.identity);

            Destroy(a, 0.5f);
            Destroy(b, 2f);
        }
    }

}



