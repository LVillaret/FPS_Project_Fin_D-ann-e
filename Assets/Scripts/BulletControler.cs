using UnityEngine;

public class BulletControler : MonoBehaviour
{
   private float _timer;

   private void Update()
   {
      _timer += Time.deltaTime;
      if (_timer > 3f)
         Destroy(gameObject);
   }

   private void OnTriggerEnter(Collider other)
   {
      EnemyController enemy = other.GetComponent<EnemyController>();
      if (enemy != null)
      {
         enemy.TakeDamage(1);
         Destroy(gameObject);
      }
   }
}
