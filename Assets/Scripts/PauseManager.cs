using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    
    
    [SerializeField] private Raycast _rc;
    [SerializeField] private PlayerControllerFPS _player;
    [SerializeField] private GameObject _reticle;
    
    public Slider _sensitivitySlider;
    public GameObject _pausePanel;
    public GameObject _ammoPanel;
    public GameObject _enemiesPanel;
    public AudioSource _audioSource;
  
    
    public static bool _isPaused = false;
    public static bool _isDead = false;
    
    public Text _sensitivityText;
    public void Start()
    {
        _sensitivitySlider.minValue = 0f;
        _sensitivitySlider.maxValue = 2000f;
        _sensitivitySlider.value = _player._rotationSpeed;
        _sensitivitySlider.onValueChanged.AddListener(ChangeSensitivity);
        _sensitivityText.text = _player._rotationSpeed.ToString("F0");
    }
    public void Update()
    {
        if (DeathPanelManager._isDead)
            return;
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ChangeSensitivity(float value)
    {
        _player._rotationSpeed = value;
        _sensitivityText.text = value.ToString("F0");
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        _pausePanel.SetActive(true);
        _isPaused = true;
        _rc.enabled = false;
        _player.enabled = false;
        _reticle.SetActive(false);
        _ammoPanel.SetActive(false);
        _enemiesPanel.SetActive(false);
        _audioSource.enabled = false;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
        
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        _pausePanel.SetActive(false);
        _isPaused = false;
        _rc.enabled = true;
        _player.enabled = true;
        _reticle.SetActive(true);
        _ammoPanel.SetActive(true);
        _enemiesPanel.SetActive(true);
        _audioSource.enabled = true;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = true;
    }
    
    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        _isDead = false;
        SceneManager.LoadScene("MainMenu");
    }
    
}
