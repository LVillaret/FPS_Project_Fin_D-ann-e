using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathPanelManager : MonoBehaviour
{
    [SerializeField] private Raycast _rc;
    [SerializeField] private PlayerControllerFPS _player;
    [SerializeField] private GameObject _reticle;
    
    public float _deathDelay = 1f;
    
    public GameObject _deathPanel; 
    public GameObject _ammoPanel;
    public GameObject _enemiesPanel;
    
    public static bool _isDead = false;
    
    public void ShowDeathPanel()
    {
        StartCoroutine(DeathCoroutine());
    }

    IEnumerator DeathCoroutine()
    {
        _isDead  = true;
        yield return new WaitForSeconds(_deathDelay);
        _deathPanel.SetActive(true);
        _rc.enabled = false;
        _player.enabled = false;
        _reticle.SetActive(false);
        _ammoPanel.SetActive(false);
        _enemiesPanel.SetActive(false);
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
        Time.timeScale = 0;
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1;
        _isDead = false;
        
        SceneManager.LoadScene("GlobalScene");
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        _isDead = false;
        SceneManager.LoadScene("MainMenu");
    }
}
