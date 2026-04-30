using UnityEngine;

public class AmmoControler : MonoBehaviour
{
    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 3f)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        PlayerControllerFPS player = other.GetComponent<PlayerControllerFPS>();
        if (player != null)
        {
            player._currentHealth -= 40f;
                
            Destroy(gameObject);
        }

    }
}
