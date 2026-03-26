using UnityEngine;

public class TargetController : MonoBehaviour
{
   [SerializeField] private float _currenteHealth;

   public void TakeDamage(int damage) 
   {
       _currenteHealth -= damage;
       if (_currenteHealth <= 0)
       {
           Destroy(gameObject);
       }
   }
}
