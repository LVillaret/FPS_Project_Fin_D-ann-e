using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool _isPaused;

    [SerializeField] private Raycast _rc;
    [SerializeField] private PlayerControllerFPS _player;
    
    public GameObject pausePanel;
   
    public void Update()
    {
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
    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        _isPaused = true;
        _rc.enabled = false;
        _player.enabled = false;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        _isPaused = false;
        _rc.enabled = true;
        _player.enabled = true;
    }
    
}
