using UnityEngine;
using UnityEngine.UI;

public class EnemyCountUI : MonoBehaviour
{
    [SerializeField] private Text countText;
    
    private void Update()
    {
        countText.text = EnemyController.EnemiesNumber.ToString();
    }
}